"""
A PriorityQueue implemented with a binary heap with the additional ability to modify a given element
Complexities:
    add: O(log(n))
    extract_min: O(log(n))
    modify_element: O(n + log(n))
    re_order_element: O(log(n))

Requires usage of a class object, because it stores the PQ index on the object!
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
        value.__pq_idx = new_value_idx
        self._heapify_up(new_value_idx)
        self.count += 1

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


class Node:
    def __init__(self, value, frequency):
        self.value = value
        self.frequency = frequency
        self.parent = None
        self.left = None
        self.right = None

    def get_weight(self):
        return self.frequency

    def __str__(self):
        return str(self.value)

    def __repr__(self):
        return self.__str__()


class NonLeafNode(Node):
    def __init__(self, frequency, left, right):
        super().__init__(None, frequency)
        self.left = left
        self.left.parent = self
        self.right = right
        self.right.parent = self
        self.parent = None


class SubTree:
    def __init__(self, root):
        self.root = root
        self.cache_is_valid = False
        self.cache = {}
        self.current_bits = []

    def merge(self, other_tree):
        """
        Merges the current tree with another
        """
        if isinstance(other_tree, SubTree):
            other_tree = other_tree.root
        new_root = NonLeafNode(other_tree.get_weight() + self.get_weight(), left=other_tree, right=self.root)
        self.cache_is_valid = False
        self.root = new_root

    def decode_word(self, binary_string: str):
        # TODO: Refactor ? :)
        if not isinstance(self.root, NonLeafNode):
            return self.root.value

        whole_word = []
        curr_node = self.root
        for char in binary_string:
            if isinstance(curr_node, SubTree):
                curr_node = curr_node.root
            if not isinstance(curr_node, NonLeafNode):
                whole_word.append(curr_node.value)
                curr_node = self.root

            if char == '1':  # Go right
                curr_node = curr_node.right
            else:  # go left
                curr_node = curr_node.left
        if isinstance(curr_node, SubTree):
            curr_node = curr_node.root
        if not isinstance(curr_node, NonLeafNode):
            whole_word.append(curr_node.value)
            curr_node = self.root
        return whole_word

    def encode_word(self, word: str):
        encoded_word = []
        if not self.cache_is_valid:
            self.cache_words()

        for character in word:
            if character not in self.cache:
                raise Exception("Cache was not maintained properly or the character is not in the Tree!")
            encoded_word.append(self.cache[character])

        return ''.join(encoded_word)

    def cache_words(self):
        """
        Fill the self.cache dictionary, holding the encoded bits for each character
         Traverse the tree from left to right
        """
        # reset the cache and current_bits
        self.cache = {}
        self.current_bits = []
        self._cache_nodes(self.root)
        self.cache_is_valid = True

    def _cache_nodes(self, node):
        """
        Keeps track and modifies the bits at each node
        Traverses every node and saves its bit representation in the cache
        """
        if node.left is not None:
            self.current_bits.append('0')
            self._cache_nodes(node.left)
            self.current_bits.pop()

        if node.value is not None:
            self.cache[node.value] = ''.join(self.current_bits)

        if node.right is not None:
            self.current_bits.append('1')
            self._cache_nodes(node.right)
            self.current_bits.pop()

    def _traverse_left(self, node, current_bits) -> Node:
        leftest_node = node
        while isinstance(leftest_node, NonLeafNode):
            current_bits.append('0')
            leftest_node = leftest_node.left
        return leftest_node

    def _traverse_right(self, node, current_bits) -> Node:
        rightest_node = node
        while isinstance(rightest_node, NonLeafNode):
            current_bits.append('1')
            rightest_node = rightest_node.right
        return rightest_node

    def get_weight(self):
        return self.root.frequency

    def __gt__(self, other):
        return self.get_weight() > other.get_weight()

    def __ge__(self, other):
        return self.get_weight() >= other.get_weight()


words = [SubTree(root=Node(value='B', frequency=2)), SubTree(root=Node(value='E', frequency=2)),
         SubTree(root=Node(value='A', frequency=3)), SubTree(root=Node(value='C', frequency=6)),
         SubTree(root=Node(value='F', frequency=6)), SubTree(root=Node(value='D', frequency=8))]

sorted_words = PriorityQueue(elements=words)

# Huffman Encoding Algorithm
while len(sorted_words) > 1:
    # Take the both trees with the lowest frequency
    first_tree = sorted_words.extract_min()
    second_tree = sorted_words.extract_min()
    # Merge them
    first_tree.merge(second_tree)
    # first_tree.merge(second_tree)
    # Add the merged tree back into the priority queue
    sorted_words.add(first_tree)
test = sorted_words.extract_min()
test.cache_words()
print(test.cache)
print(test.encode_word('ABE'))
print(test.decode_word('01101010100'))
