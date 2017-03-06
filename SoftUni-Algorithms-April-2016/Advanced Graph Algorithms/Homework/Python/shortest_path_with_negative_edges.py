class Edge:
    def __init__(self, node_a, node_b, weight):
        self.node_a = node_a
        self.node_b = node_b
        self.weight = weight

    def __gt__(self, other):
        return self.weight > other.weight

    def __eq__(self, other):
        return self.weight == other.weight

    def __lt__(self, other):
        return self.weight < other.weight

    def __str__(self):
        return f'({self.node_a}, {self.node_b} -> {self.weight})'


def build_path(node, prev: dict):
    path = []
    visited_nodes = set()
    while node != -1 and node not in visited_nodes:
        visited_nodes.add(node)
        path.append(node)
        node = prev[node]
    return ' -> '.join(str(p) for p in reversed(path))


node_count = int(input().split('Nodes: ')[-1])
start_node, end_node = [int(p) for p in input().split('Path: ')[-1].split(' - ')]
edge_count = int(input().split('Edges: ')[-1])

prev = {node: -1 for node in range(node_count)}
distances = {node: float('inf') for node in range(node_count)}
distances[start_node] = 0
edges = []
for _ in range(edge_count):
    node_a, node_b, weight = [int(p) for p in input().split()]
    edges.append(Edge(node_a, node_b, weight))

for _ in range(node_count-1):
    for edge in edges:
        if distances[edge.node_b] > distances[edge.node_a] + edge.weight:
            distances[edge.node_b] = distances[edge.node_a] + edge.weight
            prev[edge.node_b] = edge.node_a

has_negative_cycle = False
negative_cycle_start = None
for edge in edges:
    if distances[edge.node_b] > distances[edge.node_a] + edge.weight:
        has_negative_cycle = True
        negative_cycle_start = edge.node_a
        break

if not has_negative_cycle:
    print(f'Distance [{start_node} -> {end_node}]: {distances[end_node]}')
    print(f'Path: {build_path(end_node, prev)}')
else:
    print(f'Negative Cycle detected: {}build_path(negative_cycle_start, prev)')

print(distances)