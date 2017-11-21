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
    }
}
