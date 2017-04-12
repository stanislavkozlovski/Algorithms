#  https://www.interviewbit.com/problems/flip-array/?ref=dash-reco
"""
Given an array of positive elements, you have to flip the sign of some of its elements such that the resultant sum of the elements of
array should be minimum non-negative(as close to zero as possible).
Return the minimum no. of elements whose sign needs to be flipped such that the resultant sum is minimum non-negative.
"""


def build_matrix(rows, cols):
    matrix = []
    for r in range(0, rows):
        matrix.append([None for _ in range(cols)])
    return matrix

"""
The approach is the following:
We create a matrix of sums as rows and numbers as cols.
Then, we iterate per col (bottom row to top row), each cell representing the amount of numbers it took to reach
 the sum (sum is the row idx).
  We copy over the old reachable sums from the last column and iterate through each sum
 If the sum is reachable, we deduct our (current number * 2, since flip) from it and later fill that spot.
 (take care to fill those spots after you're done traversing the column for obvious reasons (you dont want to reuse your number))
 We also add a new spot, subtracting our number from the overall sum, like we've only removed 1 number from it.
"""

nums =[ 11, 10, 8, 6, 8, 11, 1, 10, 2, 3, 8, 3, 8, 12, 11, 1, 7, 5, 5, 12, 9, 4, 10, 3, 3, 3, 8, 8, 8, 6, 7, 7, 7, 6, 4, 2, 5, 8, 11, 10, 10, 10, 12, 9, 2, 3, 9, 12, 7, 6, 11, 8, 9, 9, 10, 3, 3, 5, 2, 10, 10, 9, 4, 9, 6, 11, 10, 2, 6, 1, 4, 7, 10, 3, 4, 3, 9, 4, 3, 8, 1, 1, 3 ]

# create matrix with rows as sums and cols as nums
matrix = build_matrix(sum(nums), len(nums))

# first row of matrix will only have one value
nums_sum = sum(nums)
first_reachable_sum = nums_sum - (nums[0] * 2)
if first_reachable_sum >= 0:
    matrix[first_reachable_sum][0] = 1  # it only took the first number to reach this sum

for col_idx in range(1, len(nums)):
    number = nums[col_idx]
    if number <= 0:
        continue
    to_add = []

    for row_idx in reversed(range(len(matrix))):
        # go through the rows reversed (from max_sum to 0)

        # copy over the last column's values if we have none
        if matrix[row_idx][col_idx] is None:
            matrix[row_idx][col_idx] = matrix[row_idx][col_idx-1]

        if matrix[row_idx][col_idx] is not None:  # if the sum is reachable
            # calc new value with the sum
            new_value = row_idx - (number * 2)
            nums_for_new_value = matrix[row_idx][col_idx] + 1  # the amount of numbers it took to reach this new value
            if new_value >= 0:
                if matrix[new_value][col_idx] is not None and matrix[new_value][col_idx] > nums_for_new_value:
                    # if the new sum is reachable and we've found a way to reach it with less nums
                    to_add.append((new_value, col_idx, nums_for_new_value))
                    # matrix[new_value][col_idx] = nums_for_new_value
                else:
                    to_add.append((new_value, col_idx, nums_for_new_value))
                    # matrix[new_value][col_idx] = matrix[row_idx][col_idx] + 1

    # add the values after we've traversed the column
    for row, col, new in to_add:
        if matrix[row][col] is None or (matrix[row][col] is not None and matrix[row][col] > new):
            matrix[row][col] = new

    # add the reachable sum from flipping only the current number
    reachable_sum = nums_sum - (2 * number)
    if reachable_sum >= 0:
        matrix[reachable_sum][col_idx] = 1

# get the min amount of numbers
min_count = None
for row_idx in range(len(matrix)):
    for col_idx in range(len(nums)):
        # find the minimum count of numbers needed to reach this sum
        if matrix[row_idx][col_idx] is not None:
            if min_count is None or matrix[row_idx][col_idx] < min_count:
                min_count = matrix[row_idx][col_idx]

    if min_count is not None:  # if we've found a count, we need to break immediatelly, since we iterate 0-max_sum
        break

print(min_count)
