using System;

public class Edge : IComparable<Edge>
{
    public string StartNode { get; set; }
    public string EndNode { get; set; }
    public int Weight { get; set; }

    public Edge(string startNode, string endNode, int weight)
    {
        this.StartNode = startNode;
        this.EndNode = endNode;
        this.Weight = weight;
    }

    public int CompareTo(Edge other)
    {
        int weightCompared = this.Weight.CompareTo(other.Weight);
        return weightCompared;
    }

    public override string ToString()
    {
        return string.Format("({0} {1}) -> {2}", 
            this.StartNode, this.EndNode, this.Weight);
    }
}
