#nullable disable

using System;

public class Address
{
  private string _houseNumber;
  private string _street;
  private string _city;
  private string _state;
  private string _country;

  public Address(string houseNumber, string street, string city, string state, string country)
  {
    _houseNumber = houseNumber;
    _street = street;
    _city = city;
    _state = state;
    _country = country;
  }

  public string GetHouseNumber()
  {
    return _houseNumber;
  }
  public string GetStreet()
  {
    return _street;
  }
  public string GetCity()
  {
    return _city;
  }
  public string GetState()
  {
    return _state;
  }
  public string GetCountry()
  {
    return _country;
  }

  public string HouseNumber
  {
    get
    {
      return _houseNumber;
    }
  }

  public string Street
  {
    get
    {
      return _street;
    }
  }

  public string City
  {
    get
    {
      return _city;
    }
  }

  public string State
  {
    get
    {
      return _state;
    }
  }

  public string Country
  {
    get
    {
      return _country;
    }
  }

  public string GetFullAddress()
  {
    return $"{_houseNumber} {_street}, {_city}, {_state}, {_country}";
  }
}