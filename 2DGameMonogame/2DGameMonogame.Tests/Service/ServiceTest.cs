using Microsoft.VisualStudio.TestTools.UnitTesting;
using PacmanMonogame.Services;

namespace _2DGameMonogame.Tests
{
    public class  ServiceTest
    {
        private IService _service;


        public ServiceTest()
        {
            _service= new Service();
        }

        [TestInitialize]
        public void InitTest()
        {
            
        }

        [TestMethod]
        public void SaveKeyInJsonTest()
        {

        }


        [TestMethod]
        public void ReadSavedKeysMenuTest()
        {

        }
    }
}