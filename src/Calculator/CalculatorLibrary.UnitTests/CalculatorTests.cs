namespace CalculatorLibrary.UnitTests;

public class CalculatorTests
{
    private Calculator sut; // System Under Testing
    
    public CalculatorTests()
    {
        // Arrange
        sut = new Calculator();
    }
    
    // {Method}_{Scenario}_{ExcectedBehavior}
    [Theory]
    [InlineData(1, 2, 3)]
    [InlineData(2, 2, 4)]
    public void Add_PositiveNumbers_ReturnsSummary(int x, int y, int expected)
    {
        // Act
        var result = sut.Add(x, y);
        
        // Assert
        Assert.Equal(expected, result);
    }
    
    [Fact]
    public void Add_NegativeNumbers_ThrowsException()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(()=>sut.Add(-1, 0));
    }
    
}
