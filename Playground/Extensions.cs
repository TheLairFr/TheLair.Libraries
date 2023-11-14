using TheLair.Extensions.Object;
using TheLair.Extensions.Task;


namespace Playground;

[TestClass]
public class Extensions
{
    [TestMethod]
    public void ObjectToDictionnary()
    {
        Dictionary<string, object?> dict = new TestClass
        {
            Test = 42,
            Truc = "Test",
            OwO = 55f
        }.ObjectToDictionnary();

        Assert.AreEqual(42, dict["Test"]);
        Assert.AreEqual("Test", dict["Truc"]);
        Assert.AreEqual(55f, dict["OwO"]);
        Assert.AreEqual(3, dict.Count);
    }

    [TestMethod]
    public async Task AwaitableTuple()
    {
        var (test, truc) = await (Task.FromResult(42), Task.FromResult(42));

    }
}