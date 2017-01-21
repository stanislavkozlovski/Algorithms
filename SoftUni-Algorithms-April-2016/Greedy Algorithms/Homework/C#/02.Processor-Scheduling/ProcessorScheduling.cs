using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

class ProcessorScheduling
{
    static void Main(string[] args)
    {
        List<Tuple<int, int>> tasksList = ReadInput();
        List<int> optimalSchedule = new List<int>();
        int totalValue = 0;
        int maxDeadline = tasksList.Max(x => x.Item2);
        for (int i = 1; i <= maxDeadline; i++)
        {
            var maxForThisDeadlineQuery = (from tsk in tasksList
                                            where tsk.Item2 == i
                                            orderby tsk.Item1 descending
                                            select tsk).Take(1).ToArray(); // take the biggest with deadline x

            Tuple<int, int> temp = new Tuple<int, int>(maxForThisDeadlineQuery[0].Item1, maxForThisDeadlineQuery[0].Item2);

            optimalSchedule.Add(tasksList.IndexOf(temp) + 1);
            totalValue += temp.Item1;
        }

        Console.WriteLine("Optimal Schedule: {0}", string.Join(" -> ", optimalSchedule));
        Console.WriteLine("Total value: {0}", totalValue);
    }
    static List<Tuple<int,int>> ReadInput()
    {
        var list = new List<Tuple<int, int>>();
        int tasks = int.Parse(Regex.Match(Console.ReadLine(), @".+?(\d+)").Groups[1].Value);

        for (double i = 0; i < tasks; i++)
        {
            Match match = Regex.Match(Console.ReadLine(), @"(\d+)\s-\s(\d+)");
            list.Add(new Tuple<int, int>(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value)));
        }

        return list;
    }
}

