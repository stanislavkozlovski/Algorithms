"""
Given a graph, split the nodes into two groups, such that the edges between said groups are minimal.
"""
import random

def build_graph(text_file_name):
    # unique_edges = set()  # for easy checking if an edge already exists
    edges = []  # actual edges will be stored here
    nodes = set()

    with open(text_file_name, 'r') as file:
        for line in file:
            read_nodes = line.strip().split()
            main_node = read_nodes[0]
            adjacent_nodes = read_nodes[1:]

            nodes.add(main_node)
            for adj_node in adjacent_nodes:
                if adj_node in nodes:
                    # edge has already been added
                    continue
                edges.append((main_node, adj_node))
    print(nodes)
    print(edges)
    return nodes, edges
class Node:
    def __init__(self, name):
        self.name: int = name
        self.edge_indexes = set()  # set holding the indexes of the edges where the Node is in
        self.eaten_edges = []

    def __eq__(self, other):
        return self.name == other.name

    def __hash__(self):
        return hash(self.name)

    def __str__(self):
        return str(self.name) + ', ' + str(', '.join(eaten_node for eaten_node in self.eaten_edges)) + '||'

    def __repr__(self):
        return self.__str__()

    def add_edge_index(self, idx):
        self.edge_indexes.add(idx)

    def eat_node(self, node, overall_edges, edge_count: int):
        """
        Merge this node with another one, updating the edges

        """
        other_node_edges = node.edge_indexes

        for edge_index in other_node_edges:
            edge: tuple = overall_edges[edge_index]
            if self in edge:
                # loop edge, ignore it
                overall_edges[edge_index] = None
                print(edge)
                print(overall_edges)
                if edge_index in self.edge_indexes:
                    self.edge_indexes.remove(edge_index)
                edge_count -= 1
                continue

            node_a, node_b = edge
            new_edge = (self, node_b) if node_a == node else (self, node_a)
            overall_edges[edge_index] = new_edge
            self.edge_indexes.add(edge_index)

        self.eaten_edges.append(node.name)
        for n_e_edge in node.eaten_edges:
            self.eaten_edges.append(n_e_edge)
        return edge_count





def build_nodes(nodes: set()) -> {str: Node}:
    return {node: Node(name=node) for node in nodes}


def add_edges_to_nodes(nodes: {str: Node}, edges: [(str, str)]):
    """ Adds the edge indexes to the appropriate Node object """
    for idx, edge in enumerate(edges):
        node_a, node_b = edge
        nodes[node_a].add_edge_index(idx)
        nodes[node_b].add_edge_index(idx)
        edges[idx] = (nodes[node_a], nodes[node_b])





def karger(nodes, edges):
    original_edge_count = len(edges)
    max_invalid_edges = original_edge_count // 2
    current_edge_count: int = original_edge_count

    while len(nodes.keys()) > 2:
        rdm_edge: (Node, Node) = select_random_edge(edges)
        # merge the nodes
        node_a, node_b = rdm_edge
        del nodes[node_b.name]
        current_edge_count = node_a.eat_node(node_b, edges, current_edge_count)

        # if current_edge_count <= max_invalid_edges:
        #     # Update edges
        #     edges = update_edges(edges)
        #     original_edge_count = len(edges)
        #     max_invalid_edges = original_edge_count // 2
        #     current_edge_count: int = original_edge_count


def update_edges(edges):
    """ Removes all the None entries in the list of edges """
    return list(filter(lambda x: x is not None, edges))


def select_random_edge(edges):
    edge = edges[random.randint(0, len(edges) - 1)]
    while edge is None:
        edge = edges[random.randint(0, len(edges) - 1)]
    return edge

nodes, edges = build_graph('test_graph.txt')
nodes: {str: Node} = build_nodes(nodes)
add_edges_to_nodes(nodes, edges)
print(edges)
karger(nodes, edges)
print("NOODES")
print(nodes)
print(f"EDGE COUNT: {len(list(filter(lambda x: x is not None, edges)))}")
print(edges)
print(list(nodes.values())[0])
print(list(nodes.values())[1])
