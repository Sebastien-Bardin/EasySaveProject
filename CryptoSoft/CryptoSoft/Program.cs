using System;

namespace CryptoSoft
{
    class Program
    {
        static void Main(string[] args)
        {
            Xor xor = new Xor("123456");
            xor.faire_XOR(args.GetValue(0).ToString());
           

        }
    }
}
