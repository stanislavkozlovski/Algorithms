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

    public override bool Equals(object obj)
    {
        if (obj is Rectangle)
        {
            var r = (Rectangle)obj;
            return
                this.Top == r.Top &&
                this.Left == r.Left &&
                this.Right == r.Right &&
                this.Bottom == r.Bottom;
        }

        return false;
    }

    public override int GetHashCode()
    {
        return this.Top ^ this.Left ^ this.Right ^ this.Bottom;
    }
}

class NestedRectanglesGenerator
{
    static void Main()
    {
        const int randomRectsCount = 700;
        const int nestedRectsCount = 300;
        const int coordRange = 50000;

        // Generate random rectangles
        Random rnd = new Random();
        var names = new HashSet<string>();
        var rects = new HashSet<Rectangle>();
        while (rects.Count < randomRectsCount)
        {
            int leftSide = rnd.Next(-coordRange, coordRange);
            int rightSide = rnd.Next(leftSide + 1, coordRange + 1);
            int bottomSide = rnd.Next(-coordRange, coordRange);
            int topSide = rnd.Next(bottomSide + 1, coordRange + 1);
            string name;
            while (true)
            {
                name = "rect" + rnd.Next(100000);
                if (! names.Contains(name))
                {
                    names.Add(name);
                    break;
                }
            }
            var rect = new Rectangle()
            {
                Name = name,
                Left = leftSide,
                Top = topSide,
                Right = rightSide,
                Bottom = bottomSide
            };
            rects.Add(rect);
        }

        // Generate random rectangles that are guaranteed to be nested
        int left = -50000;
        int right = 50000;
        int bottom = -50000;
        int top = 50000;
        while (rects.Count < randomRectsCount + nestedRectsCount)
        {
            left += rnd.Next(50);
            right -= rnd.Next(50);
            top -= rnd.Next(50);
            bottom += rnd.Next(50);
            string name;
            while (true)
            {
                name = "rect" + rnd.Next(100000);
                if (!names.Contains(name))
                {
                    names.Add(name);
                    break;
                }
            }
            Rectangle rect = new Rectangle()
            {
                Name = name,
                Left = left,
                Top = top,
                Right = right,
                Bottom = bottom
            };
            rects.Add(rect);
        }

        // Print the output
        foreach (var r in rects.OrderBy(r => r.Name.GetHashCode()))
        {
            Console.WriteLine("{0}: {1} {2} {3} {4}", 
                r.Name, r.Left, r.Top, r.Right, r.Bottom);
        }
        Console.WriteLine("End");
    }
}
