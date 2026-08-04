#nullable disable

using System;

public class ExceptionPractice
{
  private int _age;

  public ExceptionPractice(int age)
  {
    ValidateAge(age);
    _age = age;
  }

  private void ValidateAge(int age)
  {

    if (age <= 0)
    {
      throw new ArgumentException("Please Enter a valid Age");
    }
  }
}