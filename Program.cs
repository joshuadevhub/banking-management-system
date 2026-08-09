#nullable disable

using System;

class Program
{
  static void Main(string[] args)
  {
    JsonStorage jsonStorage = new JsonStorage();
    BankingService bankingService = new BankingService(jsonStorage);

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
    DisplaySectionHeading("Register Customer");

    string firstName = PromptString("Enter First Name");
    string middleName = PromptString("Enter Middle Name (Press Enter if none)");
    string lastName = PromptString("Enter Last Name");
    string phoneNumber = PromptString("Enter Phone Number");
    string email = PromptString("Enter Email Address");

    DisplaySectionHeading("Address Information");

    string houseNumber = PromptString("House Number");
    string street = PromptString("Street");
    string city = PromptString("City");
    string state = PromptString("State");
    string country = PromptString("Country");
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
    DisplaySectionHeading("Open Account");

    string customerPhoneNumber = PromptString("Enter Phone Number");

    DisplaySectionHeading("New Account Information");

    string accountType = PromptString("Enter Account Type (Savings/Current)");

    string accountPin = PromptString("Set a 4-digit PIN for your new account");
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
    DisplaySectionHeading("Deposit");

    string accountNumber = PromptString("Enter Account Number");

    decimal amount = PromptDecimal("Enter Deposit Amount", "$");
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
    DisplaySectionHeading("Withdraw");

    string accountNumber = PromptString("Enter Account Number");

    decimal withdrawalAmount = PromptDecimal("Enter Withdrawal Amount", "$");
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
    DisplaySectionHeading("Customer Information");

    string phoneNumber = PromptString("Enter Phone Number");
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
    DisplaySectionHeading("Account Information");

    string accountNumber = PromptString("Enter Account Number");
    try
    {
      Account account = bankingService.GetAccountByNumber(accountNumber);
      Console.WriteLine(account.GetAccountInfo());
    }
    catch (Exception ex)
    {
      Console.WriteLine(ex.Message);
    }
  }

  static void ViewTransactionHistory(BankingService bankingService)
  {
    DisplaySectionHeading("Transaction History");

    string customerAccountNumber = PromptString("Enter Account Number");
    try
    {
      Account customerAccount = bankingService.GetAccountByNumber(customerAccountNumber);
      Console.WriteLine(customerAccount.GetTransactionHistory());
    }
    catch (Exception ex)
    {
      Console.WriteLine(ex.Message);
    }
  }

  static void DisplaySectionHeading(string heading)
  {
    Console.WriteLine($"\n========== {heading} ==========\n");
  }

  static string PromptString(string prompt)
  {
    while (true)
    {
      Console.Write($"{prompt}: ");
      string input = Console.ReadLine();
      if (!string.IsNullOrWhiteSpace(input))
      {
        return input;
      }
      Console.WriteLine("\nField cannot be empty. Please try again.");
    }
  }
  
  static decimal PromptDecimal(string prompt, string symbol = "")
  {
    while (true)
    {
      Console.Write($"{prompt}: {symbol}");
      string input = Console.ReadLine();

      if (decimal.TryParse(input, out decimal userResponse))
      {
        return userResponse;
      }
      else
      {
        Console.WriteLine("\nInvalid Input. Please try again");
      }
    }
  }
}