using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* INPUT:
Roads:
Sofia Plovdiv 145.4 90
Plovdiv Varna 361.4 120.5
Varna Burgas 114.95 30
Burgas Plovdiv 252.9 42
Records:
Varna CA1234AA 19:48:25
Burgas B4732AH 19:38:50
Sofia CA1234AA 08:32:18
Plovdiv A777777 15:28:56
Varna SP33D 02:24:18
Burgas A777777 18:42:15
Plovdiv CA1234AA 15:32:18
Sofia SP33D 04:32:51
Varna B4732AH 08:18:36
End
*/
class FastAndFurious
{
    static HashSet<string> speedingCars = new HashSet<string>();
    static Dictionary<string, City> cities;
    static Dictionary<City, Dictionary<City, Tuple<double, double>>> graph;
    static Dictionary<City, Dictionary<City, Tuple<double, double>>> tempG;
    static Dictionary<string, List<Tuple<City, string>>> cars = new Dictionary<string, List<Tuple<City, string>>>();
    static Dictionary<Tuple<City, City>, double> minTime = new Dictionary<Tuple<City, City>, double>();
    static StringBuilder sb = new StringBuilder();
    static void Main()
    {
        ReadInputAndBuildGraph();
        string input = Console.ReadLine();
        while (input != "End")
        {
            var values = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            City city = cities[values[0]];
            string car = values[1];
            string time = values[2];

            if (!cars.ContainsKey(car))
            {
                cars[car] = new List<Tuple<City, string>>();
                cars[car].Add(new Tuple<City, string>(city, time));
            }
            else
            {
                cars[car].Add(new Tuple<City, string>(city, time));
            }
            input = Console.ReadLine();
        }
        tempG = graph;
        GetMinimumTime();
        CheckCars();
        PrintSpeedingCars();
    }
    static void GetMinimumTime()
    {
        foreach (var cityA in cities.Values)
        {
            foreach (var cityB in cities.Values)
            {
                if (cityA != cityB && !minTime.ContainsKey(new Tuple<City, City>(cityA, cityB)) && !minTime.ContainsKey(new Tuple<City, City>(cityB, cityA)))
                {
                    double minimum = DijkstraCheck(cityA, cityB);
                    minTime[new Tuple<City, City>(cityA, cityB)] = minimum;
                    minTime[new Tuple<City, City>(cityB, cityA)] = minimum;
                }
            }
        }
    }
    static void CheckCars()
    {
        foreach (var car in cars)
        {
            foreach (var record in car.Value)
            {
                var diffTownRecordsQuery = from rec in car.Value // gets all the other records of the car
                                           where rec.Item1.Name != record.Item1.Name // where they arent from the same town
                                           select rec;

                // Compares the records
                foreach (var recordKey in diffTownRecordsQuery)
                {
                    // GET MINIMUM TIME FROM DP
                    double minimumTime = minTime[new Tuple<City, City>(record.Item1, recordKey.Item1)];

                    // Calculate the time it took the car
                    var timeA = TimeSpan.Parse(record.Item2);
                    var timeB = TimeSpan.Parse(recordKey.Item2);
                    var timeItTook = timeA.Subtract(timeB).Duration();
                    double timeForCar = timeItTook.TotalHours;

                    if (timeForCar < minimumTime) // check if the time it took the car was less than the legal minimum
                        speedingCars.Add(car.Key); // If it was, he was speeding
                }

                // DP later on?
            }
        }
    }
    static double DijkstraCheck(City source, City destination)
    {
        Dictionary<string, double> distance = new Dictionary<string, double>();
        Dictionary<string, bool> used = new Dictionary<string, bool>();
        Dictionary<string, string> prev = new Dictionary<string, string>();
        foreach (var city in cities)
        {
            distance[city.Key] = double.PositiveInfinity;
            used[city.Key] = false;
            prev[city.Key] = "";
        }
        distance[source.Name] = 0;

        while (true)
        {
            // Find the nearest unused node from source
            double minDistance = double.PositiveInfinity;
            string minNode = "";
            foreach (var city in distance)
            {
                if (!used[city.Key] && city.Value < minDistance)
                {
                    minDistance = city.Value;
                    minNode = city.Key;
                }
            }

            // TODO: MODIFY ALGORITHM TO INCORPORATE DIFFERENT SPEEDLIMITS AND DISTANCE
            // Done-need to test
            if (minDistance == double.PositiveInfinity) // no minimum distance found, everything's been traversed, we're done
                break;

            used[minNode] = true;
            City minCity = cities[minNode];
            // Calculate the distances through MinNode
            foreach (var cityB in graph[minCity].Keys)
            {
                double km = graph[minCity][cityB].Item1;
                double maxSpeed = graph[minCity][cityB].Item2;
                double minimumTime = km / maxSpeed;

                if (distance[cityB.Name] > distance[minNode] + minimumTime)
                {
                    distance[cityB.Name] = distance[minNode] + minimumTime;
                    prev[cityB.Name] = minNode;
                }
            }
        }

        if (distance[destination.Name] == double.PositiveInfinity) // if so, no path has been found
            return -1;

        // Reconstruct the shortest path from sourceNode to destinationNode
        var path = new List<string>();
        string currentNode = destination.Name;
        while (currentNode != "")
        {
            path.Add(currentNode);
            currentNode = prev[currentNode];
        }
        path.Reverse();

        // Get the minimum time
        double minimumTimeForTrip = 0;
        for (int i = 0; i < path.Count - 1; i++)
        {
            City cityA = cities[path[i]];
            City cityB = cities[path[i + 1]];
            double km = graph[cityA][cityB].Item1;
            double maxSpeed = graph[cityA][cityB].Item2;
            double minimumTime = km / maxSpeed;
            minimumTimeForTrip += minimumTime;

            if (source != cityB && !minTime.ContainsKey(new Tuple<City, City>(source, cityB)) && !minTime.ContainsKey(new Tuple<City, City>(cityB, source)))
            {
                double minimum = minimumTime;
                minTime[new Tuple<City, City>(source, cityB)] = minimum;
                minTime[new Tuple<City, City>(cityB, source)] = minimum;
            }
        }

        return minimumTimeForTrip;
    }
    static void ReadInputAndBuildGraph()
    {
        cities = new Dictionary<string, City>();
        graph = new Dictionary<City, Dictionary<City, Tuple<double, double>>>();

        Console.ReadLine(); // read the "Roads:"
        string input = Console.ReadLine();
        while (input != "Records:")
        {
            var values = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            string townA = values[0];
            if (!cities.ContainsKey(townA))
                cities[townA] = new City(townA);
            City aTown = cities[townA];

            string townB = values[1];
            if (!cities.ContainsKey(townB))
                cities[townB] = new City(townB);
            City bTown = cities[townB];

            double distance = double.Parse(values[2]);
            double maxSpeed = double.Parse(values[3]);

            if (!graph.ContainsKey(aTown))
            {
                graph[aTown] = new Dictionary<City, Tuple<double, double>>();
                if (!graph[aTown].ContainsKey(bTown))
                {
                    graph[aTown][bTown] = new Tuple<double, double>(distance, maxSpeed);
                }
            }
            else // contains the key
            {
                if (!graph[aTown].ContainsKey(bTown))
                {
                    graph[aTown][bTown] = new Tuple<double, double>(distance, maxSpeed);
                }
            }

            if (!graph.ContainsKey(bTown))
            {
                graph[bTown] = new Dictionary<City, Tuple<double, double>>();
                if (!graph[bTown].ContainsKey(aTown))
                {
                    graph[bTown][aTown] = new Tuple<double, double>(distance, maxSpeed);
                }
            }
            else
            {
                // contains the key
                if (!graph[bTown].ContainsKey(aTown))
                {
                    graph[bTown][aTown] = new Tuple<double, double>(distance, maxSpeed);
                }
            }

            input = Console.ReadLine();
        }
    }
    static void PrintSpeedingCars()
    {
        List<string> orderedSpeedingCars = speedingCars.ToList();
        orderedSpeedingCars.Sort();
        foreach (var car in orderedSpeedingCars)
        {
            sb.AppendLine(car);
        }
        Console.WriteLine(sb.ToString());
    }
}
public class City : IComparable<City>
{
    public City(string name, double distance = double.PositiveInfinity)
    {
        this.Name = name;
        this.DistanceFromStart = distance;
    }
    public string Name { get; set; }
    public double DistanceFromStart { get; set; }
    public int CompareTo(City other)
    {
        return this.DistanceFromStart.CompareTo(other.DistanceFromStart);
    }
}

