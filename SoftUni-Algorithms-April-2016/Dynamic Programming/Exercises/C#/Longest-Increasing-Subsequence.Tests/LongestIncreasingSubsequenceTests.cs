using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

using Longest_Increasing_Subsequence;

[TestClass]
public class LongestIncreasingSubsequenceTests
{

    [TestMethod]
    public void Test6Numbers()
    {
        var sequence = new int[] { 30, 1, 20, 2, 3, -1 };
        var expectedSeq = new int[] { 1, 2, 3 };
        var actualSeq = LongestIncreasingSubsequence.
            FindLongestIncreasingSubsequence(sequence);
        CollectionAssert.AreEqual(expectedSeq, actualSeq);
    }

    [TestMethod]
    public void Test11Numbers()
    {
        var sequence = new int[] { 3, 14, 5, 12, 15, 7, 8, 9, 11, 10, 1 };
        var expectedSeq = new int[] { 3, 5, 7, 8, 9, 11 };
        var actualSeq = LongestIncreasingSubsequence.
            FindLongestIncreasingSubsequence(sequence);
        CollectionAssert.AreEqual(expectedSeq, actualSeq);
    }

    [TestMethod]
    public void Test23Numbers()
    {
        var sequence = new int[] { 3, 14, 5, 12, 15, 7, 8, 9, 11, 10,
            1, 12, 13, 14, 20, 15, 30, 16, 17, 40, 18, 19, 20 };
        var expectedSeq = new int[] { 3, 5, 7, 8, 9, 11, 12, 13, 14,
            15, 16, 17, 18, 19, 20 };
        var actualSeq = LongestIncreasingSubsequence.
            FindLongestIncreasingSubsequence(sequence);
        CollectionAssert.AreEqual(expectedSeq, actualSeq);
    }

    [TestMethod]
    public void Test2Numbers()
    {
        var sequence = new int[] { 3, 4 };
        var expectedSeq = new int[] { 3, 4 };
        var actualSeq = LongestIncreasingSubsequence.
            FindLongestIncreasingSubsequence(sequence);
        CollectionAssert.AreEqual(expectedSeq, actualSeq);
    }

    [TestMethod]
    public void Test1Number()
    {
        var sequence = new int[] { 5 };
        var expectedSeq = new int[] { 5 };
        var actualSeq = LongestIncreasingSubsequence.
            FindLongestIncreasingSubsequence(sequence);
        CollectionAssert.AreEqual(expectedSeq, actualSeq);
    }

    [TestMethod]
    public void TestEmptySequence()
    {
        var sequence = new int[] { };
        var expectedSeq = new int[] { };
        var actualSeq = LongestIncreasingSubsequence.
            FindLongestIncreasingSubsequence(sequence);
        CollectionAssert.AreEqual(expectedSeq, actualSeq);
    }

    [TestMethod]
    public void TestEqualNumbers()
    {
        var sequence = new int[] { 1, 1, 1 };
        var expectedSeq = new int[] { 1 };
        var actualSeq = LongestIncreasingSubsequence.
            FindLongestIncreasingSubsequence(sequence);
        CollectionAssert.AreEqual(expectedSeq, actualSeq);
    }

    [TestMethod]
    public void Test3DecreasingNumbers()
    {
        var sequence = new int[] { 3, 2, 1 };
        var expectedSeq = new int[] { 3 };
        var actualSeq = LongestIncreasingSubsequence.
            FindLongestIncreasingSubsequence(sequence);
        CollectionAssert.AreEqual(expectedSeq, actualSeq);
    }

    [TestMethod]
    public void Test3IncreasingNumbers()
    {
        var sequence = new int[] { 1, 2, 3 };
        var expectedSeq = new int[] { 1, 2, 3 };
        var actualSeq = LongestIncreasingSubsequence.
            FindLongestIncreasingSubsequence(sequence);
        CollectionAssert.AreEqual(expectedSeq, actualSeq);
    }

    [TestMethod]
    [Timeout(500)]
    public void TestPerformance5000Numbers()
    {
        var sequence = Enumerable.Range(1, 5000).ToArray();
        sequence[500] = 0;
        sequence[2000] = 0;
        sequence[4999] = 0;
        var actualSeq = LongestIncreasingSubsequence.
            FindLongestIncreasingSubsequence(sequence);
        Assert.AreEqual(4997, actualSeq.Length);
    }
}
