using System;

namespace Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            IBlahWrapper blahWrapper = new BlahWrapper
            {
                BlahString = "BLAH!"
            };

            Console.WriteLine(blahWrapper.BlahString);
        }

        private interface IBlahWrapper
        {
            string BlahString { get; set; }
            string BlahString2 { get; set; }
        }

        private class BlahWrapper : IBlahWrapper
        {
            private readonly BlahClass _blahClass = new BlahClass();

            public string BlahString
            {
                get => _blahClass.BlahString;
                set => _blahClass.BlahString = value;
            }
            public string BlahString2
            {
                get => _blahClass.BlahString2;
                set => _blahClass.BlahString2 = value;
            }
        }

            private class BlahClass
        {
            public string BlahString;
            public string BlahString2 { get; set; }
        }
    }
}
