"""
A min-heap with the added ability to modify a given value inside the heap efficiently.
This is done by saving the index of each element for O(1) search.

NOTE: THIS WILL NOT WORK WITH DUPLICATE ELEMENTS!
For it to work with duplicate elements, we would not be able to save their indexes in dictionaries therefore
modifying an element would be O(n) to find the element only
"""


class PriorityQueue:
    def __init__(self, elements=None):
        self._elements = []
        self._indexes = {}
        self.count = 0
        if elements is not None and isinstance(elements, list):
            for el in elements:
                self.add(el)

    def __len__(self):
        return self.count

    def __contains__(self, item):
        return item in self._indexes

    def __repr__(self):
        return '{type} with {el_count} elements.'.format(type=type(self).__name__, el_count=self.count)

    def add(self, value):
        """ Add the value at the end and heapify up from there """
        self._elements.append(value)
        new_value_idx = len(self._elements) - 1
        self._indexes[value] = new_value_idx
        self._heapify_up(new_value_idx)
        self.count += 1

    def extract_min(self):
        """ Remove the min element by placing the last element on it's place and heapifying down"""
        min_el = self._elements[0]
        last_idx = len(self._elements) - 1

        self._elements[0] = self._elements[last_idx]
        self._elements.pop()
        # self._elements = self._elements[:last_idx]
        del self._indexes[min_el]
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
            self._indexes[self._elements[parent_idx]] = parent_idx
            self._indexes[self._elements[idx]] = idx
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
            # update indices
            self._indexes[self._elements[idx]] = idx
            self._indexes[self._elements[min_idx]] = min_idx
            self._heapify_down(min_idx)

    def modify_element(self, old_value, new_value):
        """
        Modify an old value and re-order it in the heap
        """
        idx = self._indexes[old_value]
        self._elements[idx] = new_value
        self._indexes[new_value] = idx
        del self._indexes[old_value]
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
        idx = self._indexes[value]
        self._heapify_up(idx)

    def re_order_increased_element(self, value):
        """
        Heapifies the given element down.
        This is typically done when the element has been changed, which, if you're calling this method,
        should be a reference type and should have been changed outside the PriorityQueue
        """
        idx = self._indexes[value]
        self._heapify_down(idx)
