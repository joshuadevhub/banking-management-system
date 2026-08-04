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
  private List<Transaction> _transactions = new List<Transaction>();

  public Account(string acctNumber, string acctPin, string acctType)
  {
    _accountNumber = acctNumber;
    ValidatePin(acctPin);
    _accountPin = acctPin;
    _accountBalance = 0;
    ValidateAccountType(acctType);
    _accountType = acctType;
    _dateCreated = DateTime.Now;
  }

  public bool Deposit(decimal amount)
  {
    if (amount <= 0)
    {
      return false;
    }
    _accountBalance += amount;
    DateTime today = DateTime.Now;
    Transaction newTransaction = new Transaction("xxx103", "Deposit", amount, today, "Cash Deposit", _accountBalance);
    AddTransaction(newTransaction);
    return true;
  }

  public bool Withdraw(decimal amount)
  {
    if (amount <= 0)
    {
      return false;
    }
    if (amount > _accountBalance)
    {
      return false;
    }
    _accountBalance -= amount;
    DateTime today = DateTime.Now;
    Transaction newTransaction = new Transaction("xxx104", "Withdraw", amount, today, "Withdrawal", _accountBalance);
    AddTransaction(newTransaction);
    return true;
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
    string[] allowedAccountType = { "savings", "current" };
    if (!allowedAccountType.Contains(accountType.ToLower()))
    {
      throw new ArgumentException($"Invalid Account Type. Allowed: {string.Join(',', allowedAccountType)}");
    }
  }
}