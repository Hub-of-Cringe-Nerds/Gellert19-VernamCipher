using System.IO;

namespace VernamCipher
{
    class Program
    {
        static Random Cipher = new Random();

        static void Main()
        {
            string textfile = "", choice = "";
            bool valid = false;

            while (!valid)
            {
                Console.Write("Do you wish to encode (E) a text file or decode (D): ");
                choice = Console.ReadLine()!;
                choice = choice.ToUpper();

                if (choice[0] == 'E')
                {
                    Console.Write("Please enter the name of the text file that you wish to encode: ");
                    textfile = Console.ReadLine()!;

                    Encode(textfile);
                }
                else if (choice[0] == 'D')
                {
                    Console.Write("Please enter the name of the text file that you wish to decode: ");
                    textfile = Console.ReadLine()!;
                }
                else
                {
                    Console.WriteLine("Not a valid choice.");
                }
            }

        }

        private static void Encode(string textfile)
        {
            StreamReader PlainText = new StreamReader(textfile);
            StreamWriter EncodedText = new StreamWriter("EncodedText.txt", false);
            EncodedText.AutoFlush = true;

            string Message = PlainText.ReadToEnd();
            string Encoded = "";
            int CipherKey = 0;

            for (int i = 0; i < Message.Length; i++)
            {
                CipherKey = (int)Message[i] + GetRandomNumber(0, 256);
                Encoded += ((char)CipherKey);
            }

            EncodedText.WriteLine(Encoded);
        }

        private static void Decode(string textfile)
        {
            StreamReader PlainText = new StreamReader(textfile);
        }

        private static int GetRandomNumber(int lowerLimitValue, int upperLimitValue)
        {
            return Cipher.Next(lowerLimitValue, upperLimitValue + 1);
        }
        
    }
}