#nullable disable

using System;

public class Transaction
{
  private string _transactionId;
  private string _transactionType;
  private decimal _amount;
  private string _description;
  private decimal _balanceAfterTransaction;
  private DateTime _transactionDate;

  public Transaction(string transactionId, string transactionType, decimal amount, string description, decimal balanceAfterTransaction, DateTime transactionDate)
  {
    _transactionId = transactionId;
    _transactionType = transactionType;
    _amount = amount;
    _description = description;
    _balanceAfterTransaction = balanceAfterTransaction;
    _transactionDate = transactionDate;
  }
  
  public string TransactionId
  {
    get
    {
      return _transactionId;
    }
  }

  public string TransactionType
  {
    get
    {
      return _transactionType;
    }
  }

  public decimal Amount
  {
    get
    {
      return _amount;
    }
  }

  public string Description
  {
    get
    {
      return _description;
    }
  }

  public decimal BalanceAfterTransaction
  {
    get
    {
      return _balanceAfterTransaction;
    }
  }

  public DateTime TransactionDate
  {
    get
    {
      return _transactionDate;
    }
  }
  
  public string GetTransactionDetails()
  {
    return $"----------------------------------------\nTransaction ID: {_transactionId}\nType: {_transactionType}\nAmount: ${_amount:N2}\nDate: {_transactionDate:g}\nDescription: {_description}\nBalance After: ${_balanceAfterTransaction:N2}\n----------------------------------------\n\n";
  }
}