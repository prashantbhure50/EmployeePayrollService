using EmployeePayrollService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;

namespace EmployeePayrollTest
{

    public class UnitTest1
    {
        RestClient client;

        [TestInitialize]
        public void Setup()
        {
            client = new RestClient("http://localhost:3000");
        }

        private IRestResponse getEmployeeList()
        {
            RestRequest request = new RestRequest("/EmployeePayroll/", Method.GET);

            //act

            IRestResponse response = client.Execute(request);
            return response;
        }

        [TestMethod]
        public void onCallingGETApi_ReturnEmployeeList()
        {
            IRestResponse response = getEmployeeList();

            //assert
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);
            List<EmployeeModel> dataResponse = JsonConvert.DeserializeObject<List<EmployeeModel>>(response.Content);
            Assert.AreEqual(19, dataResponse.Count);
            foreach (var item in dataResponse)
            {
                System.Console.WriteLine("id: " + item.ID + "Name: " + item.EmployeeName + "PhoneNumber: "+item.PhoneNumber + "Address: "+ item.Address+ "Department: "+ item.Department+ "Gender: "+ item.Gender+ "BasicPay: " + item.BasicPay+ "Deduction: "+item.Deductions+ "TaxablePay: "+item.TaxablePay+ "Tax: "+item.Tax+ "NetPay: "+item.NetPay+ "StartDate: "+item.StartDate+ "Area: " +item.area);
            }
        }


        [TestMethod]
        public void givenEmployee_OnPost_ShouldReturnAddedEmployee()
        {
            RestRequest request = new RestRequest("/EmployeePayroll", Method.POST);
            JObject jObjectbody = new JObject();
            jObjectbody.Add("name", "Bhure");
            jObjectbody.Add("Salary", "15000");
            request.AddParameter("application/json", jObjectbody, ParameterType.RequestBody);

            //act
            IRestResponse response = client.Execute(request);
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.Created);
            EmployeeModel dataResponse = JsonConvert.DeserializeObject<EmployeeModel>(response.Content);
            Assert.AreEqual("Clark", dataResponse.EmployeeName);
            Assert.AreEqual(15000, dataResponse.BasicPay);

        }

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
            employee.StartDate = Convert.ToDateTime("2020-11-03");
            var result = repo.Add_Employee(employee);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetAllEmployeeShouldReturnListOfRecords()
        {
            EmployeeRepo repo = new EmployeeRepo();
            repo.GetAllEmployee();

        }

    }

}
