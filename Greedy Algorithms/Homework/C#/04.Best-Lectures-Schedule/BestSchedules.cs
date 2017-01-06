using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

class BestSchedules
{
    static Dictionary<Tuple<int, int>, string> lecturesDictionary = new Dictionary<Tuple<int, int>, string>();
    static void Main(string[] args)
    {
        List<Tuple<int, int>> lectures = ReadInput();
        List<Tuple<int, int>> takenLectures = new List<Tuple<int, int>>();


        while (true)
        {
            if (lectures.Count == 0)
                break;
            Tuple<int, int> earliestLecture = lectures.OrderBy(l => l.Item2).Take(1).ToArray()[0];

            if (takenLectures.Count == 0) // first iteration
                takenLectures.Add(earliestLecture);
                
            if (takenLectures.Last().Item2 < earliestLecture.Item1)
            {
                takenLectures.Add(earliestLecture);
            }

            lectures.Remove(earliestLecture);
        }
        PrintResults(takenLectures);
    }
    static void PrintResults(List<Tuple<int,int>> takenLectures)
    {
        Console.WriteLine("Lectures ({0}):", takenLectures.Count);
        foreach (var lecture in takenLectures)
        {
            Console.WriteLine("{0}-{1} -> {2}", lecture.Item1, lecture.Item2, lecturesDictionary[lecture].ToString());
        }
    }
    static List<Tuple<int,int>> ReadInput()
    {
        List<Tuple<int, int>> lectures = new List<Tuple<int, int>>();
        int lecturesCount = int.Parse(Regex.Match(Console.ReadLine(), @".+:\s(\d+)").Groups[1].Value);

        for (int i = 0; i < lecturesCount; i++)
        {
            string lecture = Console.ReadLine();
            Match match = Regex.Match(lecture, @"(?<lectureName>\w+?):\s(?<startTime>\d+?)\s-\s(?<endTime>\d+?)$");
            string lectureName = match.Groups["lectureName"].Value;
            int startTime = int.Parse(match.Groups["startTime"].Value);
            int endTime = int.Parse(match.Groups["endTime"].Value);

            lectures.Add(new Tuple<int, int>(startTime, endTime));
            lecturesDictionary[new Tuple<int, int>(startTime, endTime)] = lectureName;
        }

        return lectures;
    }
}

