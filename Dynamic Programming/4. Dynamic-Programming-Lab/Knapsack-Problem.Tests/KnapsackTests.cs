using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

using Knapsack_Problem;

[TestClass]
public class KnapsackTests
{
    [TestMethod]
    public void TestKnapsack7ItemsCapacity19()
    {
        var items = new Item[]
        {
            new Item { Weight = 5, Price = 30},
            new Item { Weight = 8, Price = 120},
            new Item { Weight = 7, Price = 10},
            new Item { Weight = 0, Price = 20},
            new Item { Weight = 4, Price = 50},
            new Item { Weight = 5, Price = 80},
            new Item { Weight = 2, Price = 10},
        };
        var knapsackCapacity = 19;

        var itemsTaken = Knapsack.FillKnapsack(items, knapsackCapacity);
        var totalWeight = itemsTaken.Sum(i => i.Weight);
        var totalPrice = itemsTaken.Sum(i => i.Price);
        Assert.AreEqual(19, totalWeight);
        Assert.AreEqual(280, totalPrice);
    }

    [TestMethod]
    public void TestKnapsack7ItemsCapacity0()
    {
        var items = new Item[]
        {
            new Item { Weight = 5, Price = 30},
            new Item { Weight = 8, Price = 120},
            new Item { Weight = 7, Price = 10},
            new Item { Weight = 0, Price = 20},
            new Item { Weight = 4, Price = 50},
            new Item { Weight = 5, Price = 80},
            new Item { Weight = 2, Price = 10},
        };
        var knapsackCapacity = 0;

        var itemsTaken = Knapsack.FillKnapsack(items, knapsackCapacity);
        var totalWeight = itemsTaken.Sum(i => i.Weight);
        var totalPrice = itemsTaken.Sum(i => i.Price);
        Assert.AreEqual(0, totalWeight);
        Assert.AreEqual(20, totalPrice);
    }

    [TestMethod]
    public void TestKnapsack7ItemsCapacity1()
    {
        var items = new Item[]
        {
            new Item { Weight = 5, Price = 30},
            new Item { Weight = 8, Price = 120},
            new Item { Weight = 7, Price = 10},
            new Item { Weight = 0, Price = 20},
            new Item { Weight = 4, Price = 50},
            new Item { Weight = 5, Price = 80},
            new Item { Weight = 2, Price = 10},
        };
        var knapsackCapacity = 1;

        var itemsTaken = Knapsack.FillKnapsack(items, knapsackCapacity);
        var totalWeight = itemsTaken.Sum(i => i.Weight);
        var totalPrice = itemsTaken.Sum(i => i.Price);
        Assert.AreEqual(0, totalWeight);
        Assert.AreEqual(20, totalPrice);
    }

    [TestMethod]
    public void TestKnapsack7ItemsCapacity2()
    {
        var items = new Item[]
        {
            new Item { Weight = 5, Price = 30},
            new Item { Weight = 8, Price = 120},
            new Item { Weight = 7, Price = 10},
            new Item { Weight = 0, Price = 20},
            new Item { Weight = 4, Price = 50},
            new Item { Weight = 5, Price = 80},
            new Item { Weight = 2, Price = 10},
        };
        var knapsackCapacity = 2;

        var itemsTaken = Knapsack.FillKnapsack(items, knapsackCapacity);
        var totalWeight = itemsTaken.Sum(i => i.Weight);
        var totalPrice = itemsTaken.Sum(i => i.Price);
        Assert.AreEqual(2, totalWeight);
        Assert.AreEqual(30, totalPrice);
    }

    [TestMethod]
    public void TestKnapsack7ItemsCapacity100()
    {
        var items = new Item[]
        {
            new Item { Weight = 5, Price = 30},
            new Item { Weight = 8, Price = 120},
            new Item { Weight = 7, Price = 10},
            new Item { Weight = 0, Price = 20},
            new Item { Weight = 4, Price = 50},
            new Item { Weight = 5, Price = 80},
            new Item { Weight = 2, Price = 10},
        };
        var knapsackCapacity = 100;

        var itemsTaken = Knapsack.FillKnapsack(items, knapsackCapacity);
        var totalWeight = itemsTaken.Sum(i => i.Weight);
        var totalPrice = itemsTaken.Sum(i => i.Price);
        Assert.AreEqual(31, totalWeight);
        Assert.AreEqual(320, totalPrice);
    }

    [TestMethod]
    public void TestKnapsack1ItemEqualCapacity()
    {
        var items = new Item[]
        {
            new Item { Weight = 5, Price = 30},
        };
        var knapsackCapacity = 5;

        var itemsTaken = Knapsack.FillKnapsack(items, knapsackCapacity);
        var totalWeight = itemsTaken.Sum(i => i.Weight);
        var totalPrice = itemsTaken.Sum(i => i.Price);
        Assert.AreEqual(5, totalWeight);
        Assert.AreEqual(30, totalPrice);
    }

    [TestMethod]
    public void TestKnapsack1ItemEnoughCapacity()
    {
        var items = new Item[]
        {
            new Item { Weight = 5, Price = 30},
        };
        var knapsackCapacity = 6;

        var itemsTaken = Knapsack.FillKnapsack(items, knapsackCapacity);
        var totalWeight = itemsTaken.Sum(i => i.Weight);
        var totalPrice = itemsTaken.Sum(i => i.Price);
        Assert.AreEqual(5, totalWeight);
        Assert.AreEqual(30, totalPrice);
    }

    [TestMethod]
    public void TestKnapsack2ItemsUnsufficientCapacity()
    {
        var items = new Item[]
        {
            new Item { Weight = 5, Price = 30},
            new Item { Weight = 10, Price = 55},
        };
        var knapsackCapacity = 3;

        var itemsTaken = Knapsack.FillKnapsack(items, knapsackCapacity);
        var totalWeight = itemsTaken.Sum(i => i.Weight);
        var totalPrice = itemsTaken.Sum(i => i.Price);
        Assert.AreEqual(0, totalWeight);
        Assert.AreEqual(0, totalPrice);
    }
}
