"""

Nodes: 4
Edges: 5
0 1 9
0 3 4
3 1 6
3 2 11
1 2 5
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


nodes = int(input().split('Nodes: ')[-1])
edge_count = int(input().split('Edges: ')[-1])
added_nodes = set()

graph = {}
parents = {}

edges = []
for _ in range(edge_count):
    node_a, node_b, weight = [int(p) for p in input().split()]
    edge = Edge(node_a, node_b, weight)
    if node_a not in graph:
        graph[node_a] = []
        parents[node_a] = node_a
    if node_b not in graph:
        graph[node_b] = []
        parents[node_b] = node_b

    graph[node_a].append(edge)
    graph[node_b].append(edge)
    edges.append(edge)


def find_root(parents, node):
    parent = parents[node]
    while parent != node:
        node = parent
        parent = parents[parent]
    return parent


def modify_parent(parents, old_root, new_root):
    # O(N)
    for key, val in parents.items():
        if val == old_root:
            parents[key] = new_root

added_edges = []
for edge in sorted(edges):
    a_root, b_root = find_root(parents, edge.node_a), find_root(parents, edge.node_b)
    if a_root != b_root:
        modify_parent(parents, old_root=b_root, new_root=a_root)
        added_edges.append(edge)
print(f'Minimum spanning forest: {sum(e.weight for e in added_edges)}')
print('\n'.join(str(e) for e in added_edges))
