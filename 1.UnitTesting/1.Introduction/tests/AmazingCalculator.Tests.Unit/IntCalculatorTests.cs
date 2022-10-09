using FluentAssertions;
using Xunit;

namespace AmazingCalculator.Tests.Unit;

public class IntCalculatorTests
{
    [Fact]
    public void Add_ShouldAddTwoNumber_WhenNumbersArePositiveIntegers()
    {
        // Arrange
        var sut = new IntCalculator();
        
        // Act
        var result = sut.Add(5, 4);
        
        // Assert
        result.Should().Be(9);
    }
}
