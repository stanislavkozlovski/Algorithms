"""
O(N^4) solution to the Optimal Binary Search Tree problem
An easy way to reduce it to O(N^3) is to pre-calculate the frequency sums
    and eliminate the calculation in the calc_freq_sum() function
A hard way to reduce it to O(N^2) is to use Knuth's proposed algorithm in 1971.
Good luck finding the paper though (if you do, tell me please)

"""
__author__ = 'Enether'


def build_matrix(n):
    matrix = []
    for _ in range(n):
        matrix.append([None for _ in range(n)])
    return matrix


def calc_freq_sum(frequencies: list, start_idx: int, end_idx: int):
    """ Get the sum of frequencies from an index to the end index INCLUDING """
    return sum(frequencies[start_idx:end_idx+1])

# nums = [1,2,3,4,5,6,7]
# frequencies = [0.05, 0.4, 0.08, 0.04, 0.1, 0.1, 0.23]
nums = [1,2,3,4]
frequencies = [2, 23, 73, 4]

# cost_matrix holds the optimal cost of a binary tree which can be formed from nums[row] to nums[col]
cost_matrix = build_matrix(len(nums))
# as such, the end result would be in cost_matrix[0][-1] (last element in first row)

for i in range(len(nums)):
    cost_matrix[i][i] = frequencies[i]   # for a single key the frequence of the BST is its frequency (1 root nothing else)

# Now we need to consider BST of more than 1 element
"""
To solve this problem, we need to only fill the top-right part of the matrix.
Meaning, each matrix[i][i] and everything above.
To achieve this, we use a variable chain_length and increment it one by one.
 We achieve column J(col) as i(row) + chain_length
"""
for chain_length in range(2, len(nums)+1):
    end_row = len(nums) - chain_length + 1
    for row in range(end_row):
        col = row + chain_length - 1
        cost_matrix[row][col] = float('inf')

        # find the best root in between row to col
        for potential_root in range(row, col+1):
            # Use the previously computed subtree costs
            left_subtree_cost = cost_matrix[row][potential_root-1] if potential_root > row else 0
            right_subtree_cost = cost_matrix[potential_root+1][col] if potential_root < col else 0
            # The cost is always the sum of the all the frequencies + the subtree costs
            current_cost = left_subtree_cost + calc_freq_sum(frequencies, row, col) + right_subtree_cost
            if current_cost < cost_matrix[row][col]:
                # We've found a better root and a better cost, so we save it!
                cost_matrix[row][col] = current_cost

print(f'Answer is {cost_matrix[0][-1]}')