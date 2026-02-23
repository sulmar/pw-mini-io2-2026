namespace CalculatorLibrary;

public class Calculator
{
    public int Add(int a, int b)
    {
        NumberValidator.Validate(a, b);

        return a+b;
    }

 
}


public class NumberValidator
{
    public static void Validate(int a, int b)
    {
        if (a < 0 || b < 0)
            throw new ArgumentException();
    }
}