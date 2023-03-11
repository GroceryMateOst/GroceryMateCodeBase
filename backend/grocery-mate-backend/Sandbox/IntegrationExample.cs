using static grocery_mate_backend.OperatorType;

namespace grocery_mate_backend;

public class IntegrationExample
{
    public double Calculate(double firstNumber, double secondNumber, OperatorType operatorType)
    {
        switch (operatorType)
        {
            case Plus:
                return firstNumber + secondNumber;
            case Minus:
                return firstNumber - secondNumber;
            case Multiply:
                return firstNumber * secondNumber;
            case Divide:
                return firstNumber / secondNumber;
        }

        return -1;
    }
}

public enum OperatorType
{
    Plus = 0,
    Minus = 1,
    Multiply = 2,
    Divide = 3,
}