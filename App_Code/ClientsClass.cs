using System;
using System.Data.SqlClient;

namespace GuitarCentre.App_Code
{
    public class ClientsClass
    {
        public int ClientID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

        public ClientsClass(int clientID, string firstName, string lastName, string email, string address, string phone)
        {
            ClientID = clientID;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Address = address;
            Phone = phone;
        }

        public void AddClient()
        {
            try
            {
                using (Connection conn = new Connection())
                {
                    conn.OpenConnection();

                    using (SqlCommand cmd = conn.Command(
                        "INSERT INTO tblClients (clientID, firstName, lastName, email, address, phone) " +
                        "VALUES (@clientID, @firstName, @lastName, @Email, @Address, @Phone)"
                    ))
                    {
                        cmd.Parameters.AddWithValue("@clientID", ClientID);
                        cmd.Parameters.AddWithValue("@firstName", FirstName);
                        cmd.Parameters.AddWithValue("@lastName", LastName);
                        cmd.Parameters.AddWithValue("@Email", Email);
                        cmd.Parameters.AddWithValue("@Address", Address);
                        cmd.Parameters.AddWithValue("@Phone", Phone);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to add client: " + ex.Message);
            }
        }
    }
}
