using System.Runtime.CompilerServices;
using System.Security.Principal;

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

        // case 1: function to open a savings account
        public static void OpenSavingsAccount(Bank bank)
        {
            Console.Write("Enter owner name: ");
            string ownerName = Console.ReadLine() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(ownerName))
            {
                Console.WriteLine("Invalid name.");
                return;
            }

            BankAccount savingsAccount = new SavingsAccount(ownerName);
            bank.OpenAccount(savingsAccount);
            Console.WriteLine("Savings account opened successfully.");
        }

        // case 2: function to open a current account
        public static void OpenCurrentAccount(Bank bank)
        {
            Console.Write("Enter owner name: ");
            string ownerName = Console.ReadLine() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(ownerName))
            {
                Console.WriteLine("Invalid name.");
                return;
            }

            Console.Write("Enter overdraft limit: ");
            string input = Console.ReadLine() ?? string.Empty;

            if (!double.TryParse(input, out double overdraftLimit) || overdraftLimit < 0)
            {
                Console.WriteLine("Invalid overdraft limit. Please enter a non-negative number.");
                return;
            }

            BankAccount currentAccount = new CurrentAccount(ownerName, overdraftLimit);
            bank.OpenAccount(currentAccount);
            Console.WriteLine("Current account opened successfully.");
        }

        // case 3: function to open a fixed deposit account
        public static void OpenFixedDepositAccount(Bank bank)
        {
            Console.Write("Enter owner name: ");
            string ownerName = Console.ReadLine() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(ownerName))
            {
                Console.WriteLine("Invalid name.");
                return;
            }

            Console.Write("Enter deposit amount: ");
            string input = Console.ReadLine() ?? string.Empty;

            if (!double.TryParse(input, out double depositAmount) || depositAmount <= 0)
            {
                Console.WriteLine("Invalid deposit amount. Please enter a number greater than zero.");
                return;
            }

            BankAccount fixedDepositAccount = new FixedDepositAccount(ownerName, depositAmount);
            bank.OpenAccount(fixedDepositAccount);
            Console.WriteLine("Fixed Deposit account opened successfully.");
        }

        // case 4: function to process a deposit
        public static void Deposit(Bank bank)
        {
            Console.Write("Enter account number: ");
            string input = Console.ReadLine() ?? string.Empty;

            if (!int.TryParse(input, out int accountNumber))
            {
                Console.WriteLine("Invalid account number. Please enter a valid integer.");
                return;
            }

            BankAccount account = bank.FindAccount(accountNumber);
            if (account == null)
            {
                Console.WriteLine("Account not found.");
                return;
            }

            Console.Write("Enter deposit amount: ");
            input = Console.ReadLine() ?? string.Empty;

            if (!double.TryParse(input, out double amount) || amount <= 0)
            {
                Console.WriteLine("Invalid deposit amount. Please enter a number greater than zero.");
                return;
            }

            bank.ProcessDeposit(account, amount);
        }

        // case 5: function to process a withdrawal
        public static void Withdraw(Bank bank)
        {
            Console.Write("Enter account number: ");
            string input = Console.ReadLine() ?? string.Empty;

            if (!int.TryParse(input, out int accountNumber))
            {
                Console.WriteLine("Invalid account number. Please enter a valid integer.");
                return;
            }

            BankAccount account = bank.FindAccount(accountNumber);
            if (account == null)
            {
                Console.WriteLine("Account not found.");
                return;
            }

            Console.Write("Enter withdrawal amount: ");
            input = Console.ReadLine() ?? string.Empty;

            if (!double.TryParse(input, out double amount) || amount <= 0)
            {
                Console.WriteLine("Invalid withdrawal amount. Please enter a number greater than zero.");
                return;
            }

            bank.ProcessWithdrawal(account, amount);
        }

        // case 6: function to print account statement
        public static void PrintAccountStatement(Bank bank)
        {
            Console.Write("Enter account number: ");
            string input = Console.ReadLine() ?? string.Empty;

            if (!int.TryParse(input, out int accountNumber))
            {
                Console.WriteLine("Invalid account number. Please enter a valid integer.");
                return;
            }

            bank.PrintAccountStatement(accountNumber);
        }
        static void Main(string[] args)
        {
            Bank bank = new Bank("NovaPay");
            bool exit = true;

            while (! exit)
            {
                DisplayMenu();

                int choice = UserChoice();

                switch (choice)
                {
                    case 1:

                        OpenSavingsAccount(bank);

                        break;

                    case 2:

                        OpenCurrentAccount(bank);

                        break;

                    case 3:

                        OpenFixedDepositAccount(bank);

                        break;

                    case 4:

                        Deposit(bank);

                        break;

                    case 5:

                        Withdraw(bank);

                        break;

                    case 6:

                        PrintAccountStatement(bank);

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
        double overdraftLimit;

        // property to only get the overdraft limit
        public double GetOverdraftLimit { get; }

        // constructor to initialize the current account
        public CurrentAccount(string ownerName, double overdraftLimit) : base(ownerName)
        {
            AccountType = "Current";

            if (overdraftLimit >= 0)
            {
                this.overdraftLimit = overdraftLimit;
            }

            else
            {
                Console.WriteLine("Overdraft limit must be non negative.");
            }
        }

        // method to Withdraw
        public override void Withdraw(double amount)
        {
            if (amount > 0 && (balance - amount) >= -overdraftLimit)
            {
                balance -= amount;
                BankAccount.IncreaseTransactions();
                transactions.Add(new Transaction("Withdrawal", amount));
                Console.WriteLine("The amount withdrawn successfully.");
            }
            else
            {
                Console.WriteLine("Withdrawal exceeds overdraft limit.");
            }
        }

        // method to print the account statement, including the overdraft limit
        public sealed override void PrintStatement()
        {
            base.PrintStatement();
            Console.WriteLine("Overdraft Limit: " + overdraftLimit);
        }

    }

    class FixedDepositAccount : BankAccount
    {
        double lockedAmount;

        // constructor to initialize the fixed deposit account
        public FixedDepositAccount(string ownerName, double depositAmount) : base(ownerName)
        {
            AccountType = "Fixed Deposit";

            if(depositAmount > 0)
            {
                this.lockedAmount = depositAmount;
                Deposit(depositAmount, "Initial Fixed Deposit");
            }
            else
            {
                Console.WriteLine("Deposit amount must be greater than zero.");
            }
        }

        // method to Withdraw
        public override void Withdraw(double amount)
        {
            Console.WriteLine("Fixed Deposit accounts cannot be withdrawn before maturity.");
        }

        // method to print the account statement, including the locked amount
        public override void PrintStatement()
        {
            base.PrintStatement();
            Console.WriteLine("Locked Amount: " + lockedAmount);
        }

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

        // method to display the transaction information
        public void DisplayInfo()
        {
            Console.WriteLine(date + type + amount + note);
        }
    }

    class Bank
    {
        // Auto-Implemented Property to get and set the bank name
        public string BankName { get; private set; }

        List<BankAccount> accounts;

        // constructor to initialize the bank 
        public Bank(string name)
        {
            BankName = name;
            accounts = new List<BankAccount>();
        }

        //----------------------------- Account Management ------------------------------

        // method to open a new account and add it to the list of accounts
        public void OpenAccount(BankAccount account)
        {
            if (accounts != null)
            { 
                accounts.Add(account);
                Console.WriteLine("Account Number: " + account.GetAccountNumber);
            }

        }

        // method to find an account by account number
        public BankAccount FindAccount(int accountNumber)
        {
            BankAccount account = accounts.Find(a => a.GetAccountNumber == accountNumber);

            if (account != null)
            {
                return account;
            }
            else
            {
                Console.WriteLine("Account not found.");
                return null;
            }
        }


        //------------------------------ Deposit — Parameter declared as IDepositable ------------------------------\

        public void ProcessDeposit(IDepositable account, double amount)
        {
            account.Deposit(amount);
        }

        //------------------------------ Withdraw — Parameter declared as IWithdrawable ------------------------------

        public void ProcessWithdrawal(IWithdrawable account, double amount)
        {
            account.Withdraw(amount);
        }

        //------------------------------------------------ Statement ---------------------------------------

        // method to print the account statement, parameter declared as IStatementPrintable
        public void PrintAccountStatement(int accountNumber)
        {
            FindAccount(accountNumber);
            IStatementPrintable printable = accounts as IStatementPrintable;

            if (accounts != null)
            {
                printable.PrintStatement();
            }

            else
            {
                Console.WriteLine("Error. Account not found!");
            }
        }

        //------------------------------------------- Statistics / Report ----------------------------------

        // method to display the bank statistics
        public void DisplaySummary()
        {
            int totalSaving =0;
            int totalCurrent = 0;
            int totalFixedDeposit = 0;

             foreach (var account in accounts)
            {
                if (account is SavingsAccount)
                {
                    totalSaving++;
                }
                else if (account is CurrentAccount)
                {
                    totalCurrent++;
                }
                else if (account is FixedDepositAccount)
                {
                    totalFixedDeposit++;
                }
            }

            double totalBalance = 0;
            foreach (BankAccount account in accounts)
            {
                totalBalance += account.GetBalance;
            }
          
            Console.WriteLine("=================== Bank Statistics =================");
            Console.WriteLine("Bank Name: " + BankName);
            Console.WriteLine("Total accounts: " + accounts);
            Console.WriteLine("Count of each type: Savings: " + totalSaving + " | Current: " + totalCurrent + " | Fixed Deposit: " + totalFixedDeposit);
            Console.WriteLine("Total balances: " + totalBalance);
            Console.WriteLine("Total transactions processed: " + BankAccount.GetTotalTransactions());

        }

    }

}
