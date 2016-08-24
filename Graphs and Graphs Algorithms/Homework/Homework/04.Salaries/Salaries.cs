using System;
using System.Collections.Generic;

class Salaries
{
    static bool[] visited;
    static bool[,] matrixx;
    static long salaries = 0;
    static Dictionary<int, int> predecessorCount = new Dictionary<int, int>();
    static HashSet<int> addedSalaries = new HashSet<int>();
    static long[] salariesArray;
    static void Main()
    {
        // 50/100 initially, peeked at the done code and realized that I only needed to change the variables to long
        matrixx = GetMatrix();
        FindPredecessors();
        foreach (var node in predecessorCount)
        {
            if (node.Value == 0)
            {
                DFS(node.Key);
            }
        }
        Console.WriteLine(salaries);
    }
    static long DFS(int node)
    {
        bool isManager = false;
        long salary = 0L;

        if (!visited[node])
        {
            visited[node] = true;
            for (int i = 0; i < matrixx.GetLength(1); i++)
            {
                if (matrixx[node, i])
                {
                    isManager = true;
                    if(!visited[i])
                        DFS(i);
                    salary += salariesArray[i];
                }
            }

        }
        if (isManager == false)
        {
            if (salariesArray[node] != 0)
                salary += salariesArray[node];
            else
                salary += 1;
        }

        if (!addedSalaries.Contains(node))
        {
            salaries += salary;
            salariesArray[node] = salary;
            addedSalaries.Add(node);
        }

        return salary;
    }
    static void FindPredecessors()
    {
        for (int i = 0; i < matrixx.GetLength(0); i++)
        {
            predecessorCount[i] = 0;
            for (int j = 0; j < matrixx.GetLength(1); j++)
            {
                if (matrixx[i, j])
                {
                    if (!predecessorCount.ContainsKey(j))
                        predecessorCount[j] = 0;
                    predecessorCount[j]++;
                }
            }
        }
    }
    static bool[,] GetMatrix()
    {
        int n = int.Parse(Console.ReadLine());
        bool[,] matrix = new bool[n, n];
        visited = new bool[n];
        salariesArray = new long[n];
        for (int i = 0; i < n; i++)
        {
            string temp = Console.ReadLine();
            for (int j = 0; j < temp.Length; j++)
            {
                if (temp[j] == 'Y')
                    matrix[i, j] = true;
            }
        }
        return matrix;
    }
}

