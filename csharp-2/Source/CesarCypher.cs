using System;
using System.Linq;

namespace Codenation.Challenge
{
    public class CesarCypher : ICrypt, IDecrypt
    {
        public const int casa = 3;
        public const string alfabeto = "abcdefghijklmnopqrstuvwxyz";
        public const string numeros = "0123456789";

        public string Crypt(string message)
        {
            return Cypher(message, casa);
        }

        public string Decrypt(string cryptedMessage)
        {
            return Cypher(cryptedMessage, 26 - casa);
        }

        public string Cypher(string message, int casas)
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
    }
}


