# Find the count of sequences which equal 0
zero_sums = 0
matrix = [
    [-8, 5, 7],
    [3, 7, -8],
    [5, -8, 9]
]
sums = [
    [[], [], []],
    [[], [], []],
    [[], [], []]
]
for row_idx, row in enumerate(matrix):
    for col_idx, col in enumerate(row):
        sums[row_idx][col_idx].append(matrix[row_idx][col_idx])
        prev_sums = (set(sums[row_idx-1][col_idx] + sums[row_idx][col_idx-1]))
        prev_sums = prev_sums.difference(set(sums[row_idx-1][col_idx-1]))
        sums[row_idx][col_idx].extend(prev_sums)
        print(prev_sums)
print(sums)

