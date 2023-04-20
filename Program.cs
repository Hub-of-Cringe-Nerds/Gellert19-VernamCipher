using System.IO;

namespace VernamCipher
{
    class Program
    {
        static Random Cipher = new Random();

        static void Main()
        {
            string inputFile = "", choice = "", keyFile = "Key.txt", plainFile = "PlainText.txt";
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
                        Decode(inputFile, keyFile, plainFile);
                        break;
                    case 'B':
                        Bean();
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
            encodedText.AutoFlush = true;
            key.AutoFlush = true;

            string message = plainText.ReadToEnd();
            string encoded = "";
            string cipherKey = "";
            int rnd = 0;

            for (int i = 0; i < message.Length; i++)
            {
                rnd = GetRandomNumber(0, 256);
                cipherKey += (char)rnd;
                encoded += (char)((int)message[i] ^ rnd);
            }
            key.Write(cipherKey);
            key.Close();

            encodedText.WriteLine(encoded);
            encodedText.Close();
        }

        private static void Decode(string textFile, string keyFile, string plainFile)
        {
            StreamReader encodedText = new StreamReader(textFile);
            StreamReader keyText = new StreamReader(keyFile);
            StreamReader plainText = new StreamReader(plainFile);
            StreamWriter decodedText = new StreamWriter("DecodedText.txt");
            decodedText.AutoFlush = true;

            string encoded = encodedText.ReadToEnd();
            string key = keyText.ReadToEnd();
            string message = plainText.ReadToEnd();
            string decoded = "";

            for (int i = 0; i < message.Length; i++)
            {
                decoded += (char)((int)encoded[i] ^ (int)key[i]);
            }

            decodedText.WriteLine(decoded);
            decodedText.Close();
        }

        private static void Bean()
        {
            Random Rnd = new Random();
            StreamReader inputfile = new StreamReader("PlainText.txt");
            StreamWriter cipherfile = new StreamWriter("cipher.txt");
            StreamWriter keyfile = new StreamWriter("key.txt");
            string message = inputfile.ReadToEnd();
            string encoded = "";
            string key = "";
            string decoded = "";
            int randomthing;
            int code = 0;
            for (int i = 0; i < message.Length; i++)
            {
                randomthing = Rnd.Next(0, 256);
                key += (char)randomthing;
                code = (int)message[i] ^ randomthing;
                encoded += ((char)code);
            }
            keyfile.Write(key);
            keyfile.Flush();
            keyfile.Close();
            cipherfile.Write(encoded);
            cipherfile.Flush();
            cipherfile.Close();
            Console.WriteLine("The original message: {0}", message);
            Console.WriteLine("The message after encryption: {0}", encoded);
            Console.WriteLine("The key used: {0}", key);
            for (int i = 0; i < message.Length; i++)
            {
                decoded += (char)((int)encoded[i] ^ (int)key[i]);
            }
            Console.WriteLine("The encrypted text decypted using the saved key: {0}", decoded);
        }

        private static int GetRandomNumber(int lowerLimitValue, int upperLimitValue)
        {
            return Cipher.Next(lowerLimitValue, upperLimitValue);
        }

    }
}