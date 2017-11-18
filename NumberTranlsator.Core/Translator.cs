using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NumberTranlsator.Core
{
    public class Translator
    {
        private char[] wordsDelimiters;
        private string smallTwoDigitsSuffix;
        private string bigTwoDigitsSuffix;
        private string smallThreeDigitsSuffix;
        private string bigThreeDigitsSuffix;
        private string fourDigitsSuffix;
        private Dictionary<int, string> digits;
        private Dictionary<int, string> uniqueNumbers;
        
        public Translator()
        {
            wordsDelimiters = new char[] { ' ', ',', '.', '!', '?', '-', ':', ';', '(', ')', '"', '\'' };

            smallTwoDigitsSuffix = "надесет";
            bigTwoDigitsSuffix = "десет";
            smallThreeDigitsSuffix = "ста";
            bigThreeDigitsSuffix = "стотин";
            fourDigitsSuffix = " хиляди";

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
        }

        public IEnumerable<string> GetWordsFromTexFile(string filePath)
        {
            List<string> words = new List<string>();
            
            using (StreamReader reader = new StreamReader(filePath, Encoding.UTF8))
            {
                string line = reader.ReadLine();
                while (line != null)
                {
                    IEnumerable<string> lineWords = GetWordsFromString(line);

                    words.AddRange(lineWords);

                    line = reader.ReadLine();
                }
            }

            return words;
        }
        public string TryTranslateNumber(string word)
        {
            string translatedNumber = word;

            int number;
            if (int.TryParse(word, out number) && number > 0 && number < 10000 )
            {
                translatedNumber = "{непознато число}";
                switch (word.Length)
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
                    default:
                        break;
                }
            }

            return translatedNumber;
        }

        private IEnumerable<string> GetWordsFromString(string text)
        {
            string[] words = text.Split(wordsDelimiters, StringSplitOptions.RemoveEmptyEntries);

            return words;
        }

        private string TranslateFourDigitNumber(int number)
        {
            string translatedNumber = "{непознато число}";

            if (uniqueNumbers.ContainsKey(number))
            {
                translatedNumber = uniqueNumbers[number];
            }
            else
            {
                int reminder = number % 1000;
                int roundedNumber = (number / 1000) * 1000;

                string thousands = string.Empty;
                if (uniqueNumbers.ContainsKey(roundedNumber))
                {
                    thousands = uniqueNumbers[roundedNumber];
                }
                else
                {
                    thousands = TranslateOneDigitNumber(roundedNumber / 1000) + fourDigitsSuffix;
                }
                
                string conjunction = " ";
                if (reminder % 100 == 0)
                {
                    conjunction = " и ";
                }

                translatedNumber = thousands + conjunction + TranslateThreeDigitNumber(reminder);
            }

            return translatedNumber;
        }
        private string TranslateThreeDigitNumber(int number)
        {
            string translatedNumber = "{непознато число}";

            if(uniqueNumbers.ContainsKey(number))
            {
                translatedNumber = uniqueNumbers[number];
            }
            else
            {
                int reminder = number % 100;
                int roundedNumber = (number / 100) * 100;

                string hundreds = string.Empty;
                if (uniqueNumbers.ContainsKey(roundedNumber))
                {
                    hundreds = uniqueNumbers[roundedNumber];
                }
                else
                {
                    string hundredsSuffix = roundedNumber < 400 ? this.smallThreeDigitsSuffix : this.bigThreeDigitsSuffix;
                    hundreds = TranslateOneDigitNumber(roundedNumber / 100) + hundredsSuffix;
                }

                string conjunction = " ";
                if (reminder % 10 == 0)
                {
                    conjunction = " и ";
                }

                translatedNumber = hundreds + conjunction + TranslateTwoDigitNumber(reminder);
            }

            return translatedNumber;
        }
        private string TranslateTwoDigitNumber(int number)
        {
            string translatedNumber = "{непознато число}";

            if (uniqueNumbers.ContainsKey(number))
            {
                translatedNumber = uniqueNumbers[number];
            }
            else if (number > 12 && number < 20)
            {
                string numberAsString = number.ToString();
                string lastDigit = TranslateOneDigitNumber(int.Parse(numberAsString[1].ToString()));
                translatedNumber = lastDigit + smallTwoDigitsSuffix;
            }
            else if(number > 20)
            {
                if (number % 10 == 0)
                {
                    string numberAsString = number.ToString();
                    string firstDigit = TranslateOneDigitNumber(int.Parse(numberAsString[0].ToString()));
                    translatedNumber = firstDigit + bigTwoDigitsSuffix;
                }
                else
                {
                    int reminder = number % 10;
                    int roundedNumber = (number / 10) * 10;
                    translatedNumber = TranslateTwoDigitNumber(roundedNumber) + " и " + TranslateOneDigitNumber(reminder);
                }
            }

            return translatedNumber;
        }
        private string TranslateOneDigitNumber(int number)
        {
            string translatedNumber = digits[number];

            return translatedNumber;
        }
    }
}
