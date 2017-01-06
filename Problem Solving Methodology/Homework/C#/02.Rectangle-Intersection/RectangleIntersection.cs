using System;
using System.Collections.Generic;
using System.Linq;

namespace _02.Rectangle_Intersection
{
    // Solution courtesy of 
    // https://stackoverflow.com/questions/244452/what-is-an-efficient-algorithm-to-find-area-of-overlapping-rectangles/34624421#34624421 (whole line)
    class RectangleIntersection
    {
        static void Main()
        {
            Rectangle[] rects = ReadRectangles();

            int overlapArea = GetArea(rects, true);
            Console.WriteLine(overlapArea);
        }
        static int GetArea(Rectangle[] rects, bool overlapOrTotal)
        {
           // PrintArr(rects);

            // Step 1 : Create two wrappers for every rectangle
            RW[] rws = GetWrappers(rects);

           // PrintArr(rws);

            // Step 2 : Sort rectangles by their x-coordinates
            Array.Sort(rws, new XComp());

           // PrintArr(rws);

            // Step 3 : Group the rectangles in every range by X
            Dictionary<Range, List<Rectangle>> rangeGroups = GroupRects(rws, true);

            // Step 4 : Iterate through each of the pairs and their rectangles
            int sum = 0;
            foreach (var range in rangeGroups.Keys) 
            {
                List<Rectangle> rangeRects = rangeGroups[range];
                sum += GetOverlapOrTotalArea(rangeRects, range, overlapOrTotal);   // for each range of sorted rectangles by X, sort them by Y and calculate the area
            }

            return sum;
        }

        private static int GetOverlapOrTotalArea(List<Rectangle> rangeRects, Range range, bool isOverlap)
        {
            // Horizontal sweep the lines similar to the vertical sweep we did above

            // Step 1 : Create wrappers again
            RW[] rws = GetWrappers(rangeRects);

            // Step 2 : Sort rectangles by their y-coordinates
            Array.Sort(rws, new YComp());

            // Step 3 : Group the rectangles in every range by Y
            Dictionary<Range, List<Rectangle>> yRangeGroups = GroupRects(rws, false);

            // Step 4 : Iterate through every range, if there are more than one rectangles then calculate their area only once
            int sum = 0;

            foreach (var yRange in yRangeGroups.Keys)
            {
                List<Rectangle> yRangeRects = yRangeGroups[yRange];

                if (isOverlap)
                {
                    if (yRangeRects.Count > 1) // we need at least two rectangles to have an overlapping area
                        sum += GetArea(range, yRange);
                }
                else
                    if (yRangeRects.Count > 0) // we need at least one rectangle to have an area
                      sum += GetArea(range, yRange);
            }

            return sum;
        }
        static int GetArea(Range r1, Range r2)
        {
            return (r2.end - r2.start) * (r1.end - r1.start);
        }
        private static Dictionary<Range, List<Rectangle>> GroupRects(RW[] sortedRWs, bool isX)
        {
            // Group the Rectangle Wrappers by either X or Y coordinates
            Dictionary<Range, List<Rectangle>> rangeGroups = new Dictionary<Range, List<Rectangle>>();
            List<Rectangle> rangeRects = new List<Rectangle>();

            int i = 0;
            int prev = int.MaxValue;

            // iterate through all the sorted wrappers and group them
            while(i < sortedRWs.Length)
            {
                // Get X or Y coords for current rectangle, depending on what we're grouping by
                int curr = isX ? (sortedRWs[i].start ? sortedRWs[i].rect.minX : sortedRWs[i].rect.maxX) :
                    (sortedRWs[i].start ? sortedRWs[i].rect.minY : sortedRWs[i].rect.maxY);

                if(prev < curr)
                {
                    Range nRange = new Range(prev, curr);
                    rangeGroups.Add(nRange, rangeRects);
                    rangeRects = new List<Rectangle>(rangeRects); // reset the list
                }

                prev = curr;

                if (sortedRWs[i].start) // add to the range if we're at the start coord
                    rangeRects.Add(sortedRWs[i].rect);
                else // remove from the range if we're at the end coord
                    rangeRects.Remove(sortedRWs[i].rect);

                i++;
            }

            return rangeGroups;
        }

