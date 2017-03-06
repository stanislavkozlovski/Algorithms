import heapq

"""
Input in the document is fucked
Nodes: 7
Path: 0 – 6
Edges: 10
0 3 85
0 4 88
3 1 95
5 3 98
4 5 99
4 2 14
5 1 5
5 6 90
1 6 100
2 6 95

Nodes: 4
Path: 0 – 1
Edges: 4
0 1 94
0 2 97
2 3 99
3 1 98
"""


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


def build_path(node: int, prev: dict) -> str:
    path = [node]
    while prev[node] != node:
        node = prev[node]
        path.append(node)
    return ' -> '.join(str(p) for p in reversed(path))


node_count = int(input().split('Nodes: ')[-1])
start_node, end_node = [int(p) for p in input().split('Path: ')[-1].split(' – ')]
edge_count = int(input().split('Edges: ')[-1])
graph = {node: [] for node in range(node_count)}
distance = {node: -1 for node in range(node_count)}
distance[start_node] = 0
prev = {node: node for node in range(node_count)}

for _ in range(edge_count):
    node_a, node_b, weight = [int(p) for p in input().split()]
    edge = Edge(node_a, node_b, weight)
    graph[node_a].append(edge)


max_heap = graph[start_node]
heapq._heapify_max(max_heap)
while max_heap:
    safest_edge = heapq._heappop_max(max_heap)
    new_dist = distance[safest_edge.node_a] + safest_edge.weight

    if distance[safest_edge.node_b] < new_dist:  # Found a safer path
        distance[safest_edge.node_b] = new_dist
        # add the new node's edges with updated overall safety
        max_heap.extend(Edge(edge.node_a, edge.node_b, edge.weight + new_dist) for edge in graph[safest_edge.node_b])
        prev[safest_edge.node_b] = safest_edge.node_a

    heapq._heapify_max(max_heap)

print(build_path(end_node, prev))