using MassTransit;
using Message.Publisher.DTO;
using Message.Publisher.Tests.Mocks;
using NSubstitute;
using System;
using System.IO;
using System.Linq;
using Message.Publisher.Services;
using Xunit;
using ServicesAgreement;

namespace Message.Publisher.Tests
{
    public class Tests
    {
        [Fact]
        public async void PublisherTest()
        {
            var bus = Substitute.For<IBus>();
            var prodSrv = new ProductService(bus, new FakeContext());
            await prodSrv.AddProduct(new ProductDto { Name = "Test" });

            var message = bus.ReceivedCalls().First(x => x.GetMethodInfo().Name == "Publish").GetArguments()[0];

            var currentDir = Directory.GetCurrentDirectory();
            var withoutbin = currentDir.Split("bin")[0];
            var destination = withoutbin + "Message.Publisher-Message.Reciever-ProductAdded.json";

            var verifier = new AgreementVerifer()
               .Provider("Message.Publisher")
               .HasAgreementWith("Message.Reciever")
               .WithMessage(message)
               .WithAgreementDestination(destination);

            verifier.Verify();
        }
    }
}
