using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePayrollService
{
    public class EmployeeRepo
    {
        public static string connectionString = @"Data Source=(localdb)\ProjectsV13;Initial Catalog=Payroll_service13;Integrated Security=True";
        SqlConnection connection = new SqlConnection(connectionString);
        public void GetAllEmployee()
        {
            try
            {
                EmployeeModel employeeModel = new EmployeeModel();
                using (this.connection)
                {
                    string query = @"Select * from Erdigram;";
                    SqlCommand cmd = new SqlCommand(query, this.connection);
                    this.connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            employeeModel.EmployeeID = dr.GetInt32(0);

                            employeeModel.EmployeeName = dr.GetString(1);
                            employeeModel.BasicPay = dr.GetDouble(2);
                            employeeModel.StartDate = dr.GetDateTime(3);
                            employeeModel.Gender = Convert.ToChar(dr.GetString(4));
                            //employeeModel.PhoneNumber = dr.GetString(5);
                            //employeeModel.Address = dr.GetString(6);
                            //employeeModel.Department = dr.GetString(7);
                            //employeeModel.Deductions = dr.GetDouble(7);
                            //employeeModel.TaxablePay = dr.GetDouble(8);
                            //employeeModel.Tax = dr.GetDouble(9);
                            //employeeModel.NetPay = dr.GetDouble(10);
                            System.Console.WriteLine(employeeModel.EmployeeName + " " + employeeModel.BasicPay + " " + employeeModel.StartDate + " " + employeeModel.Gender + " " + employeeModel.PhoneNumber + " " + employeeModel.Address + " " + employeeModel.Department + " " + employeeModel.Deductions + " " + employeeModel.TaxablePay + " " + employeeModel.Tax + " " + employeeModel.NetPay);
                            System.Console.WriteLine("\n");
                        }
                    }
                    else
                    {
                        System.Console.WriteLine("No data found");
                    }
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
        }

        public bool Add_Employee(EmployeeModel model)
        {
            try
            {
                using (this.connection)
                {
                    //var qury=values()
                    SqlCommand command = new SqlCommand("SpAddEmployeeDetails", this.connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeName", model.EmployeeName);
                    command.Parameters.AddWithValue("@PhoneNumber", model.PhoneNumber);
                    command.Parameters.AddWithValue("@Address", model.Address);
                    command.Parameters.AddWithValue("@Department", model.Department);
                    command.Parameters.AddWithValue("@Gender", model.Gender);
                    command.Parameters.AddWithValue("@BasicPay", model.BasicPay);
                    command.Parameters.AddWithValue("@Deductions", model.Deductions);
                    command.Parameters.AddWithValue("@TaxablePay", model.TaxablePay);
                    command.Parameters.AddWithValue("@Tax", model.Tax);
                    command.Parameters.AddWithValue("@NetPay", model.NetPay);
                    command.Parameters.AddWithValue("@StartDate", DateTime.Now);
                    command.Parameters.AddWithValue("@area", model.area);
                    //command.Parameters.AddWithValue("@City", model.City);
                    //command.Parameters.AddWithValue("@Country", model.Country);
                    this.connection.Open();
                    var result = command.ExecuteNonQuery();
                    this.connection.Close();
                    if (result != 0)
                    {

                        return true;
                    }
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                this.connection.Close();
            }
            return false;
        }

        public bool Update_Salary(EmployeeModel model) 
        {
            try
            {
                using (this.connection)
                {
                    //var qury=values()
                    SqlCommand command = new SqlCommand("spUpdateEmployeeSalary", this.connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", model.ID);
                    command.Parameters.AddWithValue("@salary", model.salary);
                    
                    this.connection.Open();
                    var result = command.ExecuteNonQuery();
                    this.connection.Close();
                    if (result != 0)
                    {

                        return true;
                    }
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                this.connection.Close();
            }
            return false;
        }
        public bool Insert_Employee_Record(EmployeeModel employee)
        {
            employee.Deductions = Convert.ToInt32(0.2 * employee.BasicPay);
            employee.TaxablePay = employee.BasicPay - employee.Deductions;
            employee.Tax = Convert.ToInt32(0.1 * employee.TaxablePay);
            employee.NetPay = employee.BasicPay - employee.Tax;
            SqlConnection connection = new SqlConnection(connectionString);


            string storedProcedure = "sp_InsertEmployeePayrollDetails";
            string storedProcedurePayroll = "sp_InsertPayrollDetails";
            using (connection)
            {
                connection.Open();
                SqlTransaction transaction;
                transaction = connection.BeginTransaction("Insert Employee Transaction");
                try
                {
                    SqlCommand sqlCommand = new SqlCommand(storedProcedure, connection, transaction);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@StartDate", employee.StartDate);
                    sqlCommand.Parameters.AddWithValue("@Name", employee.EmployeeName);
                    sqlCommand.Parameters.AddWithValue("@Gender", employee.Gender);
                    sqlCommand.Parameters.AddWithValue("@PhoneNumber", employee.PhoneNumber);
                    sqlCommand.Parameters.AddWithValue("@Address", employee.Address);
                    SqlParameter outPutVal = new SqlParameter("@scopeIdentifier", SqlDbType.Int);
                    outPutVal.Direction = ParameterDirection.Output;
                    sqlCommand.Parameters.Add(outPutVal);

                    sqlCommand.ExecuteNonQuery();
                    //SqlCommand sqlCommand1 = new SqlCommand(storedProcedurePayroll, connection, transaction);
                    //sqlCommand1.CommandType = CommandType.StoredProcedure;
                    //sqlCommand1.Parameters.AddWithValue("@ID", outPutVal.Value);
                    //sqlCommand1.Parameters.AddWithValue("@BasicPay", employee.BasicPay);
                    //sqlCommand1.Parameters.AddWithValue("@Deduction", employee.Deductions);
                    //sqlCommand1.Parameters.AddWithValue("@TaxablePay", employee.TaxablePay);
                    //sqlCommand1.Parameters.AddWithValue("@IncomeTax", employee.Tax);
                    //sqlCommand1.Parameters.AddWithValue("@NetPay", employee.NetPay);
                    //sqlCommand1.ExecuteNonQuery();
                    transaction.Commit();
                    connection.Close();
                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception ex2)
                    {

                        Console.WriteLine(ex2.Message);
                    }
                }
                return false;
            }

        }
        public void Method_For_Multi_Threading()
        {
            try
            {
                EmployeeModel employee = new EmployeeModel();
                employee.EmployeeName = "JACKSON";
                employee.Department = "offshore";
                employee.PhoneNumber = "6304525678";
                employee.Address = "05-JABALPUR";
                employee.Gender = 'M';
                employee.BasicPay = 200000.00;
                employee.Deductions = 15000;
                employee.StartDate = Convert.ToDateTime("2020-01-03");
                Task thread = new Task(() =>
                {
                    // Console.WriteLine("Employee being added: " + employeeData.EmployeeName);
                    this.AddEmployee(employee);
                    // Console.WriteLine("Employee added: " + employeeData.EmployeeName);
                });
                thread.Start();
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
            }

        }

    }

}
