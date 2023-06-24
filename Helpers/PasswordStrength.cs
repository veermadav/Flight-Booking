using System.Text.RegularExpressions;
using System.Text;

namespace User_Management.Helpers
{
    public class PasswordStrength
    {
        public static string CheckPasswordStrength(string password)
        {
            StringBuilder sb = new StringBuilder();
            if (password.Length < 8)
                sb.Append("Minimum password length should be 8" + Environment.NewLine);
            if (!(Regex.IsMatch(password, "[a-z]") && Regex.IsMatch(password, "[A-Z]") && Regex.IsMatch(password, "[0-9]")))
                sb.Append("Password should be alphanumeric" + Environment.NewLine);
            if (!(Regex.IsMatch(password, "[>,<,?,/,{,},{,},+,=,_,-,),(,*,&,^,%,$,#,@,!,~,`]")))
                sb.Append("Password should contain special chars" + Environment.NewLine);
            return sb.ToString();
        }
    }
}
