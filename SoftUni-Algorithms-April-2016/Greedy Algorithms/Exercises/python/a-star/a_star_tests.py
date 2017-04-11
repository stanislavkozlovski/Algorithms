import unittest
from a_star import a_star
class AStarTests(unittest.TestCase):
    def test_search_first_map(self):
        map = [
                ['-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-'],
                ['-', '-', '-', 'T', '*', '-', '-', '-', '-', '-', '-'],
                ['-', '-', '-', 'T', 'T', 'T', 'T', 'T', '-', '-', '-'],
                ['-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-'],
                ['-', '-', '-', '-', '-', '-', '-', 'P', '-', '-', '-'],
                ['-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-']
            ]
        cells = a_star(map, (4,7), (1, 4))
        self.assertEqual(cells[3,8].g_cost, 20)
        self.assertEqual(cells[2,8].g_cost, 30)

        self.assertEqual(cells[1,8].g_cost, 40)
        # self.assertEqual(cells[1, 4].g_cost, 8)

if __name__ == '__main__':
    unittest.main()
