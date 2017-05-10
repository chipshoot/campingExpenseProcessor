using System.Text.RegularExpressions;

namespace CampingExpenseProcessor.Dal
{
    public static class DataProcessorHelper
    {
        public static bool IsPositiveNumber(this string numStr)
        {
            if (string.IsNullOrEmpty(numStr))
            {
                return false;
            }

            var pat = new Regex(@"^[1-9]\d*$");
            return pat.Match(numStr).Success;
        }

        public static bool IsCharge(this string numStr)
        {
            if (string.IsNullOrEmpty(numStr))
            {
                return false;
            }

            var pat = new Regex(@"^[1-9]\d*.[0-9]{1,2}$");
            return pat.Match(numStr).Success;
        }

        public static bool IsCloseNumber(this string numStr)
        {
            if (string.IsNullOrEmpty(numStr))
            {
                return false;
            }

            return numStr.Trim() == "0";
        }
    }
}