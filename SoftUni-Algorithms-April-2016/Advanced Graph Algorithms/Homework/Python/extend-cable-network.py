"""
Budget: 20
Nodes: 9
Edges: 15
1 4 8
4 0 6 connected
1 7 7
4 7 10
4 8 3
7 8 4
0 8 5 connected
8 6 9
8 3 20 connected
0 5 4
0 3 9 connected
6 3 8
6 2 12
5 3 2
3 2 14 connected
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


budget = int(input().split('Budget: ')[-1])
nodes = int(input().split('Nodes: ')[-1])
edges = int(input().split('Edges: ')[-1])
added_nodes = set()

graph = {}
for _ in range(edges):
    edge_str = input()
    split_edge_str = edge_str.split(' ')
    node_a = int(split_edge_str[0])
    node_b = int(split_edge_str[1])
    weight = int(split_edge_str[2])
    edge = Edge(node_a, node_b, weight)

    if 'connected' in edge_str:
        added_nodes.update({node_a, node_b})

    if node_a not in graph:
        graph[node_a] = []
    if node_b not in graph:
        graph[node_b] = []
    graph[node_a].append(edge)
    graph[node_b].append(edge)

added_nodes_strings = []
budget_used = 0
potential_edges = []
for node in added_nodes:
    # Only add potential edges which will extend the network
    potential_edges.extend(edge for edge in graph[node] if edge.node_a not in added_nodes or edge.node_b not in added_nodes)

# Prim algorithm to always get the edge
while potential_edges:
    potential_edges = sorted(potential_edges)
    potential_edge = potential_edges.pop(0)

    if potential_edge.node_a in added_nodes and potential_edge.node_b in added_nodes:
        continue
    if budget < potential_edge.weight:
        break

    node_to_add = potential_edge.node_a if potential_edge.node_a not in added_nodes else potential_edge.node_b
    added_nodes.add(node_to_add)
    potential_edges.extend([edge for edge in graph[node_to_add]  # Only add edges that make sense
                            if edge.node_a not in added_nodes
                            or edge.node_b not in added_nodes])

    added_nodes_strings.append(f'{{{potential_edge.node_b}, {potential_edge.node_a}}} -> {potential_edge.weight}')
    budget_used += potential_edge.weight
    budget -= potential_edge.weight

print('\n'.join(added_nodes_strings))
print(f'Budget used: {budget_used}')