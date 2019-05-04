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
            var ReadValue = JsonConvert.DeserializeObject<TestData>(json);

            Console.WriteLine($"JSON Serialized:\n  {json}");
            Console.WriteLine($"Deserialized:\n  Code: {ReadValue.TestTarget.Code}\n  Description: {ReadValue.TestTarget.Description}");

            string rawValue = "y";
            var parsedEnum = EnumYesNo.Parse<EnumYesNo>(rawValue);

            Console.WriteLine($"\n\nRaw value:\n  {rawValue}");
            Console.WriteLine($"Parsed enum:\n  Code: {parsedEnum.Code}\n  Description: {parsedEnum.Description}");

            rawValue = "no";
            parsedEnum = EnumYesNo.ParseDescription<EnumYesNo>(rawValue);

            Console.WriteLine($"\n\nRaw value:\n  {rawValue}");
            Console.WriteLine($"Parsed enum:\n  Code: {parsedEnum.Code}\n  Description: {parsedEnum.Description}");

            Console.ReadKey();
        }

        class TestData
        {
            public string SampleData = "Hello World";
            public EnumYesNo TestTarget = EnumYesNo.Undefined;
        }
    }
}
