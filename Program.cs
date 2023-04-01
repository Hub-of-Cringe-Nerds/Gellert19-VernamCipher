using System.IO;

namespace VernamCipher
{
    class Program
    {
        static Random Cipher = new Random();

        static void Main()
        {
            StreamReader PlainText = new StreamReader("PlainText.txt");
            StreamWriter EncodedText = new StreamWriter("EncodedText.txt", false);
            EncodedText.AutoFlush = true;
            string Message = PlainText.ReadLine()!;
            string Encoded = "";
            int CipherKey = 0;

            for (int i = 0; i < Message.Length; i++)
            {
                CipherKey = (int)Message[i] + GetRandomNumber(0, 256);
                Encoded += ((char)CipherKey);
            }
            
            EncodedText.WriteLine(Encoded);
        }

        private static int GetRandomNumber(int lowerLimitValue, int upperLimitValue)
        {
            return Cipher.Next(lowerLimitValue, upperLimitValue + 1);
        }
        
    }
}