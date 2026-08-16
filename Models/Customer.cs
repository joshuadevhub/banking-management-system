#nullable disable

using System;
using System.Text.RegularExpressions;
using System.Text.Json.Serialization;

public class Customer
{
  private string _customerId;
  private string _firstName;
  private string _middleName;
  private string _lastName;
  private string _phoneNumber;
  private string _email;
  private Address _address;
  private List<Account> _accounts;
  private DateTime _dateRegistered;
  [JsonConstructor]
  public Customer(string customerId, string firstName, string lastName, string phoneNumber, string email, Address address, DateTime dateRegistered, List<Account> accounts, string middleName = "")
  {
    _customerId = customerId;
    ValidateName(firstName, "First Name");
    _firstName = firstName;
    ValidateNameChar(middleName, "Middle Name");
    _middleName = middleName ?? "";
    ValidateName(lastName, "Last Name");
    _lastName = lastName;
    ValidatePhoneNumber(phoneNumber);
    _phoneNumber = phoneNumber;
    ValidateEmail(email);
    _email = email;
    _address = address;
    _accounts = accounts ?? new List<Account>();
    _dateRegistered =  dateRegistered;
  }

  public string GetCustomerId()
  {
    return _customerId;
  }

  public string CustomerId
  {
    get
    {
      return _customerId;
    }
  }

  public string FirstName
  {
    get
    {
      return _firstName;
    }
  }

  public string LastName
  {
    get
    {
      return _lastName;
    }
  }

  public string MiddleName
  {
    get
    {
      return _middleName;
    }
  }

  public string PhoneNumber
  {
    get
    {
      return _phoneNumber;
    }
  }

  public string Email
  {
    get
    {
      return _email;
    }
  }

  public DateTime DateRegistered
  {
    get
    {
      return _dateRegistered;
    }
  }
  
  public Address Address
  {
    get
    {
      return _address;
    }
  }

  public List<Account> Accounts
  {
    get
    {
      return _accounts;
    }
  }

  public string GetPhoneNumber()
  {
    return _phoneNumber;
  }

  public string GetEmail()
  {
    return _email;
  }

  public void AddAccount(Account account)
  {
    _accounts.Add(account);
  }

  public void ChangePhoneNumber(string phoneNumber)
  {
    ValidatePhoneNumber(phoneNumber);
    _phoneNumber = phoneNumber;
  }

  public void ChangeEmailAddress(string email)
  {
    ValidateEmail(email);
    _email = email;
  }

  public void ChangeAddress(Address address)
  {
    _address = address;
  }

  public Account GetAccount(string accountNumber)
  {
    foreach (Account account in _accounts)
    {
      if (account.GetAccountNumber() == accountNumber)
      {
        return account;
      }
    }
    return null;
  }

  public string GetCustomerInfo()
  {
    return $"Customer ID: {_customerId}\nFirst Name: {_firstName}\nMiddle Name: {_middleName}\nLast Name: {_lastName}\nPhone Number: {_phoneNumber}\nEmail: {_email}\nAddress: {_address.GetFullAddress()}\nDate Registered: {_dateRegistered:g}";
  }

  public List<Account> GetAccounts()
  {
    return _accounts;
  }

  private void ValidatePhoneNumber(string phoneNumber)
  {
    string pattern = @"^(070|080|081|090|091)\d{8}$";
    bool isValidPhone = Regex.IsMatch(phoneNumber, pattern);

    if (!isValidPhone)
    {
      throw new ArgumentException("Pleas enter a valid phone number");
    }
  }

  private void ValidateEmail(string email)
  {
    string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
    bool isEmailValid = Regex.IsMatch(email, emailPattern);

    if (!isEmailValid)
    {
      throw new ArgumentException("Please enter a valid email address");
    }
  }

  private void ValidateName(string name, string fieldName)
  {
    if (string.IsNullOrEmpty(name))
    {
      throw new ArgumentException($"{fieldName} cannot be empty");
    }
    if(name.Length < 3)
    {
      throw new ArgumentException($"{fieldName} must be at least 3 characters long");
    }
    ValidateNameChar(name, fieldName);
  }
  
  private void ValidateNameChar(string name, string fieldName)
  {
    foreach(Char c in name)
    {
      if (Char.IsDigit(c))
      {
        throw new ArgumentException($"{fieldName} cannot include numbers. Please use letters only");
      }
    }
  }
}