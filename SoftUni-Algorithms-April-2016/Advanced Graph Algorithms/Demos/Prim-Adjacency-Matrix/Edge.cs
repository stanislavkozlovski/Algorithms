public class Edge
{
    public int StartNode { get; set; }
    public int EndNode { get; set; }
    public int Weight { get; set; }

    public Edge(int startNode, int endNode, int weight)
    {
        this.StartNode = startNode;
        this.EndNode = endNode;
        this.Weight = weight;
    }

    public override string ToString()
    {
        return string.Format("({0} {1}) -> {2}", 
            this.StartNode, this.EndNode, this.Weight);
    }
}
