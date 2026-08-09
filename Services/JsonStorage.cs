#nullable disable

using System;
using System.Text.Json;
using System.IO;

public class JsonStorage
{
  private const string FilePath = "Data/bank.json";

  public void Save(List<Customer> customers)
  {
    JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions();
    jsonSerializerOptions.WriteIndented = true;

    string json = JsonSerializer.Serialize(customers, jsonSerializerOptions);
    File.WriteAllText(FilePath, json);
  }

  public List<Customer> Load()
  {
    if (!File.Exists(FilePath))
    {
      return new List<Customer>();
    }

    string json = File.ReadAllText(FilePath);
    List<Customer> customers = JsonSerializer.Deserialize<List<Customer>>(json);

    return customers ?? new List<Customer>();
  }
}