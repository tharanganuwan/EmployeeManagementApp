using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementApp.Helpers
{
    public static class EmailBody
    {
        public static string EmailStringBody(string email, string emailToken)
        {
            return
                $@"<html>
                <head>
                    <body>
                        <h1>Reset Your Password</h1>
                        <p>We received a request to reset your password. Don’t worry,we are here to help you. </p>
                        
                        <a href=""http://localhost:4200/reset?email={email}&amp;code={emailToken}"" target=""_blank"">Reset Password</a>
                    </body>
                </head>
              </html>";

        }
    }
}
