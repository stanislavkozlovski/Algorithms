using System;
using System.Collections.Generic;
using System.Linq;

class TowerOfHanoi
{
    private static int stepsTaken = 0;
    private static Stack<int> source;
    private static Stack<int> destination = new Stack<int>();
    private static Stack<int> spare = new Stack<int>();
    static void Main()
    {
        source = new Stack<int>(Enumerable.Range(1,3).Reverse());
        
        PrintRods();
        MoveDisks(3, source, destination, spare);
    }
    private static void MoveDisks(int bottomDisk, Stack<int> sourceRod, Stack<int> destinationRod, Stack<int> spareRod)
    {
        if(bottomDisk == 1)
        {
            stepsTaken++;
            destinationRod.Push(sourceRod.Pop());
            Console.WriteLine($"Step #{stepsTaken}: Moved disk {bottomDisk}");
            PrintRods();
        }
        else
        {
            MoveDisks(bottomDisk - 1, sourceRod, spareRod, destinationRod);

            destinationRod.Push(sourceRod.Pop());

            stepsTaken++;
            Console.WriteLine($"Step #{stepsTaken}: Moved disk {bottomDisk}");
            PrintRods();

            MoveDisks(bottomDisk - 1, spareRod, destinationRod, sourceRod);

        }
    }
    private static void PrintRods()
    {
        Console.WriteLine("Source: {0}", string.Join(", ", source.Reverse()));
        Console.WriteLine("Destination: {0}", string.Join(", ", destination.Reverse()));
        Console.WriteLine("Spare: {0}", string.Join(", ", spare.Reverse()));
        Console.WriteLine();
    }
}

