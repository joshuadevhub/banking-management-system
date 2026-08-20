#nullable disable

using System;
using System.Linq;

public class BankingService
{
  private List<Customer> _customers;
  private Random _random = new Random();
  private JsonStorage _storage;

  public BankingService() { }
  
  public BankingService(JsonStorage storage)
  {
    _storage = storage;
    _customers = _storage.Load();
  }


  public Customer CreateCustomer(string firstName, string lastName, string phoneNumber, string email, Address address, string middleName = "")
  {
    ValidateUniquePhoneNumber(phoneNumber);
    ValidateUniqueEmail(email);
    string customerId = GenerateCustomerId();
    Customer customer = new Customer(customerId, firstName, lastName, phoneNumber, email, address, DateTime.Now, new List<Account>(), middleName);
    AddCustomer(customer);
    SaveData();
    return customer;
  }

  public Account OpenAccount(string customerId, string accountType, string accountPin)
  {
    Customer customer = GetCustomerById(customerId);
    bool customerHasAccountType = CustomerHasAccountType(customer, accountType);
    if (customerHasAccountType)
    {
      throw new ArgumentException($"Customer already owns a {accountType} account.");
    }
    string hashedPin = Account.ValidateAndHashPin(accountPin);
    string customerAccountNumber = GenerateAccountNumber();
    Account newAccount = new Account(customerAccountNumber, hashedPin, 0, accountType, new List<Transaction>(), DateTime.Now);
    customer.AddAccount(newAccount);
    SaveData();
    return newAccount;
  }

  public void Deposit(string accountNumber, decimal amount)
  {
    Account account = GetAccountByNumber(accountNumber);
    string transactionId = GenerateTransactionId();
    account.Deposit(amount, transactionId);
    SaveData();
  }

  public void Withdraw(string accountNumber, decimal amount, string pin)
  {
    Account account = GetAccountByNumber(accountNumber);
    VerifyPin(account, pin);
    string transactionId = GenerateTransactionId();
    account.Withdraw(amount, transactionId);
    SaveData();
  }

  public void Transfer(string senderAccountNumber, string receiverAccountNumber, decimal amount, string pin)
  {
    Account senderAccount = GetAccountByNumber(senderAccountNumber, "Sender's");
    Account receiverAccount = GetAccountByNumber(receiverAccountNumber, "Receiver's");

    ValidateTransfer(senderAccount, receiverAccount, amount);
    VerifyPin(senderAccount, pin);
    Customer receiver = FindCustomerByAccountNumber(receiverAccountNumber);
    Customer sender = FindCustomerByAccountNumber(senderAccountNumber);

    string senderTransactionId = GenerateTransactionId();
    string senderDescription = $"Transfer to {receiver.FirstName} {receiver.LastName} {MaskedNumber(receiverAccount.AccountNumber)}";

    senderAccount.Withdraw(amount, senderTransactionId, senderDescription);

    string receiverTransactionId = GenerateTransactionId();
    string receiverDescription = $"Transfer from {sender.FirstName} {sender.LastName} {MaskedNumber(senderAccount.AccountNumber)}";

    receiverAccount.Deposit(amount, receiverTransactionId, receiverDescription);

    SaveData();
  }

  public Customer FindCustomerByPhoneNumber(string phoneNumber)
  {
    string maskedPhoneNumber = MaskedNumber(phoneNumber);
    Customer customer = _customers.FirstOrDefault(c => c.PhoneNumber == phoneNumber);
    if (customer == null)
    {
      throw new ArgumentException($"Customer with this phone number '{maskedPhoneNumber}' was not found");
    }
    return customer;
  }

  public Customer FindCustomerByAccountNumber(string accountNumber)
  {
    string maskedAccountNumber = MaskedNumber(accountNumber);
    Customer customer = _customers.FirstOrDefault(c => c.Accounts.Any(a => a.AccountNumber == accountNumber));
    if (customer == null)
    {
      throw new ArgumentException($"Customer with this account number '{maskedAccountNumber}' was not found");
    }
    return customer;
  }

  public string MaskedNumber(string number)
  {
    string firstFourNumber = number.Substring(0, 4);
    string lastFourNumber = number.Substring(number.Length - 4);
    return $"{firstFourNumber}****{lastFourNumber}";
  }

