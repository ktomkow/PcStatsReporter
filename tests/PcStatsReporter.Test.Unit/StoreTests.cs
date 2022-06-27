using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PcStatsReporter.Core.Test.Unit
{
    [TestClass]
    public class StoreTests
    {
        private readonly Store store;

        public StoreTests()
        {
            this.store = new Store();
        }

        [TestMethod]
        public void Get_Int_WhenNothingSet_ThenReturnZero()
        {
            int result = this.store.Get<int>();

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Get_Int_WhenSixSet_ThenReturnSix()
        {
            store.Set(6);
            int result = this.store.Get<int>();

            Assert.AreEqual(6, result);
        }


        [TestMethod]
        public void Get_Panda_WhenNothingSet_ThenReturnNull()
        {
            Panda result = this.store.Get<Panda>();

            Assert.AreEqual(null, result);
        }

        [TestMethod]
        public void Get_Panda_WhenSet_ThenReturnPanda()
        {
            Panda panda = new Panda()
            {
                Name = "Cheese",
                Number = 2.5d
            };

            store.Set(panda);
            panda = null;

            Panda result = this.store.Get<Panda>();

            Assert.AreEqual("Cheese", result.Name);
            Assert.AreEqual(2.5d, result.Number);
        }
        
        [TestMethod]
        public void GetSet_PandaTwoTimes_GetSecondOne()
        {
            Panda pandaOne = new Panda()
            {
                Name = "Cheese",
                Number = 2.5d
            };
            
            Panda pandaTwo = new Panda()
            {
                Name = "Tatanka",
                Number = 33.19d
            };

            store.Set(pandaOne);
            store.Set(pandaTwo);

            Panda result = this.store.Get<Panda>();

            Assert.AreEqual("Tatanka", result.Name);
            Assert.AreEqual(33.19d, result.Number);
        }

        private class Panda
        {
            public string Name { get; set; }
            public double Number { get; set; }
        }
    }
}
