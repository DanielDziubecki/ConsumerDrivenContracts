using System;
using System.Collections.Generic;

namespace Message.Contracts
{
    public class ProductAdded : IEvent
    {
        public int ProductId { get; set; }
        public List<TestMsg>  Msgs { get; set; }
        public TypeNotExistsingInContract TypeNotExistsingInContract { get; set; }
    }

    public class TestMsg
    {
        public int Type { get; set; }
        public string Type1 { get; set; }
    }

    public class TypeNotExistsingInContract
    {
        public DateTime DateTimeProperty { get; set; }
    }
};
