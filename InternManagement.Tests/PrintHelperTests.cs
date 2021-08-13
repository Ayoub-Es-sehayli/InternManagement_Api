using InternManagement.Api.Enums;
using InternManagement.Api.Helpers;
using Xunit;

namespace InternManagement.Tests
{
    public class PrintHelperTests
    {

        [Fact]
        public void PrintDecision_ReturnsString()
        {
            eGender gender = eGender.Female;
            PrintHelper print = new PrintHelper();
            if (gender == eGender.Female)
            {
                string result = print.PrintDecision(gender);
                Assert.NotNull(result);
            }
            else
            {
                string result = print.PrintDecision(gender);
                Assert.NotNull(result);
            }

        }
        [Fact]
        public void PrintCancel_ReturnsString()
        {
            eGender gender = eGender.Female;
            PrintHelper print = new PrintHelper();
            if (gender == eGender.Female)
            {
                string result = print.PrintCancel(gender);
                Assert.NotNull(result);
            }
            else
            {
                string result = print.PrintCancel(gender);
                Assert.NotNull(result);
            }
        }
        [Fact]
        public void PrintCertificate_ReturnsString()
        {
            eGender gender = eGender.Female;
            PrintHelper print = new PrintHelper();
            if (gender == eGender.Female)
            {
                string result = print.PrintCertificate(gender);
                Assert.NotNull(result);
            }
            else
            {
                string result = print.PrintCertificate(gender);
                Assert.NotNull(result);
            }
        }
    }
}
