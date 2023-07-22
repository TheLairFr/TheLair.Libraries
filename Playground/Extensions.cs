using TheLair.Extensions.Object;



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
}