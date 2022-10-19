namespace linearAPI.Util
{
    public class TextUtils
    {
        public static string CapitalizeFirstLetter(string str)
        {
            if (str.Length == 0)
                return "";
            else if (str.Length == 1)
                return str.ToUpper();
            else
                return char.ToUpper(str[0]) + str.Substring(1);
        }
    }
}
