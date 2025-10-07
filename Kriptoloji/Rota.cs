using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kriptoloji
{
    public class Rota:Ibase
    {
        public string Encrypt(string text, string key)
        {
            int k = int.Parse(key);
            int rows = (int)Math.Ceiling((double)text.Length / k);
            char[,] matrix = new char[rows, k];
            for (int i = 0; i < text.Length; i++)
                matrix[i / k, i % k] = text[i];

            string result = "";
            for (int j = 0; j < k; j++)
                for (int i = 0; i < rows; i++)
                    result += matrix[i, j];
            return result;
        }

        public string Decrypt(string text, string key)
        {
            int k = int.Parse(key);
            int rows = (int)Math.Ceiling((double)text.Length / k);
            char[,] matrix = new char[rows, k];
            int index = 0;
            for (int j = 0; j < k; j++)
                for (int i = 0; i < rows; i++)
                    if (index < text.Length)
                        matrix[i, j] = text[index++];

            string result = "";
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < k; j++)
                    result += matrix[i, j];
            return result;
        }
    }
}

