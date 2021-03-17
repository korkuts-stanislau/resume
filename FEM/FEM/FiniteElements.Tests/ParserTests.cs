using FiniteElements.BLL.Parsers;
using FiniteElements.BLL.SolutionTools;
using NUnit.Framework;

namespace FiniteElements.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ElasticitySolutionParserTest()
        {
            ElasticitySolutionInfoParser parser = new ElasticitySolutionInfoParser(@"S:\Data\university\5sem\kskr\lab3\mock_data");
            ElasticitySolutionInfo info = parser.ParseTxt("elasticityInfo.txt");
            Assert.AreEqual(2.1e8, info.Modulus);
            Assert.AreEqual(0.28, info.Coefficient);
        }
    }
}