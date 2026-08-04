#nullable disable

using System;

public class Transaction
{
  private string _transactionId;
  private string _transactionType;
  private decimal _amount;
  private DateTime _transactionDate;
  private string _description;
  private decimal _balanceAfterTransaction;

  public Transaction(string transactionId,  string transactionType, decimal amount, DateTime transactionDate,  string description, decimal balanceAfterTran)
  {
    _transactionId = transactionId;
    _transactionType = transactionType;
    _amount = amount;
    _transactionDate = transactionDate;
    _description = description;
    _balanceAfterTransaction = balanceAfterTran;
  }
  
  public string GetTransactionDetails()
  {
    return $"----------------------------------------\nTransaction ID: {_transactionId}\nType: {_transactionType}\nAmount: ${_amount:N2}\nDate: {_transactionDate:g}\nDescription: {_description}\nBalance After: ${_balanceAfterTransaction:N2}\n----------------------------------------\n\n";
  }
}