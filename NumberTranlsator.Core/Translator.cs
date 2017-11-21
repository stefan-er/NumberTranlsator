using System;
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
        private Dictionary<int, string> uniqueNumbersTranslations;
        private string numberPlaceholder;
        private string suffixPlaceholder;
        private string emptyConjunction;
        private string fullConjunction;
        
        public Translator()
        {
            this.smallTwoDigitsNumSuffix = "надесет";
            this.bigTwoDigitsNumSuffix = "десет";
            this.smallThreeDigitsNumSuffix = "ста";
            this.bigThreeDigitsNumSuffix = "стотин";
            this.fourDigitsNumSuffix = " хиляди";
            
            this.uniqueNumbersTranslations = new Dictionary<int, string>
            {
                { 0, "нула" },
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

            this.emptyConjunction = " ";
            this.fullConjunction = " и ";
        }
        
        public string TryTranslateNumber(string word)
        {
            string translatedNumber = word;

            if (int.TryParse(word, out int number) && number > -1 && number < 1000000)
            {
                translatedNumber = TranslateNumber(number);
            }

            return translatedNumber;
        }

        private string TranslateNumber(int number)
        {
            string translatedNumber = this.numberPlaceholder;

            if (this.uniqueNumbersTranslations.ContainsKey(number))
            {
                translatedNumber = this.uniqueNumbersTranslations[number];
            }
            else
            {
                int digitsCount = number.DigitsCount();
                int numberDivider = GetNumberDivider(digitsCount);
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
                        translatedNumber = TranslateNumber(roundedNumber) + this.fullConjunction + TranslateNumber(reminder);
                }
                else
                {
                    string roundedNumberTranslated = uniqueNumbersTranslations.ContainsKey(roundedNumber) ?
                        this.uniqueNumbersTranslations[roundedNumber] :
                        TranslateNumber(firstDigit) + suffix;

                    if (reminder == 0)
                    {
                        translatedNumber = roundedNumberTranslated;
                    }
                    else
                    {
                        int reminderDivider = (int)Math.Pow(10, digitsCount - 2);
                        string conjunction = GetConjunction(reminder, reminderDivider);

                        translatedNumber = roundedNumberTranslated + conjunction + TranslateNumber(reminder);
                    }
                }
            }

            return translatedNumber;
        }
        private int GetNumberDivider(int digitsCount)
        {
            int power = digitsCount - 1;

            if (digitsCount > 4 && digitsCount <= 6)
                power = 3;

            return (int)Math.Pow(10, power);
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
        private string GetConjunction(int number, int divider)
        {
            int reminder = number % divider;
            int @private = number / divider;
            string conjunction = (@private > 1 && reminder > 0) ? this.emptyConjunction : this.fullConjunction;

            if (divider > 10 && @private == 0 && reminder > 10)
            {
                string innerConjunction = GetConjunction(number % 10, divider / 10);
                
                conjunction = innerConjunction == this.fullConjunction ? this.emptyConjunction : this.fullConjunction;
            }
            
            return conjunction;
        }
    }
}
