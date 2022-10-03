namespace CloudCustomers.UnitTests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {

    }

    [Theory]
    [InlineData("param1", 1)]
    [InlineData("param2", 1)]
    public void Test2(string param1, string param2) {

    }

}