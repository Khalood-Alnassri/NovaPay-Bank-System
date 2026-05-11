namespace NovaPay_Bank_System
{
    internal class Program
    {
        public static void DisplayMenu()
        {
            Console.WriteLine("============================================");
            Console.WriteLine("====== Welcome to NovaPay Bank System ======");
            Console.WriteLine("1. Open a Savings Account.");
            Console.WriteLine("2. Open a Current Account.");
            Console.WriteLine("3. Open a Fixed Deposit Account.");
            Console.WriteLine("4. Deposit.");
            Console.WriteLine("5. Withdraw.");
            Console.WriteLine("6. Print Account Statement.");
            Console.WriteLine("7. Apply Interest (Savings Only).");
            Console.WriteLine("8. Bank Summary.");
            Console.WriteLine("9. Exit.");
            Console.WriteLine("============================================");
        }

        public static int UserChoice()
        {
            int choice;

            while (true)
            {
                Console.Write("Please enter your choice: ");
                string input = Console.ReadLine() ?? string.Empty;

                if (int.TryParse(input, out choice) && choice >= 1 && choice <= 9)
                {
                    return choice;
                }

                Console.WriteLine("Invalid input. Please enter a number between 1 and 9.");
            }
        }

        static void Main(string[] args)
        {

            bool exit = true;

            while (! exit)
            {
                DisplayMenu();

                int choice = UserChoice();

                switch (choice)
                {
                    case 1:

                        break;

                    case 2:

                        break;

                    case 3:

                        break;

                    case 4:

                        break;

                    case 5:

                        break;

                    case 6:

                        break;

                    case 7:

                        break;

                    case 8:

                        break;

                    case 9:
                        
                        break;

                    default:

                        Console.WriteLine("Invalid choice. Please try again.");

                        break;
                }

                Console.WriteLine("Press any key to continue....");
                Console.ReadKey();
                Console.Clear();

            }
        }


    }
    interface IDepositable
    {
        public void Deposit(double amount);
    }

    interface IWithdrawable
    {
        public void Withdraw(double amount);
    }

    interface IStatementPrintable
    {
        public void PrintStatement();
    }

    abstract class BankAccount : IDepositable, IWithdrawable, IStatementPrintable
    {
        
    }

    class SavingsAccount : BankAccount
    {

    }

    class CurrentAccount : BankAccount
    {
    }

    class FixedDepositAccount : BankAccount
    {
    }
     class Transaction
    {

    }

    class Bank
    {

    }

}
