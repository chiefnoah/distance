using NUnit.Framework;
using Distance;

namespace Distance.Tests
{
    ///Please see nodes.png for the diagram of this test node
    public class Tests
    {
        private Node testRoot;
        [SetUp]
        public void Setup()
        {
            testRoot = new Node();
            testRoot.Key = 0;
            testRoot.Left = new Node();
            testRoot.Right = new Node();
            testRoot.Right.Key = 4;
            testRoot.Left.Key = 1;
            testRoot.Left.Left = new Node();
            testRoot.Left.Left.Key = 2;
            testRoot.Left.Right = new Node();
            testRoot.Left.Right.Key = 3;
            testRoot.Right.Left = new Node();
            testRoot.Right.Left.Key = 5;
            testRoot.Right.Left.Left = new Node();
            testRoot.Right.Left.Left.Key = 6;
            testRoot.Right.Right = new Node();
            testRoot.Right.Right.Key = 7;
            testRoot.Right.Right.Left = new Node();
            testRoot.Right.Right.Left.Key = 8;
        }

        [Test]
        public void SiblingNodes()
        {
            int expectedDistance = 2;
            int nodeId1 = 1;
            int nodeId2 = 4;
            int resultDistance = Distance.Program.FindDistance(testRoot, nodeId1, nodeId2);
            Assert.AreEqual(resultDistance, expectedDistance);
        }

        [Test]
        public void FurthestDistance()
        {
            int expectedDistance = 5;
            int nodeId1 = 2;
            int nodeId2 = 8;
            int resultDistance = Distance.Program.FindDistance(testRoot, nodeId1, nodeId2);
            Assert.AreEqual(resultDistance, expectedDistance);
        }

        [Test]
        public void RootToLeaf()
        {
            int expectedDistance = 3;
            int nodeId1 = 0;
            int nodeId2 = 6;
            int resultDistance = Distance.Program.FindDistance(testRoot, nodeId1, nodeId2);
            Assert.AreEqual(resultDistance, expectedDistance);
        }

        [Test]
        public void BadId()
        {
            int expectedDistance = -1;
            int nodeId1 = 11;
            int nodeId2 = 4;
            int resultDistance = Distance.Program.FindDistance(testRoot, nodeId1, nodeId2);
            Assert.AreEqual(resultDistance, expectedDistance);
        }

        [Test]
        public void BothLeafsNodeNotLCA()
        {
            int expectedDistance = 4;
            int nodeId1 = 6;
            int nodeId2 = 8;
            int resultDistance = Distance.Program.FindDistance(testRoot, nodeId1, nodeId2);
            Assert.AreEqual(resultDistance, expectedDistance);
        }

        [Test]
        public void NonLeafNodes()
        {
            int expectedDistance = 3;
            int nodeId1 = 5;
            int nodeId2 = 8;
            int resultDistance = Distance.Program.FindDistance(testRoot, nodeId1, nodeId2);
            Assert.AreEqual(resultDistance, expectedDistance);
        }
    }
}