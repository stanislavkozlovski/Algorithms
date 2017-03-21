"""
Given a n*n matrix where numbers all numbers are distinct and are distributed from range 1 to n2,
find the maximum length path (starting from any cell)
such that all cells along the path are increasing order with a difference of 1.
"""

""" This problem is super easy when they're distinct """

POSSIBLE_STEPS = [(0, -1), (0, 1), (1, 0), (-1, 0)]


def find_longest_step(matrix: list, row: int, col: int, best_paths: list):
    """
        This function will store the path length for every number in the path
        Every coordinate will get computed exactly once.
        Complexity:
            O(N^2)

    """
    if best_paths[row][col] != 0:
        return best_paths[row][col]

    best_paths[row][col] = 1
    for row_to_add, col_to_add in POSSIBLE_STEPS:
        new_row = row + row_to_add
        new_col = col + col_to_add

        is_valid_step = (len(matrix) > new_row >= 0 and len(matrix[row]) > new_col >= 0
                         and (matrix[new_row][new_col] - matrix[row][col] == 1))
        if is_valid_step:
            best_paths[row][col] = 1 + find_longest_step(matrix, new_row, new_col, best_paths)
            break

    return best_paths[row][col]


def get_best_distance_str(matrix, best_paths, best_row, best_col):
    """
    Traverse the path again and build a nice user-friendly string of the longest path
    Complexity:
        O(P) where P is the length of the best path
    """
    path = [str(matrix[best_row][best_col])]
    curr_value = best_paths[best_row][best_col]
    while curr_value != 0:
        curr_value -= 1
        for row_to_add, col_to_add in POSSIBLE_STEPS:
            new_row = best_row + row_to_add
            new_col = best_col + col_to_add
            is_valid_step = (len(best_paths) > new_row >= 0 and len(best_paths[best_row]) > new_col >= 0
                             and (best_paths[new_row][new_col] == curr_value)
                             and (matrix[new_row][new_col] - matrix[best_row][best_col] == 1))
            if is_valid_step:
                best_row = new_row
                best_col = new_col
                path.append(str(matrix[best_row][best_col]))
                break

    return '->'.join(path)


def find_best_distance_coords(best_paths):
    """
    Traverse the whole computed best paths matrix to find the longest path there.
    :return: The coordinates of the start of said path
    """
    best_dist = 0
    best_coords = None
    for i in range(len(best_paths)):
        for j in range(len(best_paths[i])):
            if best_paths[i][j] > best_dist:
                best_coords = (i, j)
                best_dist = best_paths[i][j]

    return best_coords


def main():
    matrix = [[1, 2, 9],
                [5, 3, 8],
                [4, 6, 7]]
    # matrix = [[2, 6, 7],
    #           [1, 5, 8],
    #           [4, 10, 9]]

    best_paths = [[0, 0, 0],
                  [0, 0, 0],
                  [0, 0, 0]]
    for row_idx, row in enumerate(matrix):
        for col_idx, col in enumerate(row):
            find_longest_step(matrix, row_idx, col_idx, best_paths)

    best_row, best_col = find_best_distance_coords(best_paths)
    print(get_best_distance_str(matrix, best_paths, best_row, best_col))


if __name__ == '__main__':
    main()