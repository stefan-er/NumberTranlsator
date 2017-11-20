using System;
using System.Collections.Generic;

namespace NumberTranlsator.Core
{
    class MainPoint
    {
        static void Main(string[] args)
        {
            var textManipulator = new TextManipulator("Files\\SomeFile.txt");
            IEnumerable<string> textWords = textManipulator.GetWordsFromTex();

            var translatedWords = new List<string>();
            var translator = new Translator();

            foreach (var word in textWords)
            {
                string translatedWord = translator.TryTranslateNumber(word);
                translatedWords.Add(translatedWord);
            }

            string translatedText = textManipulator.GetTextFromWords(translatedWords);

            bool translatedCorrectly = translatedText == "Днес на пътя попаднаха сто двадесет и три коли от които двадесет бяха сини десет лилави единадесет зелени пет черни двадесет и едно кафяви тридесет и пет бели и останалите шарени хиляда нови коли дойдоха а после още две хиляди и сто а после още три хиляди двеста и двадесет и още пет хиляди осемстотин и четиридесет";

            Console.WriteLine(translatedText);
        }
    }
}
