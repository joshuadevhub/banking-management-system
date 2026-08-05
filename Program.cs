#nullable disable

using System;

class Program
{
  static void Main(string[] args)
  {
    BankingService bankingService = new BankingService();

    Console.WriteLine("====================================================");
    Console.WriteLine("WELCOME TO C# BANK");
    Console.WriteLine("====================================================");

    int userResponse;

    do
    {
      Console.WriteLine();
      Console.WriteLine("1. Register Customer");
      Console.WriteLine("2. Open Account");
      Console.WriteLine("3. Deposit");
      Console.WriteLine("4. Withdraw");
      Console.WriteLine("5. View Customer Information");
      Console.WriteLine("6. View Account Information");
      Console.WriteLine("7. View Transaction History");
      Console.WriteLine("0. Exit");

      Console.Write("Please choose from the options above to continue our banking service: ");
      string input = Console.ReadLine();

      if (!int.TryParse(input, out userResponse))
      {
        Console.WriteLine("Invalid input. Please enter a number.");
        continue;
      }

      if (userResponse < 0 || userResponse > 7)
      {
        Console.WriteLine("Invalid Option. Input must be between 0 and 7");
        continue;
      }

      switch (userResponse)
      {
        case 1:
          RegisterCustomer(bankingService);
          break;

        case 2:
          OpenAccount(bankingService);
          break;

        case 3:
          Deposit(bankingService);
          break;

        case 4:
          Withdraw(bankingService);
          break;

        case 5:
          ViewCustomerInformation(bankingService);
          break;

        case 6:
          ViewAccountInformation(bankingService);
          break;

        case 7:
          ViewTransactionHistory(bankingService);
          break;
      }
    } while (userResponse != 0);

    Console.WriteLine("\nThank you for banking with C# Bank. Goodbye!");
  }

  static void RegisterCustomer(BankingService bankingService)
  {
    Console.WriteLine("\n========== Register Customer ==========\n");

    Console.Write("Enter First Name: ");
    string firstName = Console.ReadLine();
    Console.Write("Enter Middle Name (Press Enter if none): ");
    string middleName = Console.ReadLine();
    Console.Write("Enter Last Name: ");
    string lastName = Console.ReadLine();
    Console.Write("Enter Phone Number: ");
    string phoneNumber = Console.ReadLine();
    Console.Write("Enter Email Address: ");
    string email = Console.ReadLine();
    Console.WriteLine("\n----- Address Information -----");
    Console.Write("House Number: ");
    int houseNumber = int.Parse(Console.ReadLine());
    Console.Write("Street: ");
    string street = Console.ReadLine();
    Console.Write("City: ");
    string city = Console.ReadLine();
    Console.Write("State: ");
    string state = Console.ReadLine();
    Console.Write("Country: ");
    string country = Console.ReadLine();
    try
    {
      Address customerAddress = new Address(houseNumber, street, city, state, country);
      Customer customer = bankingService.CreateCustomer(firstName, lastName, phoneNumber, email, customerAddress, middleName);
      Console.WriteLine("\nCustomer registered successfully!");
      Console.WriteLine($"Customer ID: {customer.GetCustomerId()}");
    }
    catch (Exception ex)
    {
      Console.WriteLine(ex.Message);
    }
  }

  static void OpenAccount(BankingService bankingService)
  {
    Console.WriteLine("\n========== Open Account ==========\n");

    Console.Write("Enter Phone Number: ");
    string customerPhoneNumber = Console.ReadLine();

    Console.WriteLine("\n========== New Account Information ==========\n");

    Console.Write("Enter Account Type (Savings/Current): ");
    string accountType = Console.ReadLine();

    Console.Write("Set a 4-digit PIN for your new account: ");
    string accountPin = Console.ReadLine();
    try
    {
      Customer customer = bankingService.FindCustomerByPhoneNumber(customerPhoneNumber);
      string customerId = customer.GetCustomerId();
      Account account = bankingService.OpenAccount(customerId, accountType, accountPin);
      Console.WriteLine("\nAccount Created successfully!");
      Console.WriteLine($"Account Number: {account.GetAccountNumber()}");
    }
    catch (Exception ex)
    {
      Console.WriteLine(ex.Message);
    }
  }

