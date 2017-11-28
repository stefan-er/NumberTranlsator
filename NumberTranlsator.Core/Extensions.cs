namespace NumberTranlsator.Core
{
    public  static class Extensions
    {
        public static int DigitsCount(this int number)
        {
            int digitsCount = 0;
            int currentNumber = number;

            do
            {
                currentNumber = currentNumber / 10;
                digitsCount++;
            }
            while (currentNumber > 0);

            return digitsCount;
        }
        public static string FirstLetterToUpper(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            if (str.Length > 1)
                return char.ToUpper(str[0]) + str.Substring(1);

            return str.ToUpper();
        }
    }
}
