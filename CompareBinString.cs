using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YandexTest
{
    internal class Class1
    {
        static void Main(string[] args)
        {
            string a, b;

            string[] array = File.ReadAllText("input.txt").Split('\n');

            a = GetBytes(array[0]);
            b = GetBytes(array[1]);

            int lengthBits = a.Length > b.Length ? a.Length : b.Length;
            AddStartZeros(ref a, lengthBits - a.Length);
            AddStartZeros(ref b, lengthBits - b.Length);

            char result = (char)61;
            for (int i = 0; i < lengthBits; i++)
            {
                if (a[i] > b[i])
                {
                    result = (char)62;
                    break;
                }
                else if (a[i] < b[i])
                {
                    result = (char)60;
                    break;
                }
            }

            File.WriteAllText("output.txt", result.ToString());
        }

        static void AddStartZeros(ref string bits, int size)
        {
            for (int i = 0; i < size; i++)
            {
                bits = "0" + bits;
            }
        }

        static string GetBytes(string text)
        {
            string[] numbers = { "zero", "one" };
            string buffer = text;
            string result = "";
            for (int i = 0; i < numbers.Length;)
            {
                if (buffer.StartsWith(numbers[i]))
                {
                    buffer = buffer.Substring(numbers[i].Length);
                    result += i;
                    i = 0;
                }
                else
                    i++;
            }
            return result;
        }

    }
}
