# from priority_queue import PriorityQueue
from priority_queue_no_duplicates import PriorityQueue
from sys import maxsize


class Node:
    def __init__(self, value, distance_from_start=maxsize):
        self.value = value
        self.distance_from_start = distance_from_start

    def __repr__(self):
        return str(self.value)

    def __hash__(self):
        return hash(self.value)

    def __eq__(self, other):
        return self.distance_from_start == other.distance_from_start

    def __ne__(self, other):
        return not self.distance_from_start == other.distance_from_start

    def __le__(self, other):
        return self.distance_from_start <= other.distance_from_start

    def __lt__(self, other):
        return self.distance_from_start < other.distance_from_start

    def __gt__(self, other):
        return self.distance_from_start > other.distance_from_start

    def __ge__(self, other):
        return self.distance_from_start >= other.distance_from_start


class Graph:
    def __init__(self):
        self.nodes = dict()
        self.graph = dict()

    def add_node(self, value):
        if value in self.nodes:
            raise Exception('Value is already in the graph!')
        node = Node(value)
        self.nodes[value] = node
        self.graph[node] = dict()

    def add_edge(self, value_a, value_b, weight):
        if value_a not in self.nodes or value_b not in self.nodes:
            raise Exception('Node is not in the graph!')
        # if value_a not in self.nodes:
        #     self.add_node(value_a)
        # if value_b not in self.nodes:
        #     self.add_node(value_b)
        node_a, node_b = self.nodes[value_a], self.nodes[value_b]
        self.graph[node_a][node_b] = weight
        self.graph[node_b][node_a] = weight

    def get_node_by_value(self, value):
        """ Given a value, return the node object in the graph with that value"""
        if value not in self.nodes:
            raise Exception('Value not in the graph!')
        return self.nodes[value]

    def dijkstra(self, start_value, end_value=None):
        """

        :param start_value:
        :param end_value:  the value we want to find the shortest path to
            if it's not set, we find the shortest path to every node
        :return:
        """
        pq = PriorityQueue()
        previous = dict()
        visited = set()
        for node in self.nodes.values():
            node.distance_from_start = maxsize
        start_node = self.nodes[start_value]
        start_node.distance_from_start = 0
        pq.add(start_node)

        while len(pq) > 0:
            current_node = pq.extract_min()

            if end_value is not None and end_value == current_node.value:
                break

            for conn_node, weight in self.graph[current_node].items():
                if conn_node not in visited:
                    visited.add(conn_node)
                    pq.add(conn_node)

                new_distance = current_node.distance_from_start + weight

                if new_distance < conn_node.distance_from_start:
                    # we've found a shorter way
                    conn_node.distance_from_start = new_distance
                    previous[conn_node] = current_node
                    pq.re_order_decreased_element(conn_node)

        return previous

gr = Graph()
gr.add_node(0)
gr.add_node(6)
gr.add_node(8)
gr.add_node(5)
gr.add_node(4)
gr.add_node(2)
gr.add_node(11)
gr.add_node(1)
gr.add_node(7)
gr.add_node(9)

gr.add_node(3)
gr.add_node(10)

gr.add_edge(0, 6, 10)
gr.add_edge(0, 8, 12)
gr.add_edge(6, 5, 6)
gr.add_edge(6, 4, 17)
gr.add_edge(8, 5, 3)
gr.add_edge(8, 2, 14)
gr.add_edge(5, 4, 5)
gr.add_edge(5, 11, 33)
gr.add_edge(4, 1, 20)
gr.add_edge(4, 11, 11)
gr.add_edge(2, 11, 9)
gr.add_edge(2, 7, 15)
gr.add_edge(11, 1, 6)
gr.add_edge(11, 7, 20)
gr.add_edge(1, 7, 26)
gr.add_edge(1, 9, 5)
gr.add_edge(7, 9, 3)
gr.add_edge(3, 10, 7)

print(gr.dijkstra(0))
print(gr.get_node_by_value(9).distance_from_start)
