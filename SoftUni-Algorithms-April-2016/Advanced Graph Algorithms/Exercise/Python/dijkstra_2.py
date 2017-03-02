"""
Rewrote Dijkstra, this time keeping a PriorityQueue of the edges instead of the nodes
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

    def add_elements(self, elements: list):
        for el in elements:
            self.add(el)

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

    def __ge__(self, other):
        if self == other:
            return True
        return self.weight > other.weight

    def __repr__(self):
        return str(self.node_a) + str(self.node_b)


nodes = list(range(0, 12))
graph = {
    0: [Edge(0, 6, 10), Edge(0, 8, 12)],
    1: [Edge(1, 9, 5), Edge(1, 7, 26), Edge(1, 11, 6), Edge(1, 4, 20)],
    2: [Edge(2, 8, 14), Edge(2, 11, 9), Edge(2, 7, 15)],
    3: [Edge(3, 10, 7)],
    4: [Edge(4, 6, 17), Edge(4, 5, 5), Edge(4, 11, 11), Edge(4, 1, 20)],
    5: [Edge(5, 6,6), Edge(5, 4, 5), Edge(5, 11, 33), Edge(5, 8, 3)],
    6: [Edge(6, 0, 10), Edge(6, 5, 6), Edge(6, 4, 17)],
    7: [Edge(7, 2, 15), Edge(7, 11, 20), Edge(7, 1, 26), Edge(7, 9, 3)],
    8: [Edge(8, 0, 12), Edge(8, 5, 3), Edge(8, 2, 14)],
    9: [Edge(9, 1, 5), Edge(9, 7, 3)],
    10: [Edge(10, 3, 7)],
    11: [Edge(11, 5, 33), Edge(11, 4, 11), Edge(11, 2, 9), Edge(11, 1,6), Edge(11, 7, 20)]
}
import sys
dist = [sys.maxsize for _ in nodes]
prev = [-1 for _ in nodes]
dist[0] = 0
wanted_node = 9
# starting at node 0
visited = {0}
edges = PriorityQueue(elements=graph[0])
current_node = 0

while len(edges):
    edge: Edge = edges.extract_min()
    current_node = edge.node_a
    next_node = edge.node_b
    weight = edge.weight

    new_dist = weight

    if new_dist < dist[next_node]:
        prev[next_node] = current_node
        dist[next_node] = new_dist
        if next_node not in visited:
            # Move on to the next node
            edges.add_elements([Edge(next_node, edg.node_b, edg.weight + dist[next_node]) for edg in graph[next_node]])
            visited.add(next_node)

print(dist[9])
print(prev[9])

# Rebuild path
step_node = wanted_node
path = [wanted_node]
while True:
    step_node = prev[step_node]
    if step_node == -1:
        break
    path.append(step_node)
print(f'Path is {"->".join(str(p) for p in reversed(path))}')
