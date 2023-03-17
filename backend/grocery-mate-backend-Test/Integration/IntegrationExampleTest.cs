
using grocery_mate_backend;
using Xunit;
using Assert = Xunit.Assert;

namespace grocery_mate_backend_Test.Integration;

public class IntegrationExampleTest
{
    private IntegrationExample _integrationExample = new IntegrationExample();

    [Theory]
    [InlineData(1, 1, 2)]
    public void Calculate_Plus(double firstNumber, double secondNumber, double expected)
    {
        //Arrange

        // Act
        double actual = _integrationExample.Calculate(firstNumber, secondNumber, OperatorType.Plus);

        // Assert
        Assert.Equal(expected, actual);
    }
    
    [Theory]
    [InlineData(1, 1, 0)]
    public void Calculate_Minus(double firstNumber, double secondNumber, double expected)
    {
        //Arrange

        // Act
        double actual = _integrationExample.Calculate(firstNumber, secondNumber, OperatorType.Minus);

        // Assert
        Assert.Equal(expected, actual);
    }
    
    [Theory]
    [InlineData(1, 1, 1)]
    [InlineData(1, 2, 2)]
    public void inCalculate_Multiply(double firstNumber, double secondNumber, double expected)
    {
        //Arrange

        // Act
        double actual = _integrationExample.Calculate(firstNumber, secondNumber, OperatorType.Multiply);

        // Assert
        Assert.Equal(expected, actual);
    }
    
    [Theory]
    [InlineData(1, 1, 1)]
    [InlineData(10, 5, 2)]
    public void Calculate_Divide(double firstNumber, double secondNumber, double expected)
    {
        //Arrange

        // Act
        double actual = _integrationExample.Calculate(firstNumber, secondNumber, OperatorType.Divide);

        // Assert
        Assert.Equal(expected, actual);
    }
}