using System.IO;

namespace VernamCipher
{
    class Program
    {
        static Random Cipher = new Random();
        
        static void Main()
        {
            StreamReader PlainText = new StreamReader("Plain.txt");


        }

        private static int GetRandomNumber(int lowerLimitValue, int upperLimitValue)
        {
            return Cipher.Next(lowerLimitValue, upperLimitValue + 1);
        }
        
    }
}