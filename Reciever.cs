using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace _4_Projektas_uzd
{
    internal class Receiver
    {
        public void Run()
        {
            Console.WriteLine("Reading message and digital signature...");

            string filePath = "message.txt";

            // Read the message, signature, and public key from the text file
            using (StreamReader reader = new StreamReader(filePath))
            {
                // Read the encoded message
                string messageBase64 = reader.ReadLine();
                string signatureBase64 = reader.ReadLine();
                string publicKeyBase64 = reader.ReadLine();

                // Convert encoded strings back to byte arrays
                byte[] messageData = Convert.FromBase64String(messageBase64);
                byte[] signatureData = Convert.FromBase64String(signatureBase64);
                byte[] publicKeyData = Convert.FromBase64String(publicKeyBase64);

                // Convert byte arrays back to strings
                string message = Encoding.UTF8.GetString(messageData);

                // Create an RSA object
                using RSA rsa = RSA.Create();
                rsa.ImportSubjectPublicKeyInfo(publicKeyData, out _);

                // Compute hash of the message
                byte[] hash;
                using (SHA256 sha256 = SHA256.Create())
                {
                    hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(message));
                }

                // Verify the digital signature
                bool isSignatureValid = rsa.VerifyHash(hash, signatureData, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);

                if (isSignatureValid)
                {
                    Console.WriteLine("The digital signature is valid. The message is authentic.");
                }
                else
                {
                    Console.WriteLine("The digital signature is invalid. The message cannot be trusted.");
                }
            }
        }
    }
}
