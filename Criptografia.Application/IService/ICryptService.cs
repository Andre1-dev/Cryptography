using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Criptografia.Application.IService
{
    public interface ICryptService
    {
        byte[] Cifrar(string texto, string chave);
        string Decifrar(byte[] cifrado, string chave);
    }
}
