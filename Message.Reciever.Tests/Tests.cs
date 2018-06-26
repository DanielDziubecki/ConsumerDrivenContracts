using Message.Contracts;
using ServicesAgreement;
using System;
using System.Collections.Generic;
using Xunit;

namespace Message.Reciever.Tests
{
    public class Tests
    {

        [Fact]
        public void ReceiveTest()
        {
            var message = new ProductAdded
            {
                ProductId = 5,
                Msgs = new List<TestMsg>()
                {
                    new TestMsg()
                    {
                        Type = Int32.MaxValue,
                        Type1 =  "asas"
                    },
                    new TestMsg()
                    {
                        Type = Int32.MinValue,
                        Type1 = "asdasd"
                    }
                }
            };

            var builder = new AgreementBuilder();
            builder.Consumer("Message.Reciever")
               .HasAgreementWith("Message.Publisher")
               .ExpectsMessage(message)
               .Build();
        }
    }
}
