using System;
using System.Data.SqlClient;

namespace GuitarCentre.App_Code
{
    public class EmployeesClass
    {
        private int employeeID;
        private string firstName;
        private string lastName;
        private string email;
        private string address;
        private string position;
        private string branch;
        private string phone;
        private DateTime birthdate;
        private decimal salary;

        public int EmployeeID { get => employeeID; set => employeeID = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string Email { get => email; set => email = value; }
        public string Address { get => address; set => address = value; }
        public string Position { get => position; set => position = value; }
        public string Branch { get => branch; set => branch = value; }
        public string Phone { get => phone; set => phone = value; }
        public DateTime Birthdate { get => birthdate; set => birthdate = value; }
        public decimal Salary { get => salary; set => salary = value; }

        public EmployeesClass(int employeeID, string firstName, string lastName, string email, string address, string position, string branch, string phone, DateTime birthdate, decimal salary)
        {
            this.employeeID = employeeID;
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.address = address;
            this.position = position;
            this.branch = branch;
            this.phone = phone;
            this.birthdate = birthdate;
            this.salary = salary;
        }

        public void AddEmployee()
        {
            using (Connection conn = new Connection())
            {
                conn.OpenConnection();
                SqlCommand cmd = conn.Command(@"
                    INSERT INTO tblEmployees 
                    (employeeID, firstName, lastName, email, address, position, branch, phone, birthdate, salary)
                    VALUES (@employeeID, @firstName, @lastName, @Email, @Address, @Position, @Branch, @Phone, @Birthdate, @Salary)
                ");

                cmd.Parameters.AddWithValue("@employeeID", employeeID);
                cmd.Parameters.AddWithValue("@firstName", firstName);
                cmd.Parameters.AddWithValue("@lastName", lastName);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Address", address);
                cmd.Parameters.AddWithValue("@Position", position);
                cmd.Parameters.AddWithValue("@Branch", branch);
                cmd.Parameters.AddWithValue("@Phone", phone);
                cmd.Parameters.AddWithValue("@Birthdate", birthdate);
                cmd.Parameters.AddWithValue("@Salary", salary);

                cmd.ExecuteNonQuery();
            }
        }
    }
}
