using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kriptoloji
{
    public class Doğrusal:Ibase
    {
        private int ModInverse(int a, int m)
        {
            for (int i = 0; i < m; i++)
                if ((a * i) % m == 1)
                    return i;
            throw new Exception("Modular inverse does not exist");
        }

        public string Encrypt(string text, string key=null)
        {
            var parts = key.Split(',');
            int a = int.Parse(parts[0]);
            int b = int.Parse(parts[1]);
            string result = "";
            foreach (char c in text)
            {
                if (char.IsLetter(c))
                {
                    char baseChar = char.IsUpper(c) ? 'A' : 'a';
                    result += (char)(((a * (c - baseChar) + b) % 26) + baseChar);
                }
                else
                    result += c;
            }
            return result;
        }
        public string Decrypt(string text, string key = null)
        {
            string[] parts = key?.Split(',') ?? new string[] { "5", "8" };
            int a = int.Parse(parts[0]);
            int b = int.Parse(parts[1]);
            int a_inv = ModInverse(a, 26);
            string result = "";
            foreach (char c in text.ToLower())
            {
                if (char.IsLetter(c))
                    result += (char)((((a_inv * ((c - 'a') - b + 26)) % 26) + 'a'));
                else result += c;
            }
            return result;
        }
    }
}
