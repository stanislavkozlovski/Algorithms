using System;
using System.Collections.Generic;
using System.Linq;

public class GraphCycles
{
	public static void Main()
	{
		var graph = new SortedDictionary<int, SortedSet<int>>();

		int numberOfVertices = int.Parse(Console.ReadLine());
		for (int i = 0; i < numberOfVertices; i++)
		{
			var tokens = Console.ReadLine()
				.Split(new[] { ' ', '-', '>', ',' }, StringSplitOptions.RemoveEmptyEntries);
			int source = int.Parse(tokens[0]);
			var children = tokens.Skip(1).Select(int.Parse);

			graph.Add(source, new SortedSet<int>(children));
		}

		bool cyclesFound = false;

		foreach (var nodeA in graph.Keys)
		{
			foreach (var nodeB in graph[nodeA].Where(n => n > nodeA))
			{
				foreach (var nodeC in graph[nodeB].Where(n => n > nodeA))
				{
					if (graph[nodeC].Contains(nodeA) && nodeB != nodeC)
					{
						Console.WriteLine($"{{{nodeA} -> {nodeB} -> {nodeC}}}");
						cyclesFound = true;
					}
				}
			}
		}

		if (!cyclesFound)
		{
			Console.WriteLine("No cycles found");
		}
	}
}
