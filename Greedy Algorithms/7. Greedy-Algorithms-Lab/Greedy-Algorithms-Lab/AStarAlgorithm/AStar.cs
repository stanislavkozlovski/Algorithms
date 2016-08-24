namespace AStarAlgorithm
{
    using System;
    using System.Collections.Generic;

    public class AStar
    {
        private readonly PriorityQueue<Node> openNodesByFCost;
        private readonly HashSet<Node> visitedSet;
        private readonly Node[,] graph;
        private readonly char[,] map;

        public AStar(char[,] map)
        {
            this.map = map;
            this.graph = new Node[map.GetLength(0), map.GetLength(1)];
            this.openNodesByFCost = new PriorityQueue<Node>();
            this.visitedSet = new HashSet<Node>();
        }

        public List<int[]> FindShortestPath(int[] startCoords, int[] endCoords)
        {
            Node startNode = this.GetNode(startCoords[0], startCoords[1]);
            startNode.GCost = 0;
            this.openNodesByFCost.Enqueue(startNode);

            while(openNodesByFCost.Count > 0)
            {
                Node currentNode = this.openNodesByFCost.ExtractMin();
                this.visitedSet.Add(currentNode);

                //Check if we're at the end node
                if (currentNode.Row == endCoords[0] && currentNode.Col == endCoords[1])
                {
                    return ReconstructPath(currentNode);
                }

                List<Node> neighbours = this.GetNeighbours(currentNode);
                foreach (Node neighbour in neighbours)
                {
                    if (!visitedSet.Contains(neighbour)) //  if it's not visited
                    {
                        var gCost = currentNode.GCost + CalculateGCost(neighbour, currentNode);
                        if(gCost < neighbour.GCost)
                        {
                            neighbour.GCost = gCost;
                            neighbour.Parent = currentNode;

                            if (!this.openNodesByFCost.Contains(neighbour)) // if he's not in the queue
                            {
                                neighbour.HCost = CalculateHCost(neighbour, endCoords);
                                this.openNodesByFCost.Enqueue(neighbour);
                            }
                            else // its in the queue
                                this.openNodesByFCost.DecreaseKey(neighbour); // reorder
                        }
                    }
                }
            }


            //No path found
            return new List<int[]>(0);
        }
        private List<Node> GetNeighbours(Node node)
        {
            List<Node> neighbours = new List<Node>();

            int maxRow = this.graph.GetLength(0);
            int maxCol = this.graph.GetLength(1);

            for (int row = node.Row-1; row <= node.Row + 1 && row < maxRow; row++)
            {
                if (row < 0) continue;

                for (int col = node.Col-1; col <= node.Col + 1 && col < maxCol; col++)
                {
                    if(col >= 0 && col < maxCol // not out of bounds
                        && map[row,col] != 'W' // not a wall
                        && !(row == node.Row && col == node.Col)) // not same node
                    {
                        Node neighbour = this.GetNode(row, col);
                        neighbours.Add(neighbour);
                    }
                }
            }

            return neighbours;
        }
        private Node GetNode(int row, int col)
        {
            return this.graph[row, col] ?? (this.graph[row, col] = new Node(row, col));
        }
        private static int CalculateGCost(Node node, Node previous)
        {
            return GetDistance(node.Row, node.Col, previous.Row, previous.Col);
        }
        private static int CalculateHCost(Node node, int[] endNodeCoords)
        {
            return GetDistance(node.Row, node.Col, endNodeCoords[0], endNodeCoords[1]);
        }
        private static int GetDistance(int r1, int c1, int r2, int c2)
        {
            var deltaX = Math.Abs(c1 - c2); // absolute X distance
            var deltaY = Math.Abs(r1 - r2); // absolute Y distance

            if (deltaX > deltaY) // diagonal until we're on the same row... I think
                return 14 * deltaY + 10 * (deltaX - deltaY);
            else
                return 14 * deltaX + 10 * (deltaY - deltaX);
        }
        private static List<int[]> ReconstructPath(Node currentNode)
        {
            var cells = new List<int[]>();

            while(currentNode != null)
            {
                cells.Add(new[] { currentNode.Row, currentNode.Col });
                currentNode = currentNode.Parent;
            }

            return cells;
        }
    }
}
