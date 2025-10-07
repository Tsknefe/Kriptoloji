using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kriptoloji
{
    public class Yerdegistirme:Ibase
    {
        public string Encrypt(string text, string key)
        {
            int k = int.Parse(key);
            string result = "";
            for (int i = 0; i < k; i++)
                for (int j = i; j < text.Length; j += k)
                    result += text[j];
            return result;
        }

        public string Decrypt(string text, string key)
        {
            int k = int.Parse(key);
            int rows = (int)Math.Ceiling((double)text.Length / k);
            char[] result = new char[text.Length];
            int index = 0;

            for (int i = 0; i < k; i++)
                for (int j = 0; j < rows; j++)
                {
                    int pos = j * k + i;
                    if (pos < text.Length)
                        result[pos] = text[index++];
                }

            return new string(result);
        }
    }
}
