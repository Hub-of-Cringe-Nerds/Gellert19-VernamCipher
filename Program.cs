using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace VernamCipher
{
    class Program
    {
        static Random Cipher = new Random();
        const string keyTextFile = "Text/Key.txt";
        const string encodedTextFile = "Text/EncodedText.txt";
        const string decodedTextFile = "Text/DecodedText.txt";
        const string keyImageFile = "Image/Key.png";
        const string encodedImageFile = "Image/EncodedText.png";
        const string decodedImageFile = "Image/DecodedText.png";

        static void Main()
        {
            string choice = "";
            bool valid = false;

            Console.WriteLine("If you wish to return to this choice enter (Q) at any time.");

            while (!valid)
            {
                Console.Write("Do you wish to encode a Text file (T) or an Image file (I): ");
                choice = Console.ReadLine()!;
                choice = choice.ToUpper();

                switch (choice[0])
                {
                    case 'T':
                        Text();
                        break;
                    case 'I':
                        Image();
                        break;
                    default:
                        Console.WriteLine("Not a valid choice.");
                        break;
                }
            }
        }

        static void Text()
        {
            string inputFile = "Text/", choice = "", plainFile = "";
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
                        inputFile += (Console.ReadLine()!) + ".txt";
                        TextEncode(inputFile);
                        break;
                    case 'D':
                        Console.Write("Please enter the name of the file that you wish to decode: ");
                        inputFile += (Console.ReadLine()!) + ".txt";
                        plainFile = inputFile;
                        TextDecode(inputFile, plainFile);
                        break;
                    case 'Q':
                        return;
                    default:
                        Console.WriteLine("Not a valid choice.");
                        break;
                }
            }
        }

        private static void TextEncode(string inputFile)
        {
            StreamReader plainText = new StreamReader(inputFile);
            StreamWriter encodedText = new StreamWriter(encodedTextFile);
            StreamWriter key = new StreamWriter(keyTextFile);
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

        private static void TextDecode(string inputFile, string plainFile)
        {
            StreamReader encodedText = new StreamReader(inputFile);
            StreamReader keyText = new StreamReader(keyTextFile);
            StreamReader plainText = new StreamReader(plainFile);
            StreamWriter decodedText = new StreamWriter(decodedTextFile);
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

        static void Image()
        {
            string inputFile = "Image/", choice = "", plainFile = "";
            bool valid = false;

            while (!valid)
            {
                Console.Write("Do you wish to encode (E) a image file or decode (D): ");
                choice = Console.ReadLine()!;
                choice = choice.ToUpper();

                switch (choice[0])
                {
                    case 'E':
                        Console.Write("Please enter the name of the file that you wish to encode: ");
                        inputFile += (Console.ReadLine()!) + ".png";
                        ImageEncode(inputFile);
                        break;
                    case 'D':
                        Console.Write("Please enter the name of the file that you wish to decode: ");
                        inputFile += (Console.ReadLine()!) + ".png";
                        plainFile = inputFile;
                        ImageDecode(inputFile, plainFile);
                        break;
                    case 'Q':
                        return;
                    default:
                        Console.WriteLine("Not a valid choice.");
                        break;
                }
            }
        }

        static void ImageEncode(string inputFile)
        {
            var plainImage = SixLabors.ImageSharp.Image.Load<Rgba32>(inputFile);
            var encodedImage = SixLabors.ImageSharp.Image.Load<Rgba32>(inputFile);


        }

        static void ImageDecode(string inputFile, string plainFile)
        {

        }

        private static int GetRandomNumber(int lowerLimitValue, int upperLimitValue)
        {
            return Cipher.Next(lowerLimitValue, upperLimitValue);
        }
    }
}