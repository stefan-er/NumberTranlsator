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

            numberPlaceholder = "{непознато число}";
        }
        
        public string TryTranslateNumber(string word)
        {
            string translatedNumber = word;

            int number;
            if (int.TryParse(word, out number) && number > 0 && number < 10000 )
            {
                translatedNumber = TranslateNumber(number, word.Length);
            }

            return translatedNumber;
        }

        private string TranslateNumber(int number, int digitsCount)
        {
            string translatedNumber = this.numberPlaceholder;

            switch (digitsCount)
            {
                case 1:
                    translatedNumber = TranslateOneDigitNumber(number);
                    break;
                case 2:
                    translatedNumber = TranslateTwoDigitNumber(number);
                    break;
                case 3:
                    translatedNumber = TranslateThreeDigitNumber(number);
                    break;
                case 4:
                    translatedNumber = TranslateFourDigitNumber(number);
                    break;
            }

            return translatedNumber;
        }
        private string TranslateFourDigitNumber(int number)
        {
            string translatedNumber = this.numberPlaceholder;

            if (uniqueNumbers.ContainsKey(number))
            {
                translatedNumber = uniqueNumbers[number];
            }
            else
            {
                int reminder = number % 1000;
                int firstDigit = number / 1000;
                int roundedNumber = (firstDigit) * 1000;

                string thousandsTranslated = this.numberPlaceholder;
                if (uniqueNumbers.ContainsKey(roundedNumber))
                {
                    thousandsTranslated = uniqueNumbers[roundedNumber];
                }
                else
                {
                    thousandsTranslated = TranslateNumber(firstDigit, 1) + fourDigitsNumSuffix;
                }
                
                string conjunction = " ";
                if (reminder % 100 == 0)
                {
                    conjunction = " и ";
                }

                translatedNumber = thousandsTranslated + conjunction + TranslateNumber(reminder, 3);
            }

            return translatedNumber;
        }
        private string TranslateThreeDigitNumber(int number)
        {
            string translatedNumber = this.numberPlaceholder;

            if(uniqueNumbers.ContainsKey(number))
            {
                translatedNumber = uniqueNumbers[number];
            }
            else
            {
                int reminder = number % 100;
                int firstDigit = number / 100;
                int roundedNumber = firstDigit * 100;

                string hundredsTranslated = this.numberPlaceholder;
                if (uniqueNumbers.ContainsKey(roundedNumber))
                {
                    hundredsTranslated = uniqueNumbers[roundedNumber];
                }
                else
                {
                    string hundredsSuffix = roundedNumber < 400 ? this.smallThreeDigitsNumSuffix : this.bigThreeDigitsNumSuffix;
                    hundredsTranslated = TranslateNumber(firstDigit, 1) + hundredsSuffix;
                }

                string conjunction = " ";
                if (reminder % 10 == 0)
                {
                    conjunction = " и ";
                }

                translatedNumber = hundredsTranslated + conjunction + TranslateNumber(reminder, 2);
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
                string lastDigitTranslated = TranslateNumber(lastDigit, 1);
                translatedNumber = lastDigitTranslated + smallTwoDigitsNumSuffix;
            }
            else if(number > 20)
            {
                int lastDigit = number % 10;
                int firstDigit = number / 10;

                if (lastDigit == 0)
                {
                    string firstDigitTranslated = TranslateNumber(firstDigit, 1);
                    translatedNumber = firstDigitTranslated + bigTwoDigitsNumSuffix;
                }
                else
                {
                    int roundedNumber = firstDigit * 10;
                    translatedNumber = TranslateNumber(roundedNumber, 2) + " и " + TranslateNumber(lastDigit, 1);
                }
            }

            return translatedNumber;
        }
        private string TranslateOneDigitNumber(int number)
        {
            return this.digits[number];
        }
    }
}
