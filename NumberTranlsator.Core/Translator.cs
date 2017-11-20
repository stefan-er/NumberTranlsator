using System.Collections.Generic;

namespace NumberTranlsator.Core
{
    public class Translator
    {
        private string smallTwoDigitsNumSuffix;
        private string bigTwoDigitsNumSuffix;
        private string smallThreeDigitsNumSuffix;
        private string bigThreeDigitsNumSuffix;
        private string fourDigitsNumSuffix;
        private Dictionary<int, string> digits;
        private Dictionary<int, string> uniqueNumbers;
        private string numberPlaceholder;
        private string suffixPlaceholder;
        
        public Translator()
        {
            smallTwoDigitsNumSuffix = "надесет";
            bigTwoDigitsNumSuffix = "десет";
            smallThreeDigitsNumSuffix = "ста";
            bigThreeDigitsNumSuffix = "стотин";
            fourDigitsNumSuffix = " хиляди";

            //Remark: not implemented for 0
            digits = new Dictionary<int, string>
            {
                { 1, "едно" },
                { 2, "две" },
                { 3, "три" },
                { 4, "четири" },
                { 5, "пет" },
                { 6, "шест"   },
                { 7, "седем"  },
                { 8, "осем" },
                { 9, "девет" }
            };

            uniqueNumbers = new Dictionary<int, string> {
                { 10, "десет" },
                { 11, "единадесет" },
                { 12, "дванадесет" },
                { 20, "двадесет" },
                { 100, "сто" },
                { 1000, "хиляда" },
                { 2000, "две хиляди" }
            };

            this.numberPlaceholder = "{непознато число}";
            this.suffixPlaceholder = "{непозната наставка}";
        }
        
        public string TryTranslateNumber(string word)
        {
            string translatedNumber = word;

            int number;
            if (int.TryParse(word, out number) && number > 0 && number < 10000 )
            {
                translatedNumber = TranslateNumber(number);
            }

            return translatedNumber;
        }

        private string TranslateNumber(int number)
        {
            string translatedNumber = this.numberPlaceholder;

            int digitsCount = number.DigitsCount();
            switch (digitsCount)
            {
                case 1:
                    translatedNumber = TranslateOneDigitNumber(number);
                    break;
                case 2:
                    translatedNumber = TranslateTwoDigitNumber(number);
                    break;
                case 3:
                case 4:
                    {
                        string suffix = GetSuffix(number, digitsCount);
                        int divider = 10.Power(digitsCount - 1);

                        translatedNumber = TranslateManyDigitNumber(number, divider, suffix);
                    }
                    break;
            }

            return translatedNumber;
        }

        private string TranslateManyDigitNumber(int number, int divider, string suffix)
        {
            string translatedNumber = this.numberPlaceholder;

            if (uniqueNumbers.ContainsKey(number))
            {
                translatedNumber = uniqueNumbers[number];
            }
            else
            {
                int reminder = number % divider;
                int firstDigit = number / divider;
                int roundedNumber = firstDigit * divider;

                string bigestNumTranslated = uniqueNumbers.GetValueOrDefault(roundedNumber) ?? TranslateNumber(firstDigit) + suffix;
                
                string conjunction = " ";
                if (reminder % (divider / 10) == 0)
                {
                    conjunction = " и ";
                }

                translatedNumber = bigestNumTranslated + conjunction + TranslateNumber(reminder);
            }

            return translatedNumber;
        }
        private string TranslateTwoDigitNumber(int number)
        {
            string translatedNumber = this.numberPlaceholder;

            if (uniqueNumbers.ContainsKey(number))
            {
                translatedNumber = uniqueNumbers[number];
            }
            else if (number >= 13 && number <= 19)
            {
                int lastDigit = number % 10;
                string lastDigitTranslated = TranslateNumber(lastDigit);
                translatedNumber = lastDigitTranslated + smallTwoDigitsNumSuffix;
            }
            else if(number > 20)
            {
                int lastDigit = number % 10;
                int firstDigit = number / 10;

                if (lastDigit == 0)
                {
                    string firstDigitTranslated = TranslateNumber(firstDigit);
                    translatedNumber = firstDigitTranslated + bigTwoDigitsNumSuffix;
                }
                else
                {
                    int roundedNumber = firstDigit * 10;
                    translatedNumber = TranslateNumber(roundedNumber) + " и " + TranslateNumber(lastDigit);
                }
            }

            return translatedNumber;
        }
        private string TranslateOneDigitNumber(int number)
        {
            return this.digits[number];
        }

        private string GetSuffix(int number, int digitsCount = -1)
        {
            if (digitsCount == -1)
                digitsCount = number.DigitsCount();

            string suffix;
            switch (digitsCount)
            {
                case 3:
                    suffix = (number / 400) > 0 ? this.bigThreeDigitsNumSuffix : this.smallThreeDigitsNumSuffix;
                    break;
                case 4:
                    suffix = this.fourDigitsNumSuffix;
                    break;
                default:
                    suffix = this.suffixPlaceholder;
                    break;
            }

            return suffix;
        }
    }
}
