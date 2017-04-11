import sys

DIAGONAL_COST = 14
STRAIGHT_COST = 10




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


class Cell:
    directions = [
        (0, 1), (0, -1),
         (1, 0),
        (-1, 0)
    ]

    def __init__(self, row, col, g_cost, h_cost):
        self.row = row
        self.col = col
        self.g_cost = g_cost
        self.h_cost = h_cost

    def get_f_cost(self):
        return self.g_cost + self.h_cost

    def __eq__(self, other):
        return self.get_f_cost() == other.get_f_cost()

    def __hash__(self):
        return hash(str(self.row) + str(self.col))

    def __gt__(self, other):
        if self.get_f_cost() == other.get_f_cost():
            return self.h_cost > other.h_cost
        return self.get_f_cost() > other.get_f_cost()

    def __ge__(self, other):
        if self.get_f_cost() == other.get_f_cost():
            return self.h_cost >= other.h_cost
        return self.get_f_cost() >= other.get_f_cost()

    def __repr__(self):
        return f"Cell at {self.row} {self.col}. G-Cost: {self.g_cost}"


class Wall:
    pass


def get_cell_neighbours(c: Cell, all_cells: dict, sample_map: list):
    """
    Returns the neighbours of a cell
    """
    valid_neighbours = []
    for row_to_add, col_to_add in Cell.directions:
        new_row = c.row + row_to_add
        new_col = c.col + col_to_add
        if 0 <= new_row < len(sample_map) and 0 <= new_col < len(sample_map[new_row]) and not isinstance(all_cells[(new_row, new_col)], Wall):
            valid_neighbours.append(all_cells[(new_row, new_col)])

    return valid_neighbours


def get_h_cost(row_idx, col_idx, target_row, target_col):
    """
    Calculates direct distance
    Returns the h cost of a cell
    """
    delta_row = abs(row_idx - target_row)
    delta_col = abs(col_idx - target_col)

    if delta_row > delta_col:
        return (DIAGONAL_COST * delta_col) + (STRAIGHT_COST * (delta_row - delta_col))
    else:
        return (DIAGONAL_COST * delta_row) + (STRAIGHT_COST * (delta_col - delta_row))


def build_cells(sample_map, target_coords) -> dict:
    """
    Returns a dict with coordinates of the cell as key and the Cell object as value
    """
    cells = {}

    for row_idx, row in enumerate(sample_map):
        for col_idx, col in enumerate(row):
            if sample_map[row_idx][col_idx] == 'T':
                cells[(row_idx, col_idx)] = Wall()
            else:
                cells[(row_idx, col_idx)] = Cell(row_idx, col_idx, sys.maxsize,
                                                 get_h_cost(row_idx, col_idx, *target_coords))

    return cells

def a_star(sample_map, start_coords, target_coords) -> dict:
    """ Runs the A* Algorithm on a map and returns a dictionary holding cell objects with their G-Cost filled """
    cells = build_cells(sample_map, target_coords)
    start_cell: Cell = cells[start_coords]
    start_cell.g_cost = 0

    cells_to_visit = PriorityQueue([start_cell])
    visited = set()
    to_visit = {start_cell}  # set of cells we are about to visit
    while cells_to_visit.count > 0:
        cell = cells_to_visit.extract_min()
        if (cell.row, cell.col) == target_coords:
            print(f"Found the target and the distance is: {cell.g_cost}")
            break
        visited.add(cell)
        to_visit.remove(cell)
        for neighbour in get_cell_neighbours(cell, cells, sample_map):
            if neighbour in visited:
                continue
            new_g_cost = cell.g_cost + STRAIGHT_COST
            if new_g_cost < neighbour.g_cost:
                neighbour.g_cost = new_g_cost  # Found a better way to our neighbour
                if neighbour not in to_visit:  # first time seeing this node
                    cells_to_visit.add(neighbour)  # add it to the PQ
                    to_visit.add(neighbour)
                else:  # it is in the PQ so we need to update it
                    cells_to_visit.re_order_decreased_element(neighbour)
    print(cells[target_coords].g_cost)

    return cells

def main():
    sample_map = [
        [' ', ' ', ' ', ' ', 'P', ' ', ' ', ' ', ' ', ' ', ' '],
        [' ', ' ', ' ', 'T', ' ', ' ', ' ', ' ', ' ', ' ', ' '],
        [' ', ' ', ' ', 'T', 'T', 'T', 'T', 'T', 'T', 'T', ' '],
        [' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '],
        [' ', ' ', ' ', ' ', ' ', ' ', 'S', ' ', ' ', ' ', ' '],
        [' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '],
        [' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ']
    ]
    target_coords = 0, 4
    start_coords = 4, 6

    a_star(sample_map, start_coords, target_coords)

if __name__ == '__main__':
    main()
