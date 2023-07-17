using TheLair.Extensions.Object;

namespace Playground
{
    [TestClass]
    public class Extensions
    {
        [TestMethod]
        public void ObjectToDictionnary()
        {
            Dictionary<string, object?> dict = new
            {
                Test = 42,
                Truc = "Test",
                OwO = 55f
            }.ObjectToDictionnary();

            Assert.AreEqual(42, dict["Test"]);
            Assert.AreEqual("Test", dict["Truc"]);
            Assert.AreEqual(55f, dict["OwO"]);
        }
    }
}