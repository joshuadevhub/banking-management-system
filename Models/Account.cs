#nullable disable

using System;
using System.Collections.Generic;
using System.Linq;

public class Account
{
  private string _accountNumber;
  private string _accountPin;
  private decimal _accountBalance;
  private string _accountType;
  private DateTime _dateCreated;
  private List<Transaction> _transactions;

  public Account(string accountNumber, string accountPin, decimal accountBalance, string accountType, List<Transaction> transactions, DateTime dateCreated)
  {
    _accountNumber = accountNumber;
    ValidatePin(accountPin);
    _accountPin = accountPin;
    _accountBalance = accountBalance;
    ValidateAccountType(accountType);
    _accountType = accountType;
    _transactions = transactions ?? new List<Transaction>();
    _dateCreated = dateCreated;
  }

  public string AccountNumber
  {
    get
    {
      return _accountNumber;
    }
  }

  public string AccountPin
  {
    get
    {
      return _accountPin;
    }
  }

  public decimal AccountBalance
  {
    get
    {
      return _accountBalance;
    }
  }

  public string AccountType
  {
    get
    {
      return _accountType;
    }
  }

  public DateTime DateCreated
  {
    get
    {
      return _dateCreated;
    }
  }
  
  public List<Transaction> Transactions
  {
    get
    {
      return _transactions;
    }
  }

  public void Deposit(decimal amount, string transactionId)
  {
    if (amount <= 0)
    {
      throw new ArgumentException("Amount should be greater than $0");
    }
    _accountBalance += amount;
    Transaction newTransaction = new Transaction(transactionId, "Deposit", amount, "Cash Deposit", _accountBalance, DateTime.Now);
    AddTransaction(newTransaction);
  }

  public void Withdraw(decimal amount, string transactionId)
  {
    if (amount <= 0)
    {
      throw new ArgumentException("Invalid amount. Withdrawal amount cannot be zero or negative");
    }
    if (amount > _accountBalance)
    {
      throw new ArgumentException("Insufficient funds. Withdrawal amount exceeds your account balance");
    }
    _accountBalance -= amount;
    Transaction newTransaction = new Transaction(transactionId, "Withdraw", amount, "Withdrawal", _accountBalance, DateTime.Now);
    AddTransaction(newTransaction);
  }

  public void ChangePin(string newPin)
  {
    ValidatePin(newPin);
    _accountPin = newPin;
  }

  public string GetAccountNumber()
  {
    return _accountNumber;
  }

  public string GetAccountType()
  {
    return _accountType;
  }

  public decimal GetAccountBalance()
  {
    return _accountBalance;
  }

  public string GetAccountInfo()
  {
    return $"Account Number: {_accountNumber}\nAccount Balance: ${_accountBalance:N2}\nAccount Type: {_accountType}\nDate Created: {_dateCreated:g}";
  }

  public string GetTransactionHistory()
  {
    if(_transactions.Count <= 0)
    {
      return "No Transaction Record at the moment";
    }
    string transactions = "";
    foreach (Transaction transaction in _transactions)
    {
      transactions += transaction.GetTransactionDetails();
    }
    return transactions;
  }
  
  private void AddTransaction(Transaction transaction)
  {
    _transactions.Add(transaction);
  }

  private void ValidatePin(string pin)
  {

    if (pin.Length != 4)
    {
      throw new ArgumentException("Account pin should be 4 characters only");
    }
    foreach (Char c in pin)
    {
      if (!Char.IsDigit(c))
      {
        throw new ArgumentException("Account pin should be digit only");
      }
    }
  }

  private void ValidateAccountType(string accountType)
  {
    string[] allowedAccountType = { "Savings", "Current" };
    if (!allowedAccountType.Any(type => type.Equals(accountType, StringComparison.OrdinalIgnoreCase)))
    {
      throw new ArgumentException($"Invalid Account Type. Allowed: {string.Join(',', allowedAccountType)}");
    }
  }
}