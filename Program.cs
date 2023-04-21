﻿using System.IO;

namespace VernamCipher
{
    class Program
    {
        static Random Cipher = new Random();
        static string keyFile = "Key.txt";
        static string encodedFile = "EncodedText.txt";
        static string decodedFile = "DecodedText.txt";

        static void Main()
        {
            string inputFile = "", choice = "", plainFile = "";
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
                        plainFile = inputFile;
                        Decode(inputFile, plainFile);
                        break;
                    default:
                        Console.WriteLine("Not a valid choice.");
                        break;
                }
            }
        }

        private static void Encode(string inputFile)
        {
            StreamReader plainText = new StreamReader(inputFile);
            StreamWriter encodedText = new StreamWriter(encodedFile);
            StreamWriter key = new StreamWriter(keyFile);
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

        private static void Decode(string inputFile, string plainFile)
        {
            StreamReader encodedText = new StreamReader(inputFile);
            StreamReader keyText = new StreamReader(keyFile);
            StreamReader plainText = new StreamReader(plainFile);
            StreamWriter decodedText = new StreamWriter(decodedFile);
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

        private static int GetRandomNumber(int lowerLimitValue, int upperLimitValue)
        {
            return Cipher.Next(lowerLimitValue, upperLimitValue);
        }
    }
}