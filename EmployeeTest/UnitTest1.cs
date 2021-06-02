using EmployeePayrollService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace EmployeeTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            EmployeeRepo repo = new EmployeeRepo();
            EmployeeModel employee = new EmployeeModel();
            employee.EmployeeName = "Terisa";
            employee.Department = "Hr";
            employee.PhoneNumber = "6302907678";
            employee.Address = "Usa";
            employee.Gender = 'F';
            employee.BasicPay = 10000.00;
            employee.Deductions = 1500.00;
            employee.StartDate = Convert.ToString("2020-11-03");
            employee.area = "chhattisgarh";
            var result = repo.Add_Employee(employee);
            
        }

        [TestMethod]
        public void GetAllEmployeeShouldReturnListOfRecords()
        {
            EmployeeRepo repo = new EmployeeRepo();
            repo.GetAllEmployee();

        }

    }
}
