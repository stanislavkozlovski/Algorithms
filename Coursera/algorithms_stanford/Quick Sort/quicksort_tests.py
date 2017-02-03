import unittest
from quicksort import non_random_quick_sort


class QuickSortTests(unittest.TestCase):
    def test_sort(self):
        arr = [4, 3, 9, 31431, 2, -1, -2, 40]
        expected_arr = list(sorted(arr))

        self.assertEqual(non_random_quick_sort(arr), expected_arr)

    def test_sort_more_negative_numbers(self):
        arr = [1250, 4, 3, 9, 31431, 2, -1, -2, -40, -1002, -3, -50, -25]
        expected_arr = list(sorted(arr))

        self.assertEqual(non_random_quick_sort(arr), expected_arr)

    def test_sort_sorted(self):
        arr = [1, 2, 3, 4]
        expected_arr = list(sorted(arr))

        self.assertEqual(non_random_quick_sort(arr), expected_arr)

    def test_sort_array_one_element(self):
        arr = [1]

        self.assertEqual(non_random_quick_sort(arr), [1])


if __name__ == '__main__':
    unittest.main()
