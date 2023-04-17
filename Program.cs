using System.IO;

namespace VernamCipher
{
    class Program
    {
        static Random Cipher = new Random();

        static void Main()
        {
            string inputFile = "", choice = "";
            bool valid = false;

            while (!valid)
            {
                Console.Write("Do you wish to encode (E) a text file or decode (D): ");
                choice = Console.ReadLine()!;
                choice = choice.ToUpper();

                switch (choice[0])
                {
                    case 'E':
                        Console.Write("Please enter the name of the file that you wish to encode: ");
                        inputFile = Console.ReadLine()!;
                        Encode(inputFile);
                        break;
                    case 'D':
                        Console.Write("Please enter the name of the file that you wish to decode: ");
                        inputFile = Console.ReadLine()!;
                        break;
                    default:
                        Console.WriteLine("Not a valid choice.");
                        break;
                }
            }

        }

        private static void Encode(string textfile)
        {
            StreamReader plainText = new StreamReader(textfile);
            StreamWriter encodedText = new StreamWriter("EncodedText.txt");
            StreamWriter key = new StreamWriter("Key.txt");
            key.AutoFlush = true;
            encodedText.AutoFlush = true;

            string message = plainText.ReadToEnd();
            string encoded = "";
            char cipherKey = '.';

            for (int i = 0; i < message.Length; i++)
            {
                cipherKey = (char)(message[i] + GetRandomNumber(0, 256));
                key.Write(cipherKey);
                encoded += (cipherKey);
            }

            encodedText.WriteLine(encoded);
        }

        private static void Decode(string textFile, string keyFile)
        {
            StreamReader encodedText = new StreamReader(textFile);
            StreamReader keyText = new StreamReader(keyFile);
            StreamWriter decodedText = new StreamWriter("DecodedText.txt");
            decodedText.AutoFlush = true;

            string encoded = encodedText.ReadToEnd();
            string key = keyText.ReadToEnd();
            string decoded = "";

            for (int i = 0; i < encoded.Length; i++)
            {
                decoded += (encoded[i] - key[i]);
            }
        }

        private static int GetRandomNumber(int lowerLimitValue, int upperLimitValue)
        {
            return Cipher.Next(lowerLimitValue, upperLimitValue + 1);
        }

    }
}