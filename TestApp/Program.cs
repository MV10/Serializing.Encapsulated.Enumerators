using EnumerationLibrary;
using Newtonsoft.Json;
using System;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var WriteValue = new TestData() { TestTarget = EnumYesNo.Yes };
            string json = JsonConvert.SerializeObject(WriteValue);
            Console.WriteLine($"Serialized:\n\n{json}\n\n");
            var ReadValue = JsonConvert.DeserializeObject<TestData>(json);
            Console.WriteLine($"Deserialized:\n  Code: {ReadValue.TestTarget.Code}\n  Description: {ReadValue.TestTarget.Description}");
            Console.ReadKey();
        }

        class TestData
        {
            public string SampleData = "Hello World";
            public EnumYesNo TestTarget = EnumYesNo.Undefined;
        }
    }
}
