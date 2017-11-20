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
        public static int Power(this int number, int power)
        {
            int currentNumber = number;

            for (int i = 1; i < power; i++)
            {
                currentNumber = currentNumber * number;
            }

            return currentNumber;
        }
    }
}
