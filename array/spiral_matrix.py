class Solution:
    # @param A : tuple of list of integers
    # @return a list of integers
    def spiralOrder(self, A):
        result = []
        # Actual code to populate result

        go_up = False
        go_right = True
        traverse_row = True
        row, col = 0, 0  # tracks where we're at
        max_row, max_col = len(A), len(A[0])
        min_row, min_col = 0, 0
        last_len = -1  # tracks if we've reached the end
        while len(result) != last_len:
            last_len = len(result)

            if traverse_row:
                if go_right:
                    while col < max_col:
                        result.append(A[row][col])
                        col += 1
                    col -= 1
                    max_col = col
                else:
                    while col >= min_col:
                        result.append(A[row][col])
                        col -= 1
                    col +=1
                    min_col = col+1

                go_right = not go_right
            else:
                if go_up:
                    while row-1 > min_row:
                        row -= 1
                        result.append(A[row][col])
                    col += 1
                    min_row = row
                else:
                    while row+1 < max_row:
                        row += 1
                        result.append(A[row][col])
                    col -= 1
                    max_row = row
                go_up = not go_up

            traverse_row = not traverse_row

        return result

print(Solution().spiralOrder([

        [335, 401, 128, 384, 345, 275, 324, 139, 127, 343, 197, 177, 127, 72, 13, 59],
        [102, 75, 151, 22, 291, 249, 380, 151, 85, 217, 246, 241, 204, 197, 227, 96],
        [261, 163, 109, 372, 238, 98, 273, 20, 233, 138, 40, 246, 163, 191, 109, 237],
        [179, 213, 214, 9, 309, 210, 319, 68, 400, 198, 323, 135, 14, 141, 15, 168]

]))