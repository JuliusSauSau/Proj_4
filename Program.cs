using System;

namespace _4_Projektas_uzd
{
    internal class Program
    {
        static void Main()
        {
            bool exitProgram = false;

            while (!exitProgram)
            {
                Console.WriteLine("Choose mode (1 - Send, 2 - Receive, 3 - Exit):");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Sender sender = new Sender();
                        sender.Run();
                        break;
                    case "2":
                        Receiver receiver = new Receiver();
                        receiver.Run();
                        break;
                    case "3":
                        exitProgram = true;
                        Console.WriteLine("Exiting the program...");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please enter 1, 2, or 3.");
                        break;
                }
            }
        }
    }
}
