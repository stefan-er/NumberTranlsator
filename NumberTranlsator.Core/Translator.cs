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
        private Dictionary<int, string> uniqueNumbers;
        private string numberPlaceholder;
        private string suffixPlaceholder;
        
        public Translator()
        {
            this.smallTwoDigitsNumSuffix = "надесет";
            this.bigTwoDigitsNumSuffix = "десет";
            this.smallThreeDigitsNumSuffix = "ста";
            this.bigThreeDigitsNumSuffix = "стотин";
            this.fourDigitsNumSuffix = " хиляди";
            
            //Remark: not implemented for 0
            this.uniqueNumbers = new Dictionary<int, string>
            {
                { 1, "едно" },
                { 2, "две" },
                { 3, "три" },
                { 4, "четири" },
                { 5, "пет" },
                { 6, "шест"   },
                { 7, "седем"  },
                { 8, "осем" },
                { 9, "девет" },
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

            if (this.uniqueNumbers.ContainsKey(number))
            {
                translatedNumber = this.uniqueNumbers[number];
            }
            else
            {
                int digitsCount = number.DigitsCount();
                int numberDivider = (int)10.Power(digitsCount - 1);
                int reminder = number % numberDivider;
                int firstDigit = number / numberDivider;
                int roundedNumber = firstDigit * numberDivider;
                string suffix = GetSuffix(number);

                if (number >= 13 && number <= 19)
                {
                    translatedNumber = TranslateNumber(reminder) + suffix;
                }
                else if (number > 20 && number <= 99)
                {
                    translatedNumber = reminder == 0 ?
                        translatedNumber = TranslateNumber(firstDigit) + suffix :
                        translatedNumber = TranslateNumber(roundedNumber) + " и " + TranslateNumber(reminder);
                }
                else
                {
                    string roundedNumberTranslated = uniqueNumbers.ContainsKey(roundedNumber) ?
                        this.uniqueNumbers[roundedNumber] :
                        TranslateNumber(firstDigit) + suffix;
                    
                    int reminderDivider = (int)10.Power(digitsCount - 2);
                    string conjunction = (reminder % reminderDivider != 0) ? " " : " и ";

                    translatedNumber = roundedNumberTranslated + conjunction + TranslateNumber(reminder);
                }
            }

            return translatedNumber;
        }
        private string GetSuffix(int number)
        {
            int digitsCount = number.DigitsCount();

            string suffix = this.suffixPlaceholder;

            switch (digitsCount)
            {
                case 2:
                    suffix = number < 20 ? this.smallTwoDigitsNumSuffix : this.bigTwoDigitsNumSuffix;
                    break;
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
