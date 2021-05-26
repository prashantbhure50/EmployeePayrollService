using EmployeePayrollService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace EmployeePayrollTest
{

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
            employee.BasicPay = 10000.00M;
            employee.Deductions = 1500.00;
            employee.StartDate = Convert.ToDateTime("2020-11-03");
            //Mock<EmployeeRepo> mockObj = new Mock<EmployeeRepo>();
            //mockObj.Setup(t=>t.AddEmployee(It.IsIn<EmployeeModel>)).return (true);
            var result = repo.AddEmployee(employee);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetAllEmployeeShouldReturnListOfRecords()
        {
            //EmployeeRepo repo = new EmployeeRepo();
            //var result = repo.GetAllEmployee();
            //Assert.IsTrue(result);
        }


    }

}
