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
        const string encodedImageFile = "Image/EncodedImage.png";
        const string decodedImageFile = "Image/DecodedImage.png";

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
                    case 'Q':
                        return;
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
                inputFile = "Text/";
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
            string inputFile = "Image/", choice = "";
            bool valid = false;

            while (!valid)
            {
                inputFile = "Image/";
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
                        ImageDecode(inputFile, keyImageFile);
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
            var keyedImage = SixLabors.ImageSharp.Image.Load<Rgba32>(inputFile);


            for (int y = 0; y < plainImage.Height; y++)
            {
                for (int x = 0; x < plainImage.Width; x++)
                {
                    int red = GetRandomNumber(0, 256);
                    int green = GetRandomNumber(0, 256);
                    int blue = GetRandomNumber(0, 256);

                    int r = plainImage[x, y].R;
                    int b = plainImage[x, y].B;
                    int g = plainImage[x, y].G;

                    keyedImage[x, y] = Color.FromRgb((byte)red, (byte)green, (byte)blue);
                    plainImage[x, y] = Color.FromRgb((byte)(r ^ red), (byte)(g ^ green), (byte)(b ^ blue));
                    
                }
            }

            keyedImage.SaveAsBmp(keyImageFile);
            plainImage.SaveAsBmp(encodedImageFile);

        }

        static void ImageDecode(string inputFile, string keyImageFile)
        {
            var encodedImage = SixLabors.ImageSharp.Image.Load<Rgba32>(inputFile);
            var keyImage = SixLabors.ImageSharp.Image.Load<Rgba32>(keyImageFile);
            var keyIndex = 0;

            for (int y = 0; y < encodedImage.Height; y++)
            {
                for (int x = 0; x < encodedImage.Width; x++)
                {
                    int r = encodedImage[x, y].R;
                    int g = encodedImage[x, y].G;
                    int b = encodedImage[x, y].B;

                    int red = keyImage[x, y].R;
                    int green = keyImage[x, y].G;
                    int blue = keyImage[x, y].B;

                    encodedImage[x, y] = Color.FromRgb((byte)(r ^ red), (byte)(g ^ green), (byte)(b ^ blue));

                    keyIndex++;
                }
            }
            
            encodedImage.SaveAsBmp(decodedImageFile);
        }

        private static int GetRandomNumber(int lowerLimitValue, int upperLimitValue)
        {
            return Cipher.Next(lowerLimitValue, upperLimitValue);
        }
    }
}