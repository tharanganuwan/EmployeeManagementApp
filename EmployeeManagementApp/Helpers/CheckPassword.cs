using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EmployeeManagementApp.Helpers
{
    public class CheckPassword
    {
        public static string ChechPasswordStrength(string password)
        {
            StringBuilder sb = new StringBuilder();

            if (password.Length < 8)
                sb.Append("Minimum password length should be 8." + Environment.NewLine);


            if (!Regex.IsMatch(password, @"^(?=.*[a-z])"))
                sb.Append("Password should contain at least one lowercase letter." + Environment.NewLine);

            if (!Regex.IsMatch(password, @"^(?=.*[A-Z])"))
                sb.Append("Password should contain at least one uppercase letter." + Environment.NewLine);

            if (!Regex.IsMatch(password, @"^(?=.*\d)"))
                sb.Append("Password should contain at least one digit." + Environment.NewLine);

            if (!Regex.IsMatch(password, @"^(?=.*[^a-zA-Z0-9])"))
                sb.Append("Password should contain at least one special character." + Environment.NewLine);

            return sb.ToString();
        }
    }
}
