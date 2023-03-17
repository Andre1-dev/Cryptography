using Criptografia.Application.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Criptografia.Application.Service
{
    public class CryptService:ICryptService
    {
        static byte[] KSA(string key)
        {
            /* Key-scheduling algorithm (KSA) do RC4 */
            byte[] S = new byte[256];
            for (int i = 0; i < 256; i++)
            {
                S[i] = (byte)i;
            }

            int j = 0;
            for (int i = 0; i < 256; i++)
            {
                j = (j + S[i] + key[i % key.Length]) % 256;
                byte temp = S[i];
                S[i] = S[j];
                S[j] = temp;
            }

            return S;
        }

        static byte[] PRGA(byte[] S, int n)
        {
            /* Pseudo-random generation algorithm (PRGA) do RC4 */
            byte[] key_stream = new byte[n];

            int i = 0;
            int j = 0;
            for (int k = 0; k < n; k++)
            {
                i = (i + 1) % 256;
                j = (j + S[i]) % 256;
                byte temp = S[i];
                S[i] = S[j];
                S[j] = temp;
                int K = S[(S[i] + S[j]) % 256];
                key_stream[k] = (byte)K;
            }

            return key_stream;
        }

        public byte[] Cifrar(string texto, string chave)
        {
            /* Cifra o texto usando o algoritmo RC4 */
            byte[] S = KSA(chave);
            byte[] key_stream = PRGA(S, texto.Length);

            byte[] cifrado = new byte[texto.Length];
            for (int i = 0; i < texto.Length; i++)
            {
                cifrado[i] = (byte)(texto[i] ^ key_stream[i]);
            }

            return cifrado;
        }

        public string Decifrar(byte[] cifrado, string chave)
        {
            /* Decifra o texto cifrado usando o algoritmo RC4 */
            //byte[] cifrado = stringToByte(cifradoString);
            byte[] S = KSA(chave);
            byte[] key_stream = PRGA(S, cifrado.Length);

            StringBuilder texto = new StringBuilder(cifrado.Length);
            for (int i = 0; i < cifrado.Length; i++)
            {
                texto.Append((char)(cifrado[i] ^ key_stream[i]));
            }

            return texto.ToString();
        }

            
        private static byte[] stringToByte(string message)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(message);

            return bytes;
        }
    }
}
