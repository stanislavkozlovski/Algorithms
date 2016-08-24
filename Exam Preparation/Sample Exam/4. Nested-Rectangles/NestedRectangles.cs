using System;
using System.Collections.Generic;
using System.Linq;

class Rectangle
{
    public string Name { get; set; }
    public int Left { get; set; }
    public int Top { get; set; }
    public int Right { get; set; }
    public int Bottom { get; set; }
    public List<int> ParentRects { get; set; }
    public int ChildRectsCount { get; set; }

    public Rectangle()
    {
        this.ParentRects = new List<int>();
    }
}

class NestedRectangles
{
    static void Main()
    {
        var rects = ReadRectangles();
        BuildGraph(rects);
        string[] longestSeq = FindLongestNestedectsSequence(rects);
        Console.WriteLine(string.Join(" < ", longestSeq));
    }

    static Rectangle[] ReadRectangles()
    {
        var rects = new List<Rectangle>();
        while (true)
        {
            string line = Console.ReadLine();
            if (line == "End")
            {
                break;
            }
            string[] lineTokens = line.Split(new char[] { ' ', ':' },
                StringSplitOptions.RemoveEmptyEntries);
            Rectangle rect = new Rectangle()
            {
                Name = lineTokens[0],
                Left = int.Parse(lineTokens[1]),
                Top = int.Parse(lineTokens[2]),
                Right = int.Parse(lineTokens[3]),
                Bottom = int.Parse(lineTokens[4]),
            };
            rects.Add(rect);
        }
        return rects.ToArray();
    }

    static void BuildGraph(Rectangle[] rects)
    {
        for (int u = 0; u < rects.Length; u++)
        {
            for (int v = 0; v < rects.Length; v++)
            {
                if (u != v && IsRectangleInsideAnother(rects[v], rects[u]))
                {
                    rects[v].ParentRects.Add(u);
                    rects[u].ChildRectsCount++;
                }
            }
        }
    }

    static bool IsRectangleInsideAnother(Rectangle innerRect, Rectangle outerRect)
    {
        bool innerInsideOuter =
            innerRect.Left >= outerRect.Left && innerRect.Left <= outerRect.Right &&
            innerRect.Right >= outerRect.Left && innerRect.Right <= outerRect.Right &&
            innerRect.Top >= outerRect.Bottom && innerRect.Top <= outerRect.Top &&
            innerRect.Bottom >= outerRect.Bottom && innerRect.Bottom <= outerRect.Top;
        return innerInsideOuter;
    }

    static string[] FindLongestNestedectsSequence(Rectangle[] rects)
    {
        // Initialize childrenCount[] and maxSeqLen[] for each rectangle
        const int NotCalculated = -1;
        const int NoNextRect = -1;
        int[] maxSeqLen = new int[rects.Length];
        int[] childrenCount = new int[rects.Length];
        int[] nextRect = new int[rects.Length];
        for (int i = 0; i < maxSeqLen.Length; i++)
        {
            childrenCount[i] = rects[i].ChildRectsCount;
            maxSeqLen[i] = (childrenCount[i] == 0) ? 1 : NotCalculated;
            nextRect[i] = NoNextRect;
        }

        // Calculate the maximal sequence for all nodes, using a
        // combination of topological sorting + Dijkstra's algorithm
        bool[] used = new bool[rects.Length];
        while (true)
        {
            var rectsWithoutChildren =
                Enumerable.Range(0, rects.Length)
                .Where(v => !used[v] && childrenCount[v] == 0);
            if (!rectsWithoutChildren.Any())
            {
                // No rectangle without children -> algorithm finished
                break;
            }

            var currentRect = rectsWithoutChildren.First();
            used[currentRect] = true;

            // Improve maxSeqLen[] and remove the currentNode from the graph
            foreach (var parentRect in rects[currentRect].ParentRects)
            {
                childrenCount[parentRect]--;
                if ((maxSeqLen[currentRect] + 1 > maxSeqLen[parentRect]) ||
                    (maxSeqLen[currentRect] + 1 == maxSeqLen[parentRect] &&
                     rects[currentRect].Name.CompareTo(rects[nextRect[parentRect]].Name) < 0))
                {
                    // A better path to parentRect is found -> improve maxSeqLen[]
                    maxSeqLen[parentRect] = maxSeqLen[currentRect] + 1;
                    nextRect[parentRect] = currentRect;
                }
            }
        }

        // Find the starting rectangle that has the longest sequence
        int startRect = 0;
        for (int i = 0; i < maxSeqLen.Length; i++)
        {
            if ((maxSeqLen[i] > maxSeqLen[startRect]) ||
                (maxSeqLen[i] == maxSeqLen[startRect] && 
                rects[i].Name.CompareTo(rects[startRect].Name) < 0))
            {
                startRect = i;
            }
        }

        // Reconstruct the longest sequence of nested rectangles
        List<string> sequence = new List<string>();
        while (startRect != NoNextRect)
        {
            sequence.Add(rects[startRect].Name);
            startRect = nextRect[startRect];
        }
        
        return sequence.ToArray();
    }
}
