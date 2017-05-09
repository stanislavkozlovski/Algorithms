"""
Dijkstra's algorithm to compute the shortest path in a graph with no negative edges
    Input:
        5 6
        1 2 2
        2 5 5
        2 3 4
        1 4 1
        4 3 3
        3 5 1
    Output: (best path to the last element (5 in this case))
        1 4 3 5
    """
def main():
    node_count, edge_count = [int(p) for p in input().split()]

    graph = {node: [] for node in range(1, node_count + 1)}
    nodes = {idx: Node(idx) for idx in range(1, node_count + 1)}
    previous = {idx: None for idx in range(1, node_count + 1)}  # will hold the previous node

    for i in range(edge_count):
        node_a, node_b, weight = [int(p) for p in input().split()]
        edge = Edge(node_a, node_b, weight)
        edge_b = Edge(node_b, node_a, weight)
        graph[node_a].append(edge)
        graph[node_b].append(edge_b)

    start_node = 1
    nodes[start_node].best_path = 0  # the path from the start node is always 0

    visited_nodes = set()
    nodes_to_visit = PriorityQueue(elements=list(nodes.values()))
    while len(nodes_to_visit) > 0:
        closest_node = nodes_to_visit.extract_min()
        visited_nodes.add(closest_node.name)

        for outgoing_edge in graph[closest_node.name]:
            if outgoing_edge.node_b in visited_nodes:
                continue
            other_node = nodes[outgoing_edge.node_b]
            new_distance = nodes[closest_node.name].best_path + outgoing_edge.weight
            if other_node.best_path > new_distance:
                # We've found a shorter way
                other_node.best_path = new_distance
                nodes_to_visit.re_order_decreased_element(other_node)  # re-order PQ
                previous[other_node.name] = closest_node.name

    print(' '.join(str(p) for p in rebuild_path(previous, node_count)))
    # print(previous)


class Edge:
    def __init__(self, node_a, node_b, weight):
        self.node_a = node_a
        self.node_b = node_b
        self.weight = weight


class Node:
    def __init__(self, name):
        self.name = name
        self.best_path = float('inf')


def rebuild_path(prev, end_node):
    path = []
    curr_node = end_node
    while curr_node is not None:
        path.append(curr_node)
        curr_node = prev[curr_node]
    return list(reversed(path))


class PriorityQueue:
    """
    A PriorityQueue implemented with a binary heap with the additional ability to modify a given element
    Complexities:
        add: O(log(n))
        extract_min: O(log(n))
        modify_element: O(n + log(n))
        re_order_element: O(log(n))

    Requires usage of a class object, because it stores the PQ index on the object!
    """
    def __init__(self, elements=None):
        self._elements = []
        self.count = 0
        if elements is not None:
            if not isinstance(elements, list):
                raise Exception('The elements parameter must be a list!')
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
        value.__pq_idx = new_value_idx
        self._heapify_up(new_value_idx)
        self.count += 1

    def peek_min(self):
        return self._elements[0]

    def remove_element(self, element):
        last_idx = len(self._elements) - 1

        self._elements[element.__pq_idx] = self._elements[last_idx]
        self._elements[last_idx].__pq_idx = element.__pq_idx
        self._elements.pop()
        self._heapify_down(element.__pq_idx)
        self.count -= 1

        return element

    def extract_min(self):
        """ Remove the min element by placing the last element on it's place and heapifying down"""
        min_el = self._elements[0]
        last_idx = len(self._elements) - 1

        self._elements[0] = self._elements[last_idx]
        self._elements[0].__pq_idx = 0
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
            self._elements[parent_idx].__pq_idx = parent_idx
            self._elements[idx].__pq_idx = idx
            # update indices
            self._heapify_up(parent_idx)

    def _heapify_down(self, idx):
        """
        Heapify the value down by getting it's smaller child and swapping values. Then continue heapifying down
        until we find children that are not smaller than the value.
        """
        l_child_idx, r_child_idx = (idx * 2) + 1, (idx * 2) + 2

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
            self._elements[idx].__pq_idx = idx
            self._elements[min_idx].__pq_idx = min_idx
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
        O(logN)
        """
        idx = value.__pq_idx
        self._heapify_up(idx)

    def re_order_increased_element(self, value):
        """
        Heapifies the given element down.
        This is typically done when the element has been changed, which, if you're calling this method,
        should be a reference type and should have been changed outside the PriorityQueue
        O(logN)
        """
        idx = value.__pq_idx
        self._heapify_down(idx)




    def __ge__(self, other):
        return self.best_path >= other.best_path

    def __gt__(self, other):
        return self.best_path > other.best_path
