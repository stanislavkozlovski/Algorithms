#  https://www.interviewbit.com/problems/largest-number/

class Solution:

    # @param A : tuple of integers
    # @return a strings
    def largestNumber(self, numbers):
        class Number:
            def __init__(self, num):
                self.num = str(num)

            def __gt__(self, other):
                """
                The greater number should come first, so we compare in which scenario a bigger number comes out
                 Having self first or other first
                """
                return int(self.num + other.num) > int(other.num + self.num)

            def __str__(self):
                return self.num

        numbers = [Number(num) for num in numbers]
        biggest_number = ''.join(str(p) for p in list(reversed(sorted(numbers))))

        if biggest_number[0] == '0':
            return '0'

        return biggest_number
import unittest


class Tests(unittest.TestCase):
    def test_with_sample(self):
        self.assertEqual(Solution().largestNumber([3, 30, 34, 5, 9]), '9534330')

    def test_with_zeroes(self):
        self.assertEqual(Solution().largestNumber([0, 0, 0, 0]), '0')

    def test_with_one_and_zeroes(self):
        self.assertEqual(Solution().largestNumber([0, 0, 0, 0, 1]), '10000')

    def test_various_numbers(self):
        self.assertEqual(Solution().largestNumber([472, 663, 964, 722, 485, 852, 635, 4, 368, 676, 319, 412]),
        '9648527226766636354854724412368319')

    def test_edge_case(self):
        self.assertEqual(Solution().largestNumber([9, 99, 999, 9999, 9998]),
                         '99999999999998')
    def test_bigger_edge_case(self):
        self.assertEqual(Solution().largestNumber([782, 240, 409, 678, 940, 502, 113, 686, 6, 825, 366, 686, 877, 357,
                                                   261, 772, 798, 29, 337, 646, 868, 974, 675, 271, 791, 124, 363,
                                                   298, 470, 991, 709, 533, 872, 780, 735, 19, 930, 895, 799, 395, 905]),
                         '99197494093090589587787286882579979879178278077273570968668667867566465335024704093953663633573372982927126124019124113')
if __name__ == '__main__':
    unittest.main()

