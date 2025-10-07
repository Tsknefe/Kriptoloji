using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kriptoloji
{
    public class _4Kare : Ibase
    {
        private const string alphabet = "ABCÇDEFGĞHIİJKLMNOÖPRSŞTUÜVYZX"; // 30 harf
        private char[,] matrix1, matrix2, matrix3, matrix4;
        private string anahtar1, anahtar2;

        private char[,] CreateMatrix(string key)
        {
            char[] fullKey = key.ToUpper().Distinct().ToArray();
            List<char> filled = new List<char>(fullKey);
            foreach (char c in alphabet)
                if (!filled.Contains(c))
                    filled.Add(c);

            char[,] matrix = new char[6, 5];
            for (int i = 0; i < 30; i++)
                matrix[i / 5, i % 5] = filled[i];

            return matrix;
        }

        private string CreateRandomKey()
        {
            Random rnd = new Random();
            return new string(alphabet.OrderBy(c => rnd.Next()).Take(15).ToArray());
        }

        private void FindPosition(char[,] matrix, char c, out int row, out int col)
        {
            for (int i = 0; i < 6; i++)
                for (int j = 0; j < 5; j++)
                    if (matrix[i, j] == c)
                    {
                        row = i;
                        col = j;
                        return;
                    }
            row = col = -1;
        }

        public string Encrypt(string input, string notUsedKey = "")
        {
            anahtar1 = CreateRandomKey(); // sağ üst
            anahtar2 = CreateRandomKey(); // sol alt

            matrix1 = CreateMatrix("");          // sol üst (alfabe)
            matrix2 = CreateMatrix(anahtar1);    // sağ üst (random key1)
            matrix3 = CreateMatrix(anahtar2);    // sol alt (random key2)
            matrix4 = CreateMatrix("");          // sağ alt (alfabe)

            input = new string(input.ToUpper().Replace("İ", "I").Where(c => alphabet.Contains(c)).ToArray());
            if (input.Length % 2 != 0) input += "X";

            var sb = new StringBuilder();
            for (int i = 0; i < input.Length; i += 2)
            {
                char a = input[i];
                char b = input[i + 1];

                FindPosition(matrix1, a, out int r1, out int c1);
                FindPosition(matrix4, b, out int r2, out int c2);

                sb.Append(matrix2[r1, c2]);
                sb.Append(matrix3[r2, c1]);
            }

            return sb.ToString();
        }

        public string Decrypt(string input, string notUsedKey = "")
        {
            if (anahtar1 == null || anahtar2 == null)
                throw new InvalidOperationException("Anahtarlar şifreleme sırasında oluşturulmalıdır.");

            matrix1 = CreateMatrix("");              // sol üst (alfabe)
            matrix2 = CreateMatrix(anahtar1);        // sağ üst (random key1)
            matrix3 = CreateMatrix(anahtar2);        // sol alt (random key2)
            matrix4 = CreateMatrix("");              // sağ alt (alfabe)

            input = new string(input.ToUpper().Where(c => alphabet.Contains(c)).ToArray());

            var sb = new StringBuilder();
            for (int i = 0; i < input.Length; i += 2)
            {
                char a = input[i];
                char b = input[i + 1];

                FindPosition(matrix2, a, out int r1, out int c1);
                FindPosition(matrix3, b, out int r2, out int c2);

                sb.Append(matrix1[r1, c2]);
                sb.Append(matrix4[r2, c1]);
            }

            return sb.ToString();
        }

        public string GetAnahtarlar()
        {
            return $"Anahtar1: {anahtar1}\nAnahtar2: {anahtar2}";
        }
    }
}