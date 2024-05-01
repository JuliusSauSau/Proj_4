using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace _4_Projektas_uzd
{
    internal class Sender
    {
        public void Run()
        {
            Console.WriteLine("Enter the message:");
            string message = Console.ReadLine();

            using RSA rsa = RSA.Create();

            // Export private key (not required in this scenario)
            RSAParameters privateKey = rsa.ExportParameters(true);
            // Export public key
            RSAParameters publicKey = rsa.ExportParameters(false);

            // Compute hash of the message
            byte[] hash;
            using (SHA256 sha256 = SHA256.Create())
            {
                hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(message));
            }

            // Create a digital signature
            byte[] signature = rsa.SignHash(hash, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);

            // Export the public key as PEM or byte array
            byte[] publicKeyData = rsa.ExportSubjectPublicKeyInfo();

            // Write message, signature, and public key to a text file
            string filePath = "message.txt";
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine(Convert.ToBase64String(Encoding.UTF8.GetBytes(message)));
                writer.WriteLine(Convert.ToBase64String(signature));
                writer.WriteLine(Convert.ToBase64String(publicKeyData));
            }

            Console.WriteLine("Message and digital signature sent successfully!");
        }
    }
}
