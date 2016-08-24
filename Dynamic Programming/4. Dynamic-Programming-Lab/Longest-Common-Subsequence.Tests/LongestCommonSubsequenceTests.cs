using Longest_Common_Subsequence;

using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class LongestCommonSubsequenceTests
{
    [TestMethod]
    public void TestSimilarStrings()
    {
        var first = "Petko Marinov";
        var second = "Pletko Malinov";
        var expectedLCS = "Petko Mainov";
        var actualLCS = LongestCommonSubsequence.
            FindLongestCommonSubsequence(first, second);
        Assert.AreEqual(expectedLCS, actualLCS);
    }

    [TestMethod]
    public void TestSimilarLongStrings()
    {
        var first = "dynamic progamming we brak the oiginal prolem to smaller sub-problms that hae the same strcture";
        var second = "In dynmic prgamming we break th oriinal problem to smaler subprobems that have the sme struture";
        var expectedLCS = "dynmic prgamming we brak th oiinal prolem to smaler subprobms that hae the sme strture";
        var actualLCS = LongestCommonSubsequence.
            FindLongestCommonSubsequence(first, second);
        Assert.AreEqual(expectedLCS, actualLCS);
    }

    [TestMethod]
    public void TestEqualStrings()
    {
        var first = "hello";
        var second = "hello";
        var expectedLCS = "hello";
        var actualLCS = LongestCommonSubsequence.
            FindLongestCommonSubsequence(first, second);
        Assert.AreEqual(expectedLCS, actualLCS);
    }  

    [TestMethod]
    public void TestNonOverlappingStrings()
    {
        var first = "hello";
        var second = "rakiya";
        var expectedLCS = "";
        var actualLCS = LongestCommonSubsequence.
            FindLongestCommonSubsequence(first, second);
        Assert.AreEqual(expectedLCS, actualLCS);
    }

    [TestMethod]
    public void TestSingleLetterOverlappingStrings()
    {
        var first = "hello";
        var second = "beer";
        var expectedLCS = "e";
        var actualLCS = LongestCommonSubsequence.
            FindLongestCommonSubsequence(first, second);
        Assert.AreEqual(expectedLCS, actualLCS);
    }

    [TestMethod]
    public void TestSingleLetter()
    {
        var first = "a";
        var second = "a";
        var expectedLCS = "a";
        var actualLCS = LongestCommonSubsequence.
            FindLongestCommonSubsequence(first, second);
        Assert.AreEqual(expectedLCS, actualLCS);
    }

    [TestMethod]
    [Timeout(500)]
    public void TestPerformance3000Letters()
    {
        var first = "xxxxx" + new string('a', 3000) + "xxxxx";
        var second = "bbb" + new string('a', 3000) + "bbb";
        var expectedLCS = new string('a', 3000);
        var actualLCS = LongestCommonSubsequence.
            FindLongestCommonSubsequence(first, second);
        Assert.AreEqual(expectedLCS, actualLCS);
    }
}
