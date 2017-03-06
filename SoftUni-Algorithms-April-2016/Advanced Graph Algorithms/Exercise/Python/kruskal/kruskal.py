class Node:
    def __init__(self, name):
        self.name = name

    def __eq__(self, other):
        return self.name == other.name

    def __hash__(self):
        return hash(self.name)

    def __str__(self):
        return str(self.name)

    def __repr__(self):
        return self.__str__()

class UndirectedEdge:
    def __init__(self, node_a: Node, node_b: Node, weight: int):
        self.node_a = node_a
        self.node_b = node_b
        self.weight = weight

    def __eq__(self, other):
        return (((self.node_a == other.node_a and self.node_b == other.node_b)
                or (self.node_a == other.node_b and self.node_b == other.node_a))
                and self.weight == other.weight)

    def __hash__(self):
        return hash(str(self.node_a) + str(node_b))

    def __gt__(self, other):
        return self.weight > other.weight

    def __str__(self):
        return f'({self.node_a} {self.node_b}) -> {self.weight}'

    def __repr__(self):
        return self.__str__()

# build the hardcoded graph
node_0 = Node(0)
node_1 = Node(1)
node_2 = Node(2)
node_3 = Node(3)
node_4 = Node(4)
node_5 = Node(5)
node_6 = Node(6)
node_7 = Node(7)
node_8 = Node(8)

edges = [
    # Component #1
    UndirectedEdge(node_0, node_8, 5),
    UndirectedEdge(node_0, node_3, 9),
    UndirectedEdge(node_8, node_3, 20),
    UndirectedEdge(node_0, node_5, 4),
    UndirectedEdge(node_5, node_3, 2),
    UndirectedEdge(node_3, node_6, 8),
    UndirectedEdge(node_8, node_6, 7),
    UndirectedEdge(node_6, node_2, 12),

    # Component #2
    UndirectedEdge(node_4, node_1, 8),
    UndirectedEdge(node_4, node_7, 10),
    UndirectedEdge(node_7, node_1, 7)
]


# node_1 = Node(1)
# node_2 = Node(2)
# node_3 = Node(3)
# node_4 = Node(4)
#
# edges = [
#     UndirectedEdge(node_1, node_2, 2),
#     UndirectedEdge(node_3, node_1, 5),
#     UndirectedEdge(node_2, node_4, 10),
#     UndirectedEdge(node_4, node_1, 4),
#     UndirectedEdge(node_3, node_4, 3),
# ]

def find_root(node, parents):
    while node in parents and parents[node] != node:
        node = parents[node]

    return node

def have_equal_parents(parents: dict, node_a, node_b):
    return find_root(node_a, parents) == find_root(node_b, parents)

def inverse_tree(parents: dict, node):
    if parents[node] == node:
        return
    parent = parents[node]

    inverse_tree(parents, parent)

    parents[parent] = node


def kruskal(edges: [UndirectedEdge]):
    connected_nodes = set()
    used_edges = []
    parents = {}
    for edge in sorted(edges):
        if ((edge.node_a not in connected_nodes or edge.node_b not in connected_nodes)
                or not have_equal_parents(parents, edge.node_a, edge.node_b)):
            connected_nodes.add(edge.node_a)
            connected_nodes.add(edge.node_b)
            if edge.node_b not in parents:
                # node_b is not connected to anything
                parents[edge.node_b] = edge.node_a
                if edge.node_a not in parents:
                    parents[edge.node_a] = edge.node_a
            elif edge.node_a not in parents:
                # node a is not connected to anything
                parents[edge.node_a] = edge.node_b
                if edge.node_b not in parents:
                    parents[edge.node_b] = edge.node_b
            else:
                # change node_a's parent to node_b.
                # to do that, we first need to make node_a the root of its subtree, so we inverse its path
                # up until the root
                inverse_tree(parents, edge.node_a)
                parents[edge.node_a] = edge.node_b
            used_edges.append(edge)

    return used_edges


def build_output_string(used_edges: [UndirectedEdge]) -> str:
    """ Build the given output string"""
    edge_strings = []
    overall_weight = 0
    for edge in used_edges:
        overall_weight += edge.weight
        edge_strings.append(str(edge))

    return f'Minimum spanning forest weight: {overall_weight}\n' + '\n'.join(edge_strings)

used_edges = kruskal(edges)
print(build_output_string(used_edges))

