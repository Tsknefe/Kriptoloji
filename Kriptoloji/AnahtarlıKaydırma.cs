using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kriptoloji
{
    public class AnahtarlıKaydırma:Ibase
    {
        public string Encrypt(string text, string key)
        {
            int[] order = key.Select((c, i) => new { c, i })
                              .OrderBy(x => x.c)
                              .Select(x => x.i).ToArray();
            int len = order.Length;
            int rows = (int)Math.Ceiling((double)text.Length / len);
            char[,] matrix = new char[rows, len];
            int index = 0;
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < len; j++)
                    matrix[i, j] = index < text.Length ? text[index++] : ' ';

            string result = "";
            foreach (int col in order)
                for (int i = 0; i < rows; i++)
                    result += matrix[i, col];
            return result;
        }

        public string Decrypt(string text, string key)
        {
            int[] order = key.Select((c, i) => new { c, i })
                              .OrderBy(x => x.c)
                              .Select(x => x.i).ToArray();
            int len = order.Length;
            int rows = (int)Math.Ceiling((double)text.Length / len);
            char[,] matrix = new char[rows, len];
            int index = 0;

            foreach (int col in order)
                for (int i = 0; i < rows; i++)
                    matrix[i, col] = index < text.Length ? text[index++] : ' ';

            string result = "";
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < len; j++)
                    result += matrix[i, j];
            return result.Trim();
        }
    }
}
