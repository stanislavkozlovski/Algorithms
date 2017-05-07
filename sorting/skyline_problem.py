import unittest


def main():
    # https://leetcode.com/problems/the-skyline-problem/

    """
    Input is expected in the form of X1, X2 and Y (height) of the range
    Input:
        5
        1 3 3
        2 4 4
        5 8 2
        6 7 4
        8 9 4
    Output:
    [(1, 3), (2, 4), (4, 0), (5, 2), (6, 4), (7, 2), (8, 4), (9, 0)]
    """
    ranges = []
    points = []
    for i in range(int(input())):
        x_a, x_b, y = [int(p) for p in input().split()]
        ranges.append(Range(x_a, y, is_end=False))
        ranges.append(Range(x_b, y, is_end=True))

    height_ranges = {}  # keep a dictionary of the edges by their heights for easy access and removal from priority queue
    top_ranges = PriorityQueue()
    current_height = 0
    for curr_range in sorted(ranges):
        if curr_range.is_end:
            range_to_remove: Range = height_ranges[curr_range.height].pop()
            top_ranges.remove_element(range_to_remove)  # remove from PQ

            if len(top_ranges) > 0:  # we've removed one element, see what the max available height is
                new_height = top_ranges.peek_min().height
            else:
                new_height = 0
            if new_height != current_height:
                current_height = new_height
                points.append((curr_range.coord, new_height))
        else:  # Add a new range!
            if curr_range.height in height_ranges:
                height_ranges[curr_range.height] += [curr_range]
            else:
                height_ranges[curr_range.height] = [curr_range]
            top_ranges.add(curr_range)  # add to PQ
            if curr_range.height > current_height:
                current_height = curr_range.height
                points.append((curr_range.coord, curr_range.height))
    print(points)


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


class Range:
    def __init__(self, start, end, is_end):
        self.coord = start
        self.height = end
        self.is_end = is_end

    def __gt__(self, other):
        if self.coord == other.coord:
            if self.is_end and other.is_end:
                return self.height > other.height  # both are end, lower height should be first
            elif self.is_end and not other.is_end:
                return True  # we are end and other is start, start should be first
            elif not self.is_end and other.is_end:
                return False  # we are start and other is end, start should be first
            elif not self.is_end and not other.is_end:
                return self.height < other.height  # both start, one with higher height should be first
        return self.coord > other.coord

    def __ge__(self, other):
        return self.__gt__(other)

    def __str__(self):
        return f'Range {self.coord} {self.height} {"E" if self.is_end else "S"} '



class RangeSortingTests(unittest.TestCase):

    def test_with_equal_starts(self):
        ranges = [Range(2, 3, True), Range(0, 2, False), Range(1, 2, True), Range(0, 3, False)]
        expected_ranges = [(0,3), (0,2), (1,2), (2,3)]

        pq = PriorityQueue(ranges)
        for expected_start, expected_end in expected_ranges:
            min_range = pq.extract_min()
            self.assertEqual(min_range.coord, expected_start)
            self.assertEqual(min_range.height, expected_end)

    def test_with_equal_ends(self):
        """ The one with the lower end should come out first"""
        ranges = [Range(5, 3, True), Range(4, 2, False), Range(5, 2, True), Range(3, 3, True)]
        expected_ranges = [(3,3), (4, 2), (5, 2), (5,3)]

        pq = PriorityQueue(ranges)
        for expected_start, expected_end in expected_ranges:
            min_range = pq.extract_min()
            self.assertEqual(min_range.coord, expected_start)
            self.assertEqual(min_range.height, expected_end)

    def test_with_equal_start_end(self):
        """ The start should come out first """
        ranges = [Range(7, 3, False), Range(6, 2, False), Range(7, 2, True), Range(8, 3, True)]
        expected_ranges = [(6,2), (7,3), (7,2), (8, 3)]
        pq = PriorityQueue(ranges)
        for expected_start, expected_end in expected_ranges:
            min_range = pq.extract_min()
            self.assertEqual(min_range.coord, expected_start)
            self.assertEqual(min_range.height, expected_end)

if __name__ == '__main__':
    main()
    # unittest.main()