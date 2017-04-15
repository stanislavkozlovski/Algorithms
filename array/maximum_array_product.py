# Given an array, return the maximum product by multiplying 3 of its members
import unittest


def get_max_product(arr):
    """
    Since the array can have negative numbers and zeroes, we want biggest of
        1. Product of biggest 3 positive numbers
        2. Product of smallest 2 negative numbers and biggest 1 positive:
            ex: [10, 20, 30, -200, -200] = -200 * -200 * 30
    """
    if len(arr) < 3:
        raise Exception("Array must have 3 numbers!")
    sorted_arr = list(reversed(sorted(arr)))  # sort it by descending
    return max(sorted_arr[0] * sorted_arr[1] * sorted_arr[2], sorted_arr[0] * sorted_arr[-1] * sorted_arr[-2])


class Tests(unittest.TestCase):
    def test_positives(self):
        self.assertEqual(get_max_product([1, 2, 3, 4, 10, 22, 0, 3]), 4 * 10 * 22)

    def test_negatives(self):
        self.assertEqual(get_max_product([-2, -3, -10, -3]), -2 * -3 * -3)

    def test_negatives_and_zero(self):
        self.assertEqual(get_max_product([0, -2, -3, -10, -3]), 0)

    def test_negatives_and_positives(self):
        self.assertEqual(get_max_product([10, 20, 30, -200, -200]), -200 * -200 * 30)

if __name__ == '__main__':
    unittest.main()