class Edge:
    def __init__(self, node_a, node_b, weight):
        self.node_a = node_a
        self.node_b = node_b
        self.weight = weight


"""
The Belllman Ford algorithm goes through the edges N times and tries to update the distance to each node
"""

edges = [
    Edge('a', 'b', -1),
    Edge('a', 'c', 4),
    Edge('b', 'e', 2),
    Edge('b', 'c', 3),
    Edge('b', 'd', 2),
    Edge('d', 'b', 1),
    Edge('d', 'c', 5),
    Edge('e', 'd', -3),
]
nodes = ['a', 'b', 'c', 'd', 'e']
distances = {node: float('inf') for node in nodes}
prev = {node: -1 for node in nodes}
start_node = 'a'
distances[start_node] = 0

for _ in range(len(nodes) - 1):
    for edge in edges:
        new_dist = distances[edge.node_a] + edge.weight
        if distances[edge.node_b] > new_dist:  # found a better way
            distances[edge.node_b] = new_dist
            prev[edge.node_b] = edge.node_a

# Check for negative cycle
for edge in edges:
    new_dist = distances[edge.node_a] + edge.weight
    if distances[edge.node_b] > new_dist:  # found a better way
        raise Exception('Negative Cycle exists!')

print(distances)
