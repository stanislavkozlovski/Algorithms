"""
Given a graph, split the nodes into two groups, such that the edges between said groups are minimal.
"""
import random
import math
from copy import deepcopy

def ncr(n,r):
    """ Easily get the number for N choose 2"""
    f = math.factorial
    return int(f(n) / f(r) / f(n-r))


def build_graph(text_file_name) -> ({str}, [(str, str)]):
    """
    Builds a graph, comprising of a set() holding the nodes in the graph and a list holding the edges in the form
    of tuples
    :return:
        1. The set holding the nodes
        2. The list holding the edges in the form of tuples
    """
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


def main():
    import sys
    read_nodes, read_edges = build_graph('real_graph.txt')
    nodes_count = len(read_nodes)
    NUMBER_OF_ITERATIONS = nodes_count
    min_cut = sys.maxsize
    print(NUMBER_OF_ITERATIONS)

    for _ in range(NUMBER_OF_ITERATIONS):
        edges = deepcopy(read_edges)  # copy the edges so we don't have problems with references
        nodes: {str: Node} = build_nodes(read_nodes)
        add_edges_to_nodes(nodes, edges)

        karger(nodes, edges)

        edges_count = len(list(filter(lambda x: x is not None, edges)))
        if edges_count < min_cut:
            min_cut = edges_count

    print(f'The min cut of the given graph is {min_cut}!')

if __name__ == '__main__':
    main()