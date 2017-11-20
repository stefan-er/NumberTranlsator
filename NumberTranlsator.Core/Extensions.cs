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
        public static double Power(this int number, int power)
        {
            if (power == 0)
                return 1;

            int currentNumber = number;
            int positivePower = power > 0 ? power : power * -1;
   
            for (int i = 1; i < positivePower; i++)
            {
                currentNumber = currentNumber * number;
            }

            return power > 0 ? currentNumber : 1 / currentNumber;
        }
    }
}
