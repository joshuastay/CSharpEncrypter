using System;
using System.Collections.Generic;
using System.Text;


namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string hello = "This is my test string to encode!";

            Random generate = new Random();

            Encoder encode = new Encoder();

            //encode.testEncoder();

            string testEncode = encode.encryptString(hello);
            Console.WriteLine(hello);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(testEncode);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(encode.decryptString(testEncode));
        }
        class Encoder
        {
            private char[] characterSet = new char[256];
            private int index;
            private Random generator = new Random();
            private string result;
            private Dictionary<char, string> cipher = new Dictionary<char, string>();
            private Dictionary<string, char> reversecipher = new Dictionary<string, char>();
            private char serparator;
            private string encrypted;
            private int keyLength;
            private string decrypted;

            // Encoder constructor
            // initializes the Ecoder object storing 255 characters into an array
            // also creates dictionary to reference each character to a new value
            public Encoder()
            {
                for (int i = 0; i <= 255; i++)
                {
                    char c = Convert.ToChar(i);
                    characterSet[i] = c;
                }
                serparator = characterSet[generator.Next(255)];
                foreach (char i in characterSet)
                {
                    if (!char.IsControl(i))
                    {
                        var tempkey = this.keyGenerator();
                        cipher.Add(i, tempkey);
                        reversecipher.Add(tempkey, i);
                    }
                }
            }
            public void testEncoder()
            {
                Console.WriteLine(serparator);
                foreach (KeyValuePair<char, string> n in cipher)
                {
                    Console.WriteLine(n);
                }
            }
            // keyGenerator method returns a string to act as a key
            public string keyGenerator()
            {
                // initializes result variable to produce a unique result
                result = "";
                // sets a length to produce encyrption key
                keyLength = generator.Next(128, 255);
                // continues adding random characters unitl keylength is met
                while (result.Length != keyLength)
                {
                    index = generator.Next(255);
                    // ensures the separator is not used
                    if (characterSet[index] != serparator)
                    {
                        result += Convert.ToString(characterSet[index]);
                        continue;
                    }
                    else
                    {
                        continue;
                    }
                }
                return result;
            }
            public string encryptString(string textToHide)
            {
                encrypted = "";
                foreach (char item in textToHide)
                {
                    encrypted += cipher.GetValueOrDefault(item) + serparator;
                }
                return encrypted;
            }
            public string decryptString(string textToReveal)
            {
                decrypted = "";

                string[] tempArray = textToReveal.Split(serparator);
                foreach (string n in tempArray)
                {
                    decrypted += reversecipher.GetValueOrDefault(n);
                }
                return decrypted;
            }
        }
    }
}
