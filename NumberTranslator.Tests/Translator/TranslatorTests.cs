using Xunit;
using Xunit.Abstractions;

namespace NumberTranslator.Translator.Tests
{
    public class TranslatorTests
    {
        private readonly ITestOutputHelper outputHelper;

        public TranslatorTests(ITestOutputHelper outputHelper)
        {
            this.outputHelper = outputHelper;
        }

        [Theory]
        [InlineData("0", "нула")]
        [InlineData("1", "едно")]
        [InlineData("2", "две")]
        [InlineData("5", "пет")]
        [InlineData("8", "осем")]
        [InlineData("10", "десет")]
        [InlineData("11", "единадесет")]
        [InlineData("12", "дванадесет")]
        [InlineData("13", "тринадесет")]
        [InlineData("16", "шестнадесет")]
        [InlineData("19", "деветнадесет")]
        [InlineData("20", "двадесет")]
        [InlineData("21", "двадесет и едно")]
        [InlineData("33", "тридесет и три")]
        [InlineData("48", "четиридесет и осем")]
        [InlineData("50", "петдесет")]
        [InlineData("99", "деветдесет и девет")]
        [InlineData("100", "сто")]
        [InlineData("101", "сто и едно")]
        [InlineData("102", "сто и две")]
        [InlineData("110", "сто и десет")]
        [InlineData("120", "сто и двадесет")]
        [InlineData("137", "сто тридесет и седем")]
        [InlineData("198", "сто деветдесет и осем")]
        [InlineData("215", "двеста и петнадесет")]
        [InlineData("354", "триста петдесет и четири")]
        [InlineData("667", "шестстотин шестдесет и седем")]
        [InlineData("700", "седемстотин")]
        [InlineData("904", "деветстотин и четири")]
        [InlineData("1000", "хиляда")]
        [InlineData("1001", "хиляда и едно")]
        [InlineData("1200", "хиляда и двеста")]
        [InlineData("1300", "хиляда и триста")]
        [InlineData("2408", "две хиляди четиристотин и осем")]
        [InlineData("3732", "три хиляди седемстотин тридесет и две")]
        [InlineData("4018", "четири хиляди и осемнадесет")]
        [InlineData("4071", "четири хиляди седемдесет и едно")]
        [InlineData("6020", "шест хиляди и двадесет")]
        [InlineData("8080", "осем хиляди и осемдесет")]
        [InlineData("10000", "десет хиляди")]
        [InlineData("11001", "единадесет хиляди и едно")]
        [InlineData("12100", "дванадесет хиляди и сто")]
        [InlineData("13105", "тринадесет хиляди сто и пет")]
        [InlineData("20311", "двадесет хиляди триста и единадесет")]
        [InlineData("25513", "двадесет и пет хиляди петстотин и тринадесет")]
        [InlineData("100000", "сто хиляди")]
        [InlineData("200002", "двеста хиляди и две")]
        [InlineData("305122", "триста и пет хиляди сто двадесет и две")]
        public void TryTranslateNumber_tests_NaturalNumbers(string number, string expectedResult)
        {
            //Arange
            var translator = new NumberTranlsator.Core.Translator();

            //Act
            string translatedNumber = translator.TryTranslateNumber(number);

            //Assert
            Assert.Equal(expectedResult, translatedNumber);

            this.outputHelper.WriteLine("expected: " + expectedResult);
            this.outputHelper.WriteLine("actual: " + translatedNumber);
        }
    }
}
