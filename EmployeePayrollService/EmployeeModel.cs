using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePayrollService
{
    public class EmployeeModel
    {
        public int EmployeeID { get; set; }
        public int ID { get; set; }
        public int month { get; set; }
        public int salary { get; set; }
        public int EmpId { get; set; }
        public string EmployeeName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Department { get; set; }
        public char Gender { get; set; }
        public double BasicPay { get; set; }
        public double Deductions { get; set; }
        public double TaxablePay { get; set; }
        public double Tax { get; set; }
        public double NetPay { get; set; }
        public double area { get; set; }
        public DateTime StartDate { get; set; }



    }

}
