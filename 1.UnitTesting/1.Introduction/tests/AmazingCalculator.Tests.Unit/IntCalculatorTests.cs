using FluentAssertions;
using Xunit;

namespace AmazingCalculator.Tests.Unit;

public class IntCalculatorTests
{
    [Theory]
    // [InlineData(5, 4, 9)]
    // [InlineData(5, -5, 0)]
    // [InlineData(-5, -5, -10)]
    //[MemberData(nameof(AddData))]
    [ClassData(typeof(AddData))]
    public void Add_ShouldAddTwoNumbers_WhenNumbersAreIntegers(
        int a, int b, int expected)
    {
        // Arrange
        var sut = new IntCalculator();
        
        // Act
        var result = sut.Add(a, b);
        
        // Assert
        result.Should().Be(expected);
    }

    [Fact]
    public void Subtract_ShouldSubtractTwoNumbers_WhenTheNumbersAreIntegers()
    {
        // Arrange
        var sut = new IntCalculator();

        // Act
        var result = sut.Subtract(7, 5);

        // Assert
        result.Should().Be(2);
    }
    
    [Fact]
    public void Multiply_ShouldMultiplyTwoNumbers_WhenTheNumbersArePositiveIntegers()
    {
        // Arrange
        var sut = new IntCalculator();

        // Act
        var result = sut.Multiply(6, 9);
    
        // Assert
        result.Should().Be(54);
    }
    
    [Fact]
    public void Multiply_ShouldReturnZero_WhenOneOfTheNumbersIsZero()
    {
        // Arrange
        var sut = new IntCalculator();

        // Act
        var result = sut.Multiply(7, 0);
    
        // Assert
        result.Should().Be(0);
    }
    
    [Fact]
    public void Divide_ShouldDivideTwoNumbers_WhenNumbersAreDivisible()
    {
        // Arrange
        var sut = new IntCalculator();

        // Act
        var result = sut.Divide(10, 2);
    
        // Assert
        result.Should().Be(5);
    }
    
    [Fact]
    public void Divide_ShouldReturnTheFirstNumber_WhenNumberIsDividedByOne()
    {
        // Arrange
        var sut = new IntCalculator();

        // Act
        var result = sut.Divide(7, 1);
    
        // Assert
        result.Should().Be(7);
    }

    public static IEnumerable<object[]> AddData()
    {
        return new[]
        {
            new object[] { 5, 4, 9 },
            new object[] { 5, -5, 0 },
            new object[] { -5, -5, -10 }
        };
    }
}
