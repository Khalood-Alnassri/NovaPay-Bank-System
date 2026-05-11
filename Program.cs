using System.Runtime.CompilerServices;

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
        static int nextAccountNumber = 1001;
        static int totalTransactionsProcessed;
        protected double balance;
        protected List<Transaction> transactions;
        string accountType;
        string ownerName;

        // property to only get the account number
        public int GetAccountNumber { get; }

        // property to only get the Owner Name 
        public string GetOwnerName { get; }

        // property to only get the balance
        public double GetBalance { get; }

        // property to get and set the account type
        public string AccountType
        {
            get
            {
                return accountType;
            }

            set
            {
                if (value == "savings")
                {
                    accountType = "Savings";
                }
                else if (value == "current")
                {
                    accountType = "Current";
                }
                else if (value == "fixed deposit")
                {
                    accountType = "Fixed Deposit";
                }
                else
                {
                    Console.WriteLine("Invalid account type. Please choose 'Savings', 'Current', or 'Fixed Deposit'.");
                }
            }
        }

        // static method to increase the total transactions processed
        public static void IncreaseTransactions()
        {
            totalTransactionsProcessed++;
        }

        // constructor to initialize the account number and owner name
        public BankAccount(string ownerName)
        {
            this.ownerName = ownerName;
            this.GetAccountNumber = nextAccountNumber++;
            this.balance = 0.0;
            this.transactions = new List<Transaction>();
        }

        // method to get the total number of transactions processed
        public static int GetTotalTransactions()
        {
            return totalTransactionsProcessed;
        }

        // method to deposit money into the account
        public void Deposit(double amount)
        {
            if(amount > 0)
            {
                balance += amount;
                totalTransactionsProcessed++;
                transactions.Add(new Transaction ("Deposit", amount));
                Console.WriteLine("The amount deposited successfully.");
            }
            else
            {
                Console.WriteLine("Deposit amount must be greater than zero.");
            }
        }

        // overloaded method to deposit money with a note
        public void Deposit(double amount, string note)
        {
            if (amount > 0)
            {
                balance += amount;
                totalTransactionsProcessed++;
                transactions.Add(new Transaction("Deposit", amount));
                Console.WriteLine("The amount deposited successfully.");
                note = "Interest Credit";
            }
            else
            {
                Console.WriteLine("Deposit amount must be greater than zero.");
            }
        }

        // abstract method to withdraw money from the account
        public abstract void Withdraw(double amount);

        public virtual void PrintStatement()
        { 
            Console.WriteLine("Account Number: " + nextAccountNumber);
            Console.WriteLine("Owner Name: " + ownerName);
            Console.WriteLine("Account Type: " + accountType);
            Console.WriteLine("Balance: " + balance);

            // print the transaction history
            foreach (var transaction in transactions)
            {
                transaction.DisplayInfo();
            }

        }
    }

    class SavingsAccount : BankAccount
    {
        double interestRate;
        static double MinBalance = 100;

        // constructor to initialize the savings account
        public SavingsAccount(string ownerName, double interestRate = 0.03) : base(ownerName)
        {
            AccountType = "Savings";
            this.interestRate = 0.03;
        }

        // method to Withdraw 
        public override void Withdraw(double amount)
        {
            if (amount > 0 && (balance - amount) >= MinBalance)
            {
                balance -= amount;
                BankAccount.IncreaseTransactions();
                transactions.Add(new Transaction("Withdrawal", amount));
                Console.WriteLine("The amount withdrawn successfully.");
            }
            else
            {
                Console.WriteLine("Withdrawal failed. Ensure the amount is positive and the remaining balance does not fall below the minimum balance of " + MinBalance);
            }
        }

        //method to print the account statement, including the interest rate
        public override void PrintStatement()
        {
            base.PrintStatement();
            Console.WriteLine("Interest Rate: " + interestRate);
        }

        // method to apply interest to the account balance
        public void ApplyInterest()
        {
            double interest = balance * interestRate;
            Deposit(interest, "Interest Credit");
            Console.WriteLine("Interest applied successfully. Interest amount: " + interest);
        }
    }

    class CurrentAccount : BankAccount
    {
    }

    class FixedDepositAccount : BankAccount
    {
    }
     class Transaction
    {
        string type;
        double amount;
        DateTime date;
        string note;

        // constructor to initialize the transaction details
        public Transaction(string type, double amount, string note = "")
        {
            this.type = type;
            this.amount = amount;
            this.date = DateTime.Now;
            this.note = note;
        }

        public void DisplayInfo()
        {

        }
    }

    class Bank
    {

    }

}
