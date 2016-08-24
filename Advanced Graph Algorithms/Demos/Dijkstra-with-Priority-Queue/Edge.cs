public class Edge
{
    public Edge(Node node, double distance)
    {
        this.Node = node;
        this.Distance = distance;
    }

    public Node Node { get; set; }

    public double Distance { get; set; }
}
