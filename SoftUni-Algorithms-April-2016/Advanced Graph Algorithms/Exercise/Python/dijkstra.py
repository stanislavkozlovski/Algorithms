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

        return previous, {node.value: node.distance_from_start for node in self.nodes.values() if
                          node.distance_from_start != maxsize}

