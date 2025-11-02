using GuitarCentre.App_Code;
using System;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GuitarCentre
{
    public partial class Employees : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // טעינה ראשונית אפשרית, SqlDataSource דואג לנתונים, אין חובה לעשות DataBind כאן.
            if (!IsPostBack)
            {
                Label1.Visible = false; // יש לך Label1 ליד השכר - נשתמש בו להודעות.
            }
        }

        /// <summary>
        /// ולידציה שרתית לת.ז. ישראלית (אם עדיין מחובר בקובץ ה-ASPX ל-OnServerValidate).
        /// אם עברת ל-RegularExpressionValidator בלבד, אפשר להשאיר את זה - לא מזיק.
        /// </summary>
        protected void IDvalidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            string id = (args?.Value ?? string.Empty).Trim();

            // השלם ל-9 ספרות
            if (id.Length == 8) id = "0" + id;

            args.IsValid = IsValidIsraeliID(id);
        }

        private bool IsValidIsraeliID(string id)
        {
            if (string.IsNullOrWhiteSpace(id) || id.Length != 9 || !long.TryParse(id, out _))
                return false;

            int sum = 0;
            bool doubleDigit = false;

            for (int i = id.Length - 1; i >= 0; i--)
            {
                int digit = id[i] - '0';
                int v = doubleDigit ? digit * 2 : digit;
                if (v > 9) v -= 9;
                sum += v;
                doubleDigit = !doubleDigit;
            }

            return sum % 10 == 0;
        }

        protected void EmployeeRegisterBtn_Click(object sender, EventArgs e)
        {
            try
            {
                Label1.Visible = false;
                Label1.Text = string.Empty;

                // אם יש Validators ב-ASP.NET, ודא שהם עברו
                if (!Page.IsValid)
                {
                    Fail("Please fix validation errors.");
                    return;
                }

                // איסוף קלט
                string firstName = (EmployeeRegisterFirstName.Text ?? "").Trim();
                string lastName = (EmployeeRegisterLastName.Text ?? "").Trim();
                string email = (EmployeeRegisterEmail.Text ?? "").Trim();
                string address = (EmployeeRegisterAdress.Text ?? "").Trim(); // שומר את ה-ID Adress כדי לא לשבור רפרנסים
                string position = (EmployeeRegisterPosition.Text ?? "").Trim();
                string branch = (EmployeeRegisterBranch.Text ?? "").Trim();
                string phone = (EmployeeRegisterPhoneNumber.Text ?? "").Trim();
                string idStr = (EmployeeRegisterID.Text ?? "").Trim();
                string birthStr = (EmployeeRegisterBirhtDate.Text ?? "").Trim();
                string salaryStr = (EmployeeRegisterSalary.Text ?? "").Trim();

                // ולידציה בסיסית בצד השרת
                if (string.IsNullOrWhiteSpace(firstName) ||
                    string.IsNullOrWhiteSpace(lastName) ||
                    string.IsNullOrWhiteSpace(idStr) ||
                    string.IsNullOrWhiteSpace(email))
                {
                    Fail("Please fill all required fields.");
                    return;
                }

                // ת.ז.
                if (idStr.Length == 8) idStr = "0" + idStr; // תמיכה גם ב-8 ספרות
                if (!IsValidIsraeliID(idStr))
                {
                    Fail("Invalid Israeli ID.");
                    return;
                }

                if (!int.TryParse(idStr, out int employeeID))
                {
                    Fail("Employee ID must be numeric.");
                    return;
                }

                // שכר
                if (!decimal.TryParse(salaryStr, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal salary))
                {
                    // ננסה גם תרבויות מקומיות למקרה של פסיק/נקודה
                    if (!decimal.TryParse(salaryStr, out salary))
                    {
                        Fail("Please enter a valid salary.");
                        return;
                    }
                }

                // תאריך לידה
                if (!DateTime.TryParse(birthStr, out DateTime birthdate))
                {
                    Fail("Please enter a valid birthdate.");
                    return;
                }

                // יצירת אובייקט ושמירה — משתמש בגרסה המתוקנת של EmployeesClass:
                // ctor: (int employeeID, string firstName, string lastName, string email, string address, string position, string branch, string phone, DateTime birthdate, decimal salary)
                var newEmployee = new EmployeesClass(
                    employeeID,
                    firstName,
                    lastName,
                    email,
                    address,
                    position,
                    branch,
                    phone,
                    birthdate,
                    salary
                );

                // בגרסה שתיקנו: AddEmployee() (לא AddEmployees)
                newEmployee.AddEmployee();

                // ריענון הגריד
                GridView1.PageIndex = 0;
                GridView1.DataBind();

                // איפוס טופס
                ClearForm();

                Success("Employee registered successfully.");
            }
            catch (Exception ex)
            {
                Fail("Employee registration failed: " + ex.Message);
            }
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            // רענון עדין של הטבלה
            GridView1.DataBind();
        }

        // האירועים הריקים נשארים כדי לא לשבור קישורים מה-ASPX
        protected void EmployeeRegisterFirstName_TextChanged(object sender, EventArgs e) { }
        protected void EmployeeRegisterLastName_TextChanged(object sender, EventArgs e) { }
        protected void EmployeeRegisterID_TextChanged(object sender, EventArgs e) { }
        protected void EmployeeRegisterBirhtDate_TextChanged(object sender, EventArgs e) { }
        protected void EmployeeRegisterPosition_TextChanged(object sender, EventArgs e) { }
        protected void EmployeeRegisterSalary_TextChanged(object sender, EventArgs e) { }
        protected void EmployeeRegisterBranch_TextChanged(object sender, EventArgs e) { }
        protected void EmployeeRegisterEmail_TextChanged(object sender, EventArgs e) { }
        protected void EmployeeRegisterPhoneNumber_TextChanged(object sender, EventArgs e) { }
        protected void EmployeeRegisterAdress_TextChanged(object sender, EventArgs e) { }
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e) { }

        // ===== helpers =====

        private void ClearForm()
        {
            EmployeeRegisterFirstName.Text = string.Empty;
            EmployeeRegisterLastName.Text = string.Empty;
            EmployeeRegisterID.Text = string.Empty;
            EmployeeRegisterBirhtDate.Text = string.Empty;
            EmployeeRegisterPosition.Text = string.Empty;
            EmployeeRegisterSalary.Text = string.Empty;
            EmployeeRegisterBranch.Text = string.Empty;
            EmployeeRegisterEmail.Text = string.Empty;
            EmployeeRegisterPhoneNumber.Text = string.Empty;
            EmployeeRegisterAdress.Text = string.Empty;
        }

        private void Fail(string msg)
        {
            Label1.Visible = true;
            Label1.Text = msg;
            Label1.ForeColor = System.Drawing.Color.Red;
        }

        private void Success(string msg)
        {
            Label1.Visible = true;
            Label1.Text = msg;
            Label1.ForeColor = System.Drawing.Color.Green;
        }
    }
}