  static void Deposit(BankingService bankingService)
  {
    Console.WriteLine("\n========== Deposit ==========\n");

    Console.Write("Enter Account Number: ");
    string accountNumber = Console.ReadLine();

    Console.Write("Enter Deposit Amount: $");
    decimal amount = decimal.Parse(Console.ReadLine());
    try
    {
      Account customerAccount = bankingService.GetAccountByNumber(accountNumber);
      bankingService.Deposit(accountNumber, amount);
      Console.WriteLine();
      Console.WriteLine("=========================================");
      Console.WriteLine("      DEPOSIT SUCCESSFUL");
      Console.WriteLine("=========================================");
      Console.WriteLine($"Amount Deposited : ${amount:N2}");
      Console.WriteLine($"Current Balance  : ${customerAccount.GetAccountBalance():N2}");
      Console.WriteLine();
      Console.WriteLine("Your account has been credited successfully.");
      Console.WriteLine("Thank you for banking with C# Bank.");
      Console.WriteLine("=========================================");
    }
    catch (Exception ex)
    {
      Console.WriteLine(ex.Message);
    }
  }

  static void Withdraw(BankingService bankingService)
  {
    Console.WriteLine("\n========== Withdraw ==========\n");
    Console.Write("Enter Account Number: ");
    string accountNumber = Console.ReadLine();

    Console.Write("Enter Withdrawal Amount: ");
    decimal withdrawalAmount = decimal.Parse(Console.ReadLine());
    try
    {
      Account account = bankingService.GetAccountByNumber(accountNumber);
      bankingService.Withdraw(accountNumber, withdrawalAmount);
      Console.WriteLine();
      Console.WriteLine("=========================================");
      Console.WriteLine("      WITHDRAWAL SUCCESSFUL");
      Console.WriteLine("=========================================");
      Console.WriteLine($"Amount Withdrawn : ${withdrawalAmount:N2}");
      Console.WriteLine($"Current Balance  : ${account.GetAccountBalance():N2}");
      Console.WriteLine();
      Console.WriteLine("Your account has been debited successfully.");
      Console.WriteLine("Thank you for banking with C# Bank.");
      Console.WriteLine("=========================================");
    }
    catch (Exception ex)
    {
      Console.WriteLine(ex.Message);
    }
  }

  static void ViewCustomerInformation(BankingService bankingService)
  {
    Console.WriteLine("\n========== Customer Information ==========\n");

    Console.Write("Enter Phone Number: ");
    string phoneNumber = Console.ReadLine();
    try
    {
      Customer customer = bankingService.FindCustomerByPhoneNumber(phoneNumber);
      Console.WriteLine(customer.GetCustomerInfo());
    }
    catch (Exception ex)
    {
      Console.WriteLine(ex.Message);
    }
  }

  static void ViewAccountInformation(BankingService bankingService)
  {
    Console.Write("Enter Account Number: ");
    string accountNumber = Console.ReadLine();
    try
    {
      Account account = bankingService.GetAccountByNumber(accountNumber);
      Console.WriteLine("\n========== Account Information ==========\n");
      Console.WriteLine(account.GetAccountInfo());
    }
    catch (Exception ex)
    {
      Console.WriteLine(ex.Message);
    }
  }

  static void ViewTransactionHistory(BankingService bankingService)
  {
    Console.Write("Enter Account Number: ");
    string customerAccountNumber = Console.ReadLine();
    try
    {
      Account customerAccount = bankingService.GetAccountByNumber(customerAccountNumber);
      Console.WriteLine("\n========== Transaction History ==========\n");
      Console.WriteLine(customerAccount.GetTransactionHistory());
    }
    catch (Exception ex)
    {
      Console.WriteLine(ex.Message);
    }
  }
  
}