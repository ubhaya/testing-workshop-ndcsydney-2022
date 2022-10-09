using Xunit;
using Xunit.Abstractions;

namespace AmazingCalculator.Tests.Unit;

public class TestExample : IAsyncLifetime
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly Guid _id = Guid.NewGuid();

    public TestExample(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public async Task Test1()
    {
        await Task.Delay(2000);
        _testOutputHelper.WriteLine(_id.ToString());
        _testOutputHelper.WriteLine("Test1");
    }
    
    [Fact]
    public async Task Test2()
    {
        await Task.Delay(2000);
        _testOutputHelper.WriteLine(_id.ToString());
        _testOutputHelper.WriteLine("Test2");
    }

    public async Task InitializeAsync()
    {
        _testOutputHelper.WriteLine("Setup");
    }

    public async Task DisposeAsync()
    {
        _testOutputHelper.WriteLine("Teardown");
    }
}

public class TestExample2
{
    [Fact]
    public async Task Test1()
    {
        await Task.Delay(2000);
    }
}
