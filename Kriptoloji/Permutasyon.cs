using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kriptoloji
{
    public class Permutasyon:Ibase
    {
        public string Encrypt(string text, string key)
        {
            int[] order = key.Select(c => int.Parse(c.ToString())).ToArray();
            int len = order.Length;
            string result = "";
            for (int i = 0; i < text.Length; i += len)
            {
                char[] block = new char[len];
                for (int j = 0; j < len; j++)
                {
                    if (i + order[j] < text.Length)
                        block[j] = text[i + order[j]];
                    else
                        block[j] = ' ';
                }
                result += new string(block);
            }
            return result;
        }

        public string Decrypt(string text, string key)
        {
            int[] order = key.Select(c => int.Parse(c.ToString())).ToArray();
            int len = order.Length;
            string result = "";
            for (int i = 0; i < text.Length; i += len)
            {
                char[] block = new char[len];
                for (int j = 0; j < len; j++)
                {
                    if (i + j < text.Length)
                        block[order[j]] = text[i + j];
                }
                result += new string(block);
            }
            return result.Trim();
        }
    }
}

