using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kriptoloji
{
    public class Kaydırmalı : Ibase
    {
        private const string alphabet = "ABCÇDEFGĞHIİJKLMNOÖPRSŞTUÜVYZ";

        public string Encrypt(string input, string key)
        {
            if (!int.TryParse(key, out int shift))
                throw new ArgumentException("Caesar algoritması için anahtar bir sayı olmalıdır.");

            input = input.ToUpper();
            var sb = new StringBuilder();

            foreach (char c in input)
            {
                int index = alphabet.IndexOf(c);
                if (index == -1)
                {
                    sb.Append(c); // harf değilse direkt ekle
                }
                else
                {
                    int newIndex = (index + shift) % alphabet.Length;
                    sb.Append(alphabet[newIndex]);
                }
            }

            return sb.ToString();
        }

        public string Decrypt(string input, string key)
        {
            if (!int.TryParse(key, out int shift))
                throw new ArgumentException("Caesar algoritması için anahtar bir sayı olmalıdır.");

            input = input.ToUpper();
            var sb = new StringBuilder();

            foreach (char c in input)
            {
                int index = alphabet.IndexOf(c);
                if (index == -1)
                {
                    sb.Append(c);
                }
                else
                {
                    int newIndex = (index - shift + alphabet.Length) % alphabet.Length;
                    sb.Append(alphabet[newIndex]);
                }
            }

            return sb.ToString();
        }
    }
}