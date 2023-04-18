using System.IO;

namespace VernamCipher
{
    class Program
    {
        static Random Cipher = new Random();

        static void Main()
        {
            string inputFile = "", choice = "", keyFile = "Key.txt";
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
                        inputFile = (Console.ReadLine()!) + ".txt";
                        Encode(inputFile);
                        break;
                    case 'D':
                        Console.Write("Please enter the name of the file that you wish to decode: ");
                        inputFile = (Console.ReadLine()!) + ".txt";
                        Decode(inputFile, keyFile);
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
            string cipherKey = "";

            for (int i = 0; i < message.Length; i++)
            {
                cipherKey += (char)GetRandomNumber(0, 256);
                key.Write(cipherKey);
                encoded += (char)((int)cipherKey[i] + (int)(message[i]));
            }
            key.Close();

            encodedText.WriteLine(encoded);
            encodedText.Close();
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

            for (int i = 0; i < encoded.Length - 1; i++)
            {
                decoded += (char)((int)encoded[i] - (int)key[i]);
            }

            decodedText.WriteLine(decoded);
            decodedText.Close();
        }

        private static int GetRandomNumber(int lowerLimitValue, int upperLimitValue)
        {
            return Cipher.Next(lowerLimitValue, upperLimitValue + 1);
        }

    }
}