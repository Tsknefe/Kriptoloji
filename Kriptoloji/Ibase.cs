using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kriptoloji
{
    public interface Ibase
    {
        string Encrypt(string text, string key = null);
        string Decrypt(string text, string key = null);
    }
}
