import unittest
from karatsuba import karatsuba_multiply

class KaratsubaTests(unittest.TestCase):
    def test_small_nums(self):
        a = 10
        b = 5
        expected_result = a * b
        result = karatsuba_multiply(a, b)
        self.assertEqual(result, expected_result)

    def test_big_nums(self):
        a = 3141592653589793238462643383279502884197169399375105820974944592
        b = 2718281828459045235360287471352662497757247093699959574966967627
        expected_result = a*b
        result = karatsuba_multiply(a,b)
        self.assertEqual(expected_result, result)


if __name__ == '__main__':
    unittest.main()