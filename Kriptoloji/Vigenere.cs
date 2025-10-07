using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kriptoloji
{
    public class Vigenere:Ibase
    {
        public string Encrypt(string text, string key=null)
        {
            string result = "";
            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                char k = key[i % key.Length];
                if (char.IsLetter(c))
                {
                    char baseChar = char.IsUpper(c) ? 'A' : 'a';
                    char baseKey = char.IsUpper(k) ? 'A' : 'a';
                    result += (char)(((c - baseChar + (k - baseKey)) % 26) + baseChar);
                }
                else
                    result += c;
            }
            return result;
        }

        public string Decrypt(string text, string key=null)
        {
            string result = "";
            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                char k = key[i % key.Length];
                if (char.IsLetter(c))
                {
                    char baseChar = char.IsUpper(c) ? 'A' : 'a';
                    char baseKey = char.IsUpper(k) ? 'A' : 'a';
                    result += (char)(((c - baseChar - (k - baseKey) + 26) % 26) + baseChar);
                }
                else
                    result += c;
            }
            return result;
        }
    }
}