  private void ValidateTransfer(Account senderAccount, Account receiverAccount, decimal amount)
  {
    if (senderAccount.AccountNumber == receiverAccount.AccountNumber)
    {
      throw new InvalidOperationException("Sender and receiver account cannot be the same");
    }

    if (amount < 1)
    {
      throw new ArgumentException("Amount is below the minimum allowed transfer of $1");
    }

    if (amount > 8000)
    {
      throw new ArgumentException("Amount exceed the maximum allowed transfer limit of $8000");
    }

    if (senderAccount.AccountBalance < amount)
    {
      throw new InvalidOperationException("Insufficient balance for transfer");
    }
  }

  private string GenerateCustomerId()
  {
    string customerId;
    do
    {
      string year = DateTime.Now.Year.ToString();
      customerId = "CUS-";
      customerId += year + "-";
      for (int i = 0; i < 4; i++)
      {
        string randomNumber = _random.Next(0, 10).ToString();
        customerId += randomNumber;
      }
    } while (CustomerIdExists(customerId));
    return customerId;
  }
  
  private string GenerateAccountNumber()
  {
    string accountNumber;
    do
    {
      accountNumber = "0";
      for (int i = 0; i < 9; i++)
      {
        accountNumber += _random.Next(0, 10);
      }
    } while (AccountNumberExists(accountNumber));
    return accountNumber;
  }

  public Account GetAccountByNumber(string accountNumber, string fieldName = "")
  {
    foreach (Customer customer in _customers)
    {
      Account account = customer.GetAccount(accountNumber);
      if (account != null)
      {
        return account;
      }
    }
    throw new ArgumentException($"Invalid account number. {fieldName} Account does not exist");
  }
  
  private bool AccountNumberExists(string accountNumber)
  {
    return _customers.Any(c => c.Accounts.Any(a => a.AccountNumber == accountNumber));
  }

  private bool CustomerIdExists(string customerId)
  {
    return _customers.Any(c => c.CustomerId == customerId);
  }

  private void AddCustomer(Customer customer)
  {
    _customers.Add(customer);
  }

  private void ValidateUniquePhoneNumber(string phoneNumber)
  {
    string maskedPhoneNumber = MaskedNumber(phoneNumber);
    bool phoneNumberExists = _customers.Any(c => c.PhoneNumber == phoneNumber);
    if (phoneNumberExists)
    {
      throw new ArgumentException($"A customer with this phone number {maskedPhoneNumber} already exists.");
    }
  }

  private void ValidateUniqueEmail(string email)
  {
    bool emailExists = _customers.Any(c => c.Email == email);
    if (emailExists)
    {
      throw new ArgumentException("A customer with this email address already exists.");
    }
  }

  private Customer GetCustomerById(string customerId)
  {
    Customer customer = _customers.FirstOrDefault(c => c.CustomerId == customerId);
    if (customer == null)
    {
      throw new ArgumentException("Customer not found");
    }
    return customer;
  }

  private bool CustomerHasAccountType(Customer customer, string accountType)
  {
    return customer.Accounts.Any(account => account.AccountType.Equals(accountType, StringComparison.OrdinalIgnoreCase));
  }

  private void SaveData()
  {
    _storage.Save(_customers);
  }

  private string GenerateTransactionId()
  {
    int maxNumber = 0;
    string transactionId = "TXN-" + DateTime.Now.ToString("yyyyMMdd") + "-";

    foreach (Customer customer in _customers)
    {
      foreach (Account account in customer.Accounts)
      {
        foreach (Transaction transaction in account.Transactions)
        {
          string[] splittedTransactionId = transaction.TransactionId.Split('-');
          int lastSequence = int.Parse(splittedTransactionId[2]);
          if (lastSequence > maxNumber)
          {
            maxNumber = lastSequence;
          }
        }
      }
    }
    maxNumber++;
    transactionId += $"{maxNumber:D4}";
    return transactionId;
  }

  private void VerifyPin(Account account, string pin)
  {
    if (!account.IsPinValid(pin))
    {
      throw new UnauthorizedAccessException("The PIN you entered is not correct");
    }
  }
}