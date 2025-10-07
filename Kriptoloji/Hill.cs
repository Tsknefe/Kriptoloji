using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kriptoloji
{
    public class Hill : Ibase
    {
        private const string alphabet = "ABCÇDEFGĞHIİJKLMNOÖPRSŞTUÜVYZ";

        public string Encrypt(string input, string key)
        {
            input = PrepareInput(input);
            int[,] keyMatrix = GetKeyMatrix(key);

            StringBuilder result = new StringBuilder();

            for (int i = 0; i < input.Length; i += 3)
            {
                int[] block = new int[3];
                for (int j = 0; j < 3; j++)
                {
                    // Ensure that i + j is within bounds of the string length
                    if (i + j < input.Length)
                        block[j] = alphabet.IndexOf(input[i + j]);
                    else
                        block[j] = alphabet.IndexOf('X'); // Fill with 'X' if we run out of characters
                }

                for (int row = 0; row < 3; row++)
                {
                    int value = 0;
                    for (int col = 0; col < 3; col++)
                        value += keyMatrix[row, col] * block[col];
                    result.Append(alphabet[value % alphabet.Length]);
                }
            }

            return result.ToString();
        }

        public string Decrypt(string input, string key)
        {
            input = PrepareInput(input);
            int[,] keyMatrix = GetKeyMatrix(key);
            int[,] inverseMatrix = MatrixInverseMod(keyMatrix, alphabet.Length);

            StringBuilder result = new StringBuilder();

            for (int i = 0; i < input.Length; i += 3)
            {
                int[] block = new int[3];
                for (int j = 0; j < 3; j++)
                {
                    // Ensure that i + j is within bounds of the string length
                    if (i + j < input.Length)
                        block[j] = alphabet.IndexOf(input[i + j]);
                    else
                        block[j] = alphabet.IndexOf('X'); // Fill with 'X' if we run out of characters
                }

                for (int row = 0; row < 3; row++)
                {
                    int value = 0;
                    for (int col = 0; col < 3; col++)
                        value += inverseMatrix[row, col] * block[col];
                    result.Append(alphabet[(value % alphabet.Length + alphabet.Length) % alphabet.Length]);
                }
            }

            return result.ToString();
        }

        private string PrepareInput(string input)
        {
            input = new string(input.ToUpper().Where(c => alphabet.Contains(c)).ToArray());
            while (input.Length % 3 != 0) input += "X";
            return input;
        }

        private int[,] GetKeyMatrix(string key)
        {
            if (key.Length != 9)
                throw new ArgumentException("Hill anahtarı 9 harften oluşmalı (3x3 matris için)");

            int[,] matrix = new int[3, 3];
            key = key.ToUpper();

            for (int i = 0; i < 9; i++)
            {
                char ch = key[i];
                int index = alphabet.IndexOf(ch);
                if (index == -1)
                    throw new ArgumentException($"Geçersiz karakter: {ch}");
                matrix[i / 3, i % 3] = index;
            }

            return matrix;
        }

        private int[,] MatrixInverseMod(int[,] matrix, int mod)
        {
            int det = Determinant(matrix);
            int detInv = ModInverse(det, mod);
            if (detInv == -1) throw new ArgumentException("Anahtar matrisi terslenemez.");

            int[,] cof = Cofactor(matrix);
            int[,] adj = Transpose(cof);

            int[,] inv = new int[3, 3];
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    inv[i, j] = (adj[i, j] * detInv % mod + mod) % mod;

            return inv;
        }

        private int Determinant(int[,] m)
        {
            return (m[0, 0] * (m[1, 1] * m[2, 2] - m[1, 2] * m[2, 1])
                  - m[0, 1] * (m[1, 0] * m[2, 2] - m[1, 2] * m[2, 0])
                  + m[0, 2] * (m[1, 0] * m[2, 1] - m[1, 1] * m[2, 0])) % alphabet.Length;
        }

        private int[,] Cofactor(int[,] m)
        {
            int[,] cof = new int[3, 3];
            cof[0, 0] = m[1, 1] * m[2, 2] - m[1, 2] * m[2, 1];
            cof[0, 1] = -(m[1, 0] * m[2, 2] - m[1, 2] * m[2, 0]);
            cof[0, 2] = m[1, 0] * m[2, 1] - m[1, 1] * m[2, 0];
            cof[1, 0] = -(m[0, 1] * m[2, 2] - m[0, 2] * m[2, 1]);
            cof[1, 1] = m[0, 0] * m[2, 2] - m[0, 2] * m[2, 0];
            cof[1, 2] = -(m[0, 0] * m[2, 1] - m[0, 1] * m[2, 0]);
            cof[2, 0] = m[0, 1] * m[1, 2] - m[0, 2] * m[1, 1];
            cof[2, 1] = -(m[0, 0] * m[1, 2] - m[0, 2] * m[1, 0]);
            cof[2, 2] = m[0, 0] * m[1, 1] - m[0, 1] * m[1, 0];
            return cof;
        }

        private int[,] Transpose(int[,] m)
        {
            int[,] t = new int[3, 3];
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    t[i, j] = m[j, i];
            return t;
        }

        private int ModInverse(int a, int m)
        {
            a %= m;
            for (int x = 1; x < m; x++)
                if ((a * x) % m == 1)
                    return x;
            return -1;
        }
    }
}