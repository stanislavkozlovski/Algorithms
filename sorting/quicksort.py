import unittest
import random


def get_median(array: list, start):
    """ Chooses the median of three items from start to the second after"""
    # 3 10 12
    """
    I wonder if
     return sorted(array[start:start+3])[1]
     is faster?
     Shouldn't be, but does it matter, when this took 10 minutes more to write and test
    """
    item_a, item_b, item_c = array[start], array[start+1], array[start+2]
    if item_a > item_b:
        if item_b >= item_c:
            return item_b, start + 1
        elif item_a <= item_c:
            return item_a, start
    else:  # item_b >= item_a
        if item_a >= item_c:
            return item_a, start
        elif item_c >= item_b:
            return item_b, start + 1

    return item_c, start + 2


def quicksort(array: list, st_idx, end_idx):
    # on init shuffle the array so its randomized
    if st_idx == 0 and end_idx == len(array) - 1:
        random.shuffle(array)

    if end_idx-st_idx < 20:
        # TODO: Insertion sort
        pass
    if end_idx-st_idx <= 3:  # this is because we haven't implemented insertion sort at the top and its more practical for testing
        array[st_idx:end_idx+1] = sorted(array[st_idx:end_idx+1])
        return
    # mini optimization to get a element closer to the mid
    _, median_idx = get_median(array, (st_idx+end_idx)//2)
    array[st_idx], array[median_idx] = array[median_idx], array[st_idx]

    # partition this ShiT
    pivot_idx = partition(array, st_idx, end_idx)

    # quick sort on both parts, excluding the pivot
    quicksort(array, st_idx, pivot_idx-1)
    quicksort(array, pivot_idx+1, end_idx)


def partition(array: list, st_idx, end_idx):
    st_ptr, end_ptr = st_idx + 1, end_idx
    pivot, pivot_idx = array[st_idx], st_idx
    while True:  # always try to find two pointers that should be on the opposite side
        while st_ptr <= end_idx and array[st_ptr] < pivot:
            st_ptr += 1
        while end_ptr > pivot_idx and array[end_ptr] > pivot:
            end_ptr -= 1
        if st_ptr > end_ptr:
            break  # pointers have crossed
        array[st_ptr], array[end_ptr] = array[end_ptr], array[st_ptr]  # st_ptr is bigger than pivot and end is smaller

    # what's at the end pointer is the rightest element thats smaller than pivot, so exchange them
    array[pivot_idx], array[end_ptr] = array[end_ptr], array[pivot_idx]
    return end_ptr  # the new pivot index


class Tests(unittest.TestCase):
    def test_median_of_three(self):
        array = [3, 10, 12]
        for i in range(100):
            random.shuffle(array)
            self.assertEqual(get_median(array, 0), (10, array.index(10)), f'Did not get medium of {array}')

    def test_partitions(self):
        for i in range(100):
            orig_array = list(range(100))  # sorted array from which we'll get the right indices
            array = list(range(100))

            random.shuffle(array)
            chosen_el = array[0]
            expected_idx = orig_array.index(chosen_el)

            received_idx = partition(array, 0, len(array)-1)
            self.assertEqual(received_idx, expected_idx, f'The element {chosen_el} was but at index {received_idx} but should have been at {expected_idx}')
            self.assertEqual(array[expected_idx], chosen_el)
            for i in array[:expected_idx]:
                self.assertTrue(i <= chosen_el)
            for i in array[expected_idx+1:]:
                self.assertTrue(i >= chosen_el)

    def test_quicksort(self):
        for i in range(1):
            array = list(range(random.randint(1,444)))
            expected_arr = sorted(array)
            random.shuffle(array)

            quicksort(array, 0, len(array)-1)

            self.assertEqual(array, expected_arr)