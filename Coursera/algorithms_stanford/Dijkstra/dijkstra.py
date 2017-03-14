"""
A PriorityQueue implemented with a binary heap with the additional ability to modify a given element
Complexities:
    add: O(log(n))
    extract_min: O(log(n))
    modify_element: O(n + log(n))
    re_order_element: O(log(n))
"""


class PriorityQueue:
    def __init__(self, elements=None):
        self._elements = []
        self.count = 0
        if elements is not None and isinstance(elements, list):
            for el in elements:
                self.add(el)

    def __len__(self):
        return self.count

    def __contains__(self, item):
        # O(N)
        return item in self._elements

    def __repr__(self):
        return '{type} with {el_count} elements.'.format(type=type(self).__name__, el_count=self.count)

    def add(self, value):
        """ Add the value at the end and heapify up from there """
        self._elements.append(value)
        new_value_idx = len(self._elements) - 1
        self._heapify_up(new_value_idx)
        self.count += 1

    def extract_min(self):
        """ Remove the min element by placing the last element on it's place and heapifying down"""
        min_el = self._elements[0]
        last_idx = len(self._elements) - 1

        self._elements[0] = self._elements[last_idx]
        self._elements.pop()
        self._heapify_down(0)
        self.count -= 1

        return min_el

    def _heapify_up(self, idx):
        parent_idx = (idx - 1) // 2
        if idx < 0 or parent_idx < 0:
            return
        if self._elements[parent_idx] > self._elements[idx]:
            # swap
            self._elements[parent_idx], self._elements[idx] = self._elements[idx], self._elements[parent_idx]
            # update indices
            self._heapify_up(parent_idx)

    def _heapify_down(self, idx):
        """
        Heapify the value down by getting it's smaller child and swapping values. Then continue heapifying down
        until we find children that are not smaller than the value.
        """
        l_child_idx, r_child_idx = (idx*2) + 1, (idx*2) + 2

        if l_child_idx < len(self._elements):
            # get the index of the bigger child
            if r_child_idx < len(self._elements) and self._elements[r_child_idx] < self._elements[l_child_idx]:
                min_idx = r_child_idx
            else:
                min_idx = l_child_idx
            # check if the child is bigger than the value and stop
            if self._elements[min_idx] >= self._elements[idx]:
                return

            # swap
            self._elements[idx], self._elements[min_idx] = self._elements[min_idx], self._elements[idx]
            self._heapify_down(min_idx)

    def modify_element(self, old_value, new_value):
        """
        Modify an old value and re-order it in the heap
        O(N+log(N))
        """
        idx = self._elements.index(old_value)
        self._elements[idx] = new_value
        if new_value > old_value:
            self._heapify_down(idx)
        else:
            self._heapify_up(idx)

    def re_order_decreased_element(self, value):
        """
        Heapifies the given element up.
        This is typically done when the element has been changed, which, if you're calling this method,
        should be a reference type and should have been changed outside the PriorityQueue
        """
        idx = self._elements.index(value)
        self._heapify_up(idx)

    def re_order_increased_element(self, value):
        """
        Heapifies the given element down.
        This is typically done when the element has been changed, which, if you're calling this method,
        should be a reference type and should have been changed outside the PriorityQueue
        """
        idx = self._elements.index(value)
        self._heapify_down(idx)

class Edge:
    def __init__(self, node_a, node_b, weight):
        self.node_a = node_a
        self.node_b = node_b
        self.weight = weight

    def __gt__(self, other):
        return self.weight > other.weight

    def __ge__(self, other):
        return self.weight >= other.weight

    def __str__(self):
        return f'{self.node_a} to {self.node_b} with weight: {self.weight}'

    def __repr__(self):
        return self.__str__()


def build_graph():
    graph = {key: [] for key in range(1, 201)}
    with open('input.txt') as f:
        for line in f:
            line_info = line.split()
            node = int(line_info[0])
            for connection in line_info[1:]:
                node_b, weight = [int(p) for p in connection.split(',')]
                graph[node].append(Edge(node, node_b, weight))

    return graph


def dijkstra(start_vertex, end_vertex, graph):
    distances = {vertex: float('inf') for vertex in range(1, 201)}
    distances[start_vertex] = 0
    pq = PriorityQueue(elements=graph[start_vertex])
    visited = {start_vertex}

    while pq:
        min_edge = pq.extract_min()
        if min_edge.node_b not in visited:
            distances[min_edge.node_b] = min_edge.weight
            visited.add(min_edge.node_b)
            for edge in graph[min_edge.node_b]:
                if edge.node_b in visited:
                    continue
                pq.add(Edge(edge.node_a, edge.node_b, edge.weight + distances[edge.node_a]))
    return distances[end_vertex]
start_vertex = 1
nodes_wanted = [7,37,59,82,99,115,133,165,188,197]
print(','.join([str(dijkstra(start_vertex, node_wanted, build_graph())) for node_wanted in nodes_wanted]))
# 2599,2610,2947,2052,2367,2399,2029,2442,2505,3068
