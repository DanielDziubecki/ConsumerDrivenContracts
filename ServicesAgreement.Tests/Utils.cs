using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit.Abstractions;

namespace ServicesAgreement.Tests
{
    public static class Utils
    {
        public static void ShouldContainEquivalentTo<T>(this IEnumerable<T> subject, object expectation, ITestOutputHelper helper)
        {
            var containElements = 0;
            var serializedSubject = JsonConvert.SerializeObject(subject);
            var serializedExpectation = JsonConvert.SerializeObject(expectation);

            foreach (var item in subject)
            {
                try
                {
                    item.Should().BeEquivalentTo(expectation);
                    containElements++;
                }
                catch
                {
                    //ignore
                }
            }
            try
            {
                containElements.Should().BeGreaterThan(0);
                helper.WriteLine("Subject:{0}\r\n Expectation:{1}", serializedSubject, serializedExpectation);
            }
            catch
            {
                helper.WriteLine("Subject:{0}\r\n Expectation:{1}", serializedSubject, serializedExpectation);
                throw;
            }
        }

        public static IAgreementVerifer GetVerifier(string destination, object message)
        {
            return new AgreementVerifer()
                .Provider("TestProvider")
                .HasAgreementWith("TestConsumer")
                .WithMessage(message)
                .WithAgreementDestination(destination);
        }


        public static string GetApplicationRoot()
        {
            var exePath = Path.GetDirectoryName(System.Reflection
                              .Assembly.GetExecutingAssembly().CodeBase);
            Regex appPathMatcher = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(?=\\+bin)");
            var appRoot = appPathMatcher.Match(exePath).Value;
            return appRoot;
        }
    }
}
