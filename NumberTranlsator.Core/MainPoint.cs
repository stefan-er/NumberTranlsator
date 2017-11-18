using System;
using System.Collections.Generic;

namespace NumberTranlsator.Core
{
    class MainPoint
    {
        static void Main(string[] args)
        {
            var translator = new Translator();

            IEnumerable<string> words = translator.GetWordsFromTexFile("Files\\SomeFile.txt");

            var textBuilder = new List<string>();

            foreach (var word in words)
            {
                string translatedWord = translator.TryTranslateNumber(word);

                textBuilder.Add(translatedWord);
            }

            Console.WriteLine(string.Join(' ', textBuilder));
        }
    }
}