        static RW[] GetWrappers(Rectangle[] rects)
        {
            // Gets the start and end coord of each rect
            RW[] wrappers = new RW[rects.Length * 2];

            for (int i = 0, index = 0; i < rects.Length; i++, index+=2)
            {
                wrappers[index] = new RW(rects[i], true); // start of rect (min x min y)
                wrappers[index + 1] = new RW(rects[i], false); // end of rect (max x max y)
            }

            return wrappers;
        }
        static RW[] GetWrappers(List<Rectangle> rects)
        {
            // Same as above, just get them from a list not an array
            RW[] wrappers = new RW[rects.Count * 2];

            for (int i = 0, index = 0; i < rects.Count; i++, index += 2)
            {
                wrappers[index] = new RW(rects[i], true);
                wrappers[index + 1] = new RW(rects[i], false);
            }
            return wrappers;
        }
        static void PrintArr(Object[] rects)
        {
            // Used to keep track
            for (int i = 0; i < rects.Length; i++)
            {
                Console.WriteLine(rects[i]);
            }
            Console.WriteLine();
        }
        static Rectangle[] ReadRectangles()
        {
            // Turn the input into an array of rectangles
            int numberOfRectangles = int.Parse(Console.ReadLine());
            Rectangle[] rectangles = new Rectangle[numberOfRectangles];

            for (int i = 0; i < numberOfRectangles; i++)
            {
                int[] coordinates = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(c => int.Parse(c)).ToArray();

                int minX = coordinates[0];
                int maxX = coordinates[1];

                int minY = coordinates[2];
                int maxY = coordinates[3];

                rectangles[i] = new Rectangle(minX, minY, maxX, maxY);
            }

            return rectangles;
        }
    }
    
    class XComp : IComparer<RW> // Used as a comparer setting to sort the array by the X coordinate
    {
        public int Compare(RW rw1, RW rw2)
        {
            int x1 = -1;
            int x2 = -1;
            
            
            if (rw1.start) // Chooses which X to compare
            {
                x1 = rw1.rect.minX;
            }
            else
                x1 = rw1.rect.maxX;

            if (rw2.start) // Chooses which X to compare
                x2 = rw2.rect.minX;
            else
                x2 = rw2.rect.maxX;

            return x1.CompareTo(x2);
        }
    }
    class YComp : IComparer<RW> // Used as a comparer setting to sort the array by the Y coordinate
    {
        public int Compare(RW rw1, RW rw2)
        {
            int y1 = -1;
            int y2 = -1;

            
            if (rw1.start) // Chooses which Y to compare
                y1 = rw1.rect.minY;
            else
                y1 = rw1.rect.maxY;

            if (rw2.start) // Chooses which Y to compare
                y2 = rw2.rect.minY;
            else
                y2 = rw2.rect.maxY;

            return y1.CompareTo(y2);
        }
    }
    class Rectangle
    {
        public int minX;
        public int minY;
        public int maxX;
        public int maxY;

        public Rectangle(int minX, int minY, int maxX, int maxY)
        {
            this.minX = minX;
            this.minY = minY;
            this.maxX = maxX;
            this.maxY = maxY;    
        }
        public Range getBottomLeft()
        {
            return new Range(minX, minY);
        }
        public Range getTopRight()
        {
            return new Range(maxX, maxY);
        }
        public override int GetHashCode()
        {
            return (minX + minY + maxX + maxY) / 4;
        }
        public override bool Equals(object obj)
        {
            Rectangle otherRect = (Rectangle)obj;
            return otherRect.minX == this.minX && otherRect.minY == this.minY && otherRect.maxX == this.maxX && otherRect.maxY == this.maxY;
        }
        public override string ToString()
        {
            return string.Format("minX = {0}, minY = {1}, maxX : {2}, maxY : {3}", minX, minY, maxX, maxY);
        }
    }
    class RW
    {
        // Rectangle Wrapper
        public Rectangle rect;
        public bool start;

        public RW(Rectangle rect, bool start)
        {
            this.rect = rect;
            this.start = start;
        }
        public override int GetHashCode()
        {
            return rect.GetHashCode() + (start ? 1 : 0);
        }
        public override bool Equals(object obj)
        {
            RW other = (RW)obj;
            return other.start == this.start && other.rect.Equals(this.rect);
        }
        public override string ToString()
        {
            return "Rectangle : " + rect.ToString() + ", start = " + this.start;
        }
    }
    class Range
    {
        public int start;
        public int end;

        public Range(int start, int end)
        {
            this.start = start;
            this.end = end;
        }
        public override int GetHashCode()
        {
            return (start + end) / 2;
        }
        public override bool Equals(object obj)
        {
            Range otherRange = (Range)obj;
            return otherRange.start == this.start && otherRange.end == this.end;
        }
        public override string ToString()
        {
            return string.Format("Start = {0}, End = {1}", start, end);
        }
    }
}