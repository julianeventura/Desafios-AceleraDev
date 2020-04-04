using System;
using System.Linq;

namespace Codenation.Challenge
{
    public class CesarCypher : ICrypt, IDecrypt
    {
        public const int casas = 3;
        public const string alfabeto = "abcdefghijklmnopqrstuvwxyz";
        public const string numeros = "0123456789";

        public string Crypt(string message)
        {
            string output = string.Empty;

            if (message != null)
            {
                message = message.ToLower();
            }
            else
                throw new ArgumentNullException();

            foreach (char c in message)
            {
                if (alfabeto.Contains(c))
                {
                    int alfabetoIndex = alfabeto.IndexOf(c);
                    if (alfabetoIndex + casas >= alfabeto.Length)
                        alfabetoIndex = (alfabetoIndex + casas) - alfabeto.Length;
                    else
                        alfabetoIndex += casas;

                    output += alfabeto.Substring(alfabetoIndex, 1);
                }
                else if (numeros.Contains(c) || char.IsWhiteSpace(c))
                {
                    output += c;
                }
                else
                    throw new ArgumentOutOfRangeException();
            }

            return output;
        }


        public string Decrypt(string cryptedMessage)
        {
            string output = string.Empty;

            if (cryptedMessage != null)
            {
                cryptedMessage = cryptedMessage.ToLower();
            }
            else
                throw new ArgumentNullException();

            foreach (char c in cryptedMessage)
            {
                if (alfabeto.Contains(c))
                {
                    int alfabetoIndex = alfabeto.IndexOf(c);
                    if (alfabetoIndex - casas < 0)
                        alfabetoIndex = (alfabetoIndex - casas) + 26;
                    else
                        alfabetoIndex -= casas;

                    output += alfabeto.Substring(alfabetoIndex, 1);
                }
                else if (numeros.Contains(c) || char.IsWhiteSpace(c))
                {
                    output += c;
                }
                else
                    throw new ArgumentOutOfRangeException();
            }

            return output;
        }

        
    }
}


