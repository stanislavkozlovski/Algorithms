class Node:
    def __init__(self, name, lead):
        self.name = name
        self.leader = self

    def get_leader(self):
        lead = self.leader
        while lead.leader != lead:
            lead = lead.leader
        return lead

    def __eq__(self, other):
        return self.name == other.name


def build_graph() -> (dict, dict):
    lines = open('input_1.txt').readlines()
    graph = {node: [] for node in range(1, int(lines[0].strip())+1)}
    nodes = {node: Node(node, node) for node in range(1, int(lines[0].strip())+1)}

    for line in lines[1:]:
        node_a, node_b, dist = [int(p) for p in line.split()]
        graph[node_a].append((nodes[node_b], dist))
        graph[node_b].append((nodes[node_a], dist))

    return nodes, graph


def get_closest_edge(nodes, graph) -> (int, int):
    closest_dist = float('inf')
    closest_edge = None
    for node in graph.keys():
        for edge in graph[node]:
            node_b, dist = edge
            if dist < closest_dist and nodes[node].get_leader() != node_b.get_leader():
                closest_dist = dist
                closest_edge = (nodes[node], node_b, closest_dist)

    return closest_edge

k = 4
nodes, graph = build_graph()
print('Built graph')

components = {}
component_count = len(nodes)

while component_count > k:
    edge = get_closest_edge(nodes, graph)  # O(M*2)
    # connect them
    component_count -= 1
    edge[0].get_leader().leader = edge[1].get_leader()
    print(component_count)
edge = get_closest_edge(nodes, graph)  # O(M*2)
print(edge[2])