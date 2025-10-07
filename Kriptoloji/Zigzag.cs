using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kriptoloji
{
    public class Zigzag:Ibase
    {
        public string Encrypt(string text, string key)
        {
            int rails = int.Parse(key);
            string[] fence = new string[rails];
            int rail = 0, dir = 1;

            foreach (char c in text)
            {
                fence[rail] += c;
                rail += dir;
                if (rail == 0 || rail == rails - 1) dir *= -1;
            }

            return string.Join("", fence);
        }

        public string Decrypt(string text, string key)
        {
            int rails = int.Parse(key);
            int[] len = new int[rails];
            int rail = 0, dir = 1;
            for (int i = 0; i < text.Length; i++)
            {
                len[rail]++;
                rail += dir;
                if (rail == 0 || rail == rails - 1) dir *= -1;
            }

            string[] fence = new string[rails];
            int index = 0;
            for (int i = 0; i < rails; i++)
            {
                fence[i] = text.Substring(index, len[i]);
                index += len[i];
            }

            string result = "";
            rail = 0; dir = 1;
            int[] pos = new int[rails];
            for (int i = 0; i < text.Length; i++)
            {
                result += fence[rail][pos[rail]++];
                rail += dir;
                if (rail == 0 || rail == rails - 1) dir *= -1;
            }
            return result;
        }
    }
}

