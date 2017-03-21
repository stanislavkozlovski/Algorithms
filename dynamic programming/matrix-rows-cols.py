"""
Given a square boolean matrix mat[n][n],
find k such that all elements in k’th row are 0 and all elements in k’th column are 1.
The value of mat[k][k] can be anything (either 0 or 1). If no such k exists, return -1.
"""

# matrix = [[1, 0, 0, 0],
#           [1, 1, 1, 0],
#           [1, 1, 0, 0],
#           [1, 1, 1, 0]]

# matrix = [[1, 1, 1, 0, 1],
#           [0, 0, 0, 0, 0],
#           [1, 1, 1, 0, 0],
#           [1, 1, 1, 1, 0],
#           [1, 1, 1, 1, 1]]
#
matrix = [[0, 1, 1, 0, 1],
          [0, 0, 0, 0, 0],
          [1, 1, 1, 0, 0],
          [1, 0, 1, 1, 0],
          [1, 1, 1, 1, 1]]


valid_coords = None
row, col = 0, len(matrix[0]) - 1
while row < len(matrix) and col < len(matrix):
    if matrix[row][col] == 0:
        # This might be a valid row
        row_is_valid = True

        for cur_col in reversed(range(col)):
            if matrix[row][cur_col] != 0 and row != cur_col:
                row_is_valid = False
                break

        if row_is_valid:
            # These are the only possible valid coordinates, since every other column will have a 0 in it
            valid_coords = (row, row)
            break
        else:
            # This row is invalid
            row += 1
    elif matrix[row][col] == 1:
        # This might be a valid column
        col_is_valid = True

        for cur_row in range(row, len(matrix)):
            if matrix[cur_row][col] != 1 and cur_row != col:
                col_is_valid = False
                break

        if col_is_valid:
            # There are the only possible valid coordinates, since every other row will have a 1 in it
            valid_coords = (col, col)
            break
        else:
            # This column is invalid
            col -= 1

if valid_coords is None:
    print(-1)
    exit()

# One final check for validity

# check row
best_row, best_col = valid_coords
row_is_valid = True
for i in range(len(matrix[best_row])):
    if matrix[best_row][i] != 0 and best_row != i:
        row_is_valid = False
        break
col_is_valid = True
for i in range(len(matrix)):
    if matrix[i][best_col] != 1 and best_col != i:
        col_is_valid = False
        break

if row_is_valid and col_is_valid:
    print("Found valid k!")
    print(valid_coords[0])
