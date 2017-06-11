"""
This is a somewhat smarter solution to the bruteforce Minimum Vertex Cover problem,
where we need to identify if a Minimum Vertex Cover of K exists for the given graph.

It uses the substructure lemma that if a graph G has a min vertex cover of K,
then for one edge (u, v), the graph without U or V should have a min vertex cover of K-1

Time Complexity: (2^k * m), where
    m - number of edges
    k - wanted vertex cover
"""


class Node:
    def __init__(self, name):
        self.name = name
        self.is_valid = True  # track if we're using the Node

    def __hash__(self):
        return hash(self.name)

    def __repr__(self):
        return f'{"INVALID" if not self.is_valid else "valid"} Node {self.name}.'

    def __str__(self):
        return self.__repr__()


class Edge:
    def __init__(self, a, b):
        self.a: Node = a
        self.b: Node = b
        self.is_valid = True  # track validity of edge (if its removed or not)

    def __repr__(self):
        return self.__str__()

    def __str__(self):
        return f'{"INVALID" if not self.is_valid else "valid"} Edge {self.a}-{self.b}'


def build_graph_1():
    """
    representing the following graph
          -------------2------3
         /                    |
        5                     |
         \                    |
          -------------1------4

    Min vertex cover is 3
    """

    one = Node(1)
    two = Node(2)
    three = Node(3)
    four = Node(4)
    five = Node(5)

    one_four = Edge(one, four)
    four_three = Edge(four, three)
    three_two = Edge(three, two)
    two_five = Edge(two, five)
    one_five = Edge(one, five)

    edges = [
        one_four, four_three, three_two, two_five, one_five
    ]

    graph = {
        one: [one_five, one_four],
        two: [two_five, three_two],
        three: [three_two, four_three],
        four: [four_three, one_four],
        five: [one_five, two_five]
    }

    return graph, edges


def build_graph_2():
    """
    9 7 8
     \|/
    1-3---2
     /| \
    4 5  6
    The min cover is 1
    """
    nine = Node(9)
    eight = Node(8)
    seven = Node(7)
    six = Node(6)
    five = Node(5)
    four = Node(4)
    three = Node(3)
    two = Node(2)
    one = Node(1)

    nine_three = Edge(nine, three)
    seven_three = Edge(seven, three)
    eight_three = Edge(eight, three)
    two_three = Edge(two, three)
    one_three = Edge(one, three)
    four_three = Edge(four, three)
    six_three = Edge(six, three)
    five_three = Edge(five, three)

    graph = {
        one: [one_three],
        two: [two_three],
        three: [one_three, two_three, four_three, five_three, six_three, seven_three, eight_three, nine_three],
        four: [four_three],
        five: [five_three],
        six: [six_three],
        seven: [seven_three],
        eight: [eight_three],
        nine: [nine_three]
    }
    edges = [nine_three, eight_three, seven_three, six_three, five_three, four_three, two_three, one_three]

    return graph, edges

graph, edges = build_graph_1()
# graph, edges = build_graph_2()


def remove_node(graph, node):
    """ Invalidate the edges of the given node """
    node.is_valid = False
    for edge in graph[node]:
        edge.is_valid = False


def add_node(graph, node):
    """ Validate the edges of the given node """
    node.is_valid = True
    for edge in graph[node]:
        if edge.a.is_valid and edge.b.is_valid:
            edge.is_valid = True


def verify_vertex_cover(graph, edges, k):
    valid_edges = list(filter(lambda x: x.is_valid, edges))
    if k == 0 and len(valid_edges) == 0:
        return True
    for edge in valid_edges:
        node_a, node_b = edge.a, edge.b

        # try without A
        remove_node(graph, node_a)
        is_valid_without_a = verify_vertex_cover(graph, edges, k-1)
        if is_valid_without_a:
            return True
        add_node(graph, node_a)

        # try without B
        remove_node(graph, node_b)
        is_valid_without_b = verify_vertex_cover(graph, edges, k - 1)
        if is_valid_without_b:
            return True
        add_node(graph, node_b)

    return False

print(verify_vertex_cover(graph, edges, 1))
