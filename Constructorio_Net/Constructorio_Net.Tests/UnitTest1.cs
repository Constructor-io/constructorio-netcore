using NUnit.Framework;

namespace Constructorio_Net.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async void Test1()
        {
            ConstructorIO constructorIO = new ConstructorIO("", "", true, "", "");
            ConstructorItem item = new ConstructorItem("Random Item Name");
            item.setUrl("Random Url");
            var response = await constructorIO.addItem(item, "Products");
            var z = "Welcome Admin";
            Assert.AreEqual("Welcome Admin.", z);
            //Assert.Pass();
        }
    }
}