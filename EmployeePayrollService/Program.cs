using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePayrollService
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Employee Payroll!");
            EmployeeRepo repo = new EmployeeRepo();
            EmployeeModel employee = new EmployeeModel();
             employee.EmployeeName = "Mohan";
            //employee.BasicPay = 10000.00M;
            employee.StartDate = Convert.ToString("2020-11-03");
            employee.Gender = 'M';
            employee.PhoneNumber = "6302907678";
            employee.Address = "02-Patna";
            //employee.Department = "Tech1";
            //employee.Deductions = 1500.00;
            //employee.TaxablePay = 23444.00;
            //employee.Tax = 4000.00;
            //employee.NetPay = 700000.00;
            //employee.area=2223.33; 
            //employee.ID = 1;
            //employee.salary = 2999;

            if (repo.Insert_Employee_Record(employee))
                Console.WriteLine("Records added successfully");
           // repo.GetAllEmployee();
            Console.ReadKey();
        }
    }
}
