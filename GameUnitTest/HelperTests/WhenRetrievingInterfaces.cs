using System.Linq;
using GameLogic.Helpers;
using GameLogic.SkillTree.Paths;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameUnitTest.HelperTests
{
    [TestClass]
    public class WhenRetrievingInterfaces
    {
        [TestMethod]
        public void ShouldRetrieveInterfaces()
        {
            var retriever = new InstanceRetriever<IPath>();
            var paths = retriever.RetrieveInstances();
            Assert.IsNotNull(paths);
            Assert.IsTrue(paths.Any());
        }
    }
}
