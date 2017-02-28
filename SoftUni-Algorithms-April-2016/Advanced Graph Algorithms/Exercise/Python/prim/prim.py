class Node:
    def __init__(self, name):
        self.name = name
    def __repr__(self):
        return self.name

class Edge:
    def __init__(self, node_a, node_b, weight):
        self.node_a = node_a
        self.node_b = node_b
        self.weight = weight

    def __hash__(self):
        return hash(str(self.node_a) + str(self.node_b) + str(self.weight))

    def __eq__(self, other):
        return self.node_a == other.node_a and self.node_b == other.node_b and self.weight == other.weight

    def __gt__(self, other):
        return self.weight > other.weight

    def __repr__(self):
        return str(self.node_a) + str(self.node_b)


node_a = Node('A')
node_b = Node('B')
node_c = Node('C')
node_d = Node('D')
node_e = Node('E')
node_f = Node('F')

edge_ac = Edge(node_a, node_c, 5)
edge_ab = Edge(node_a, node_b, 4)
edge_ad = Edge(node_a, node_d, 9)
edge_bd = Edge(node_b, node_d, 2)
edge_cd = Edge(node_c, node_d, 20)
edge_ce = Edge(node_c, node_e, 7)
edge_de = Edge(node_d, node_e, 8)
edge_ef = Edge(node_e, node_f, 12)

edges = {
    node_a: [edge_ab, edge_ac, edge_ad],
    node_b: [edge_bd, edge_ab],
    node_c: [edge_ac, edge_ce, edge_cd],
    node_d: [edge_ad, edge_cd, edge_bd, edge_de],
    node_e: [edge_de, edge_ce, edge_ef],
    node_f: [edge_ef]
}

# start from A
connected_set = {node_a}
used_edges = []
edges_to_add: {Edge} = {edge for edge in edges[node_a]}
# TODO: Add ordered set
while edges_to_add:
    min_edge = min(edges_to_add)
    if min_edge.node_a not in connected_set:
        connected_set.add(min_edge.node_a)
        edges_to_add.update({edge for edge in edges[min_edge.node_a]})
        used_edges.append(min_edge)

    elif min_edge.node_b not in connected_set:
        connected_set.add(min_edge.node_b)
        edges_to_add.update({edge for edge in edges[min_edge.node_b]})
        used_edges.append(min_edge)
    edges_to_add.remove(min_edge)
print(used_edges)