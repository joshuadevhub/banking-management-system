#nullable disable

using System;

public class BankingService
{
  private List<Customer> _customers = new List<Customer>();

  Random random = new Random();

  public Customer CreateCustomer(string firstName, string lastName, string phoneNumber, string email, Address address, string middleName = "")
  {
    ValidateUniquePhoneNumber(phoneNumber);
    ValidateUniqueEmail(email);
    string customerId = GenerateCustomerId();
    Customer customer = new Customer(customerId, firstName, lastName, phoneNumber, email, address, middleName);
    AddCustomer(customer);
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
    string customerAccountNumber = GenerateAccountNumber();
    Account newAccount = new Account(customerAccountNumber, accountPin, accountType);
    customer.AddAccount(newAccount);
    return newAccount;
  }

  public void Deposit(string accountNumber, decimal amount)
  {
    Account account = GetAccountByNumber(accountNumber);
    account.Deposit(amount);
  }

  public void Withdraw(string accountNumber, decimal amount)
  {
    Account account = GetAccountByNumber(accountNumber);
    account.Withdraw(amount);
  }

  public Customer FindCustomerByPhoneNumber(string phoneNumber)
  {
    foreach (Customer customer in _customers)
    {
      if (customer.GetPhoneNumber() == phoneNumber)
      {
        return customer;
      }
    }
    throw new ArgumentException($"Customer with this phone number '{phoneNumber}' was not found");
  }

  private string GenerateCustomerId()
  {
    string customerId;
    do
    {
      string year = $"{DateTime.Now.Year.ToString()}-";
      customerId = "CUS-";
      customerId += year;
      for (int i = 0; i < 4; i++)
      {
        string randomNumber = random.Next(0, 10).ToString();
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
        accountNumber += random.Next(0, 10);
      }
    } while (AccountNumberExists(accountNumber));
    return accountNumber;
  }

  public Account GetAccountByNumber(string accountNumber)
  {
    foreach (Customer customer in _customers)
    {
      Account account = customer.GetAccount(accountNumber);
      if (account != null)
      {
        return account;
      }
    }
    throw new ArgumentException("Invalid account number. Account does not exist");
  }
  
  private bool AccountNumberExists(string accountNumber)
  {
    foreach (Customer customer in _customers)
    {
      foreach (Account account in customer.GetAccounts())
      {
        if (account.GetAccountNumber() == accountNumber)
        {
          return true;
        }
      }
    }
    return false;
  }

  private bool CustomerIdExists(string customerId)
  {
    foreach (Customer customer in _customers)
    {
      if (customer.GetCustomerId() == customerId)
      {
        return true;
      }
    }
    return false;
  }

  private void AddCustomer(Customer customer)
  {
    _customers.Add(customer);
  }

  private void ValidateUniquePhoneNumber(string phoneNumber)
  {
    foreach (Customer customer in _customers)
    {
      if (customer.GetPhoneNumber() == phoneNumber)
      {
        throw new ArgumentException("A customer with this phone number already exists.");
      }
    }
  }

  private void ValidateUniqueEmail(string email)
  {
    foreach (Customer customer in _customers)
    {
      if (customer.GetEmail() == email)
      {
        throw new ArgumentException("A customer with this email address already exists.");
      }
    }
  }

  private Customer GetCustomerById(string customerId)
  {
    foreach (Customer customer in _customers)
    {
      if (customer.GetCustomerId() == customerId)
      {
        return customer;
      }
    }
    throw new ArgumentException("Customer not found");
  }

  private bool CustomerHasAccountType(Customer customer, string accountType)
  {
    foreach (Account account in customer.GetAccounts())
    {
      if (account.GetAccountType() == accountType.ToLower())
      {
        return true;
      }
    }
    return false;
  }
}