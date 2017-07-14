import operator


def left_most_non_zero(row, solved_columns):
    for idx, el in enumerate(row):
        if el != 0 and idx not in solved_columns:
            return idx, el
    return -1, -1


def swap_row_to_top(matrix, pivot_rows, idx_to_swap, equation_results):
    """
    Swaps the row to the top of the non-pivot rows
    """
    for row_idx, row in enumerate(matrix):
        if row_idx not in pivot_rows:
            # this is the top non-pivot row!
            matrix[row_idx], matrix[idx_to_swap] = matrix[idx_to_swap], matrix[row_idx]
            equation_results[row_idx], equation_results[idx_to_swap] = equation_results[idx_to_swap], equation_results[row_idx]
            pivot_rows.add(idx_to_swap)
            return


def rescale_pivot(pivot_row, pivot_row_idx, pivot_idx, equation_results):
    amount_to_rescale = pivot_row[pivot_idx] / 1
    for idx in range(len(pivot_row)):
        if pivot_row[idx] != 0:
            pivot_row[idx] /= amount_to_rescale

    if equation_results[pivot_row_idx] != 0:
        equation_results[pivot_row_idx] /= amount_to_rescale


def subtract_equation_from_others(matrix, pivot_row_idx, pivot_idx, equation_results):
    for row_idx, row in enumerate(matrix):
        if row_idx == pivot_row_idx:
            continue
        if row[pivot_idx] == 0:
            continue

        op_to_do = None
        if row[pivot_idx] > matrix[pivot_row_idx][pivot_idx]:
            # need to subtract
            op_to_do = operator.sub
        else:
            # need to add
            op_to_do = operator.add
        multiplier = abs(row[pivot_idx] / matrix[pivot_row_idx][pivot_idx])
        multiplied_pivot_row = [multiplier * el for el in matrix[pivot_row_idx]]
        evened_row = list(map(op_to_do, row, multiplied_pivot_row))
        equation_results[row_idx] = op_to_do(equation_results[row_idx], equation_results[pivot_row_idx] * multiplier)

        matrix[row_idx] = evened_row

equations = [
  [2, 4, -2, 0],
  [-1, -2, 1, -2],
  [2, 2,  0,  2]
]
equation_results = [
    2,
    -1,
    0
]
used_pivot_rows = set()
solved_columns = set()
ends = True
while ends:
    ends = False
    for row_idx, row in enumerate(equations):
        if row_idx in used_pivot_rows:
            continue

        # find left most non-zero
        left_most_idx, left_most_val = left_most_non_zero(row, solved_columns)
        if left_most_idx == -1:  # no variable in this row
            continue
        solved_columns.add(left_most_idx)

        swap_row_to_top(equations, used_pivot_rows, row_idx, equation_results)

        # rescale to make pivot 1
        rescale_pivot(row, row_idx, left_most_idx, equation_results)

        # subtract/add row to others to make other entries in the same column 0
        subtract_equation_from_others(equations, row_idx, left_most_idx, equation_results)

        from pprint import pprint
        pprint(equations, width=40)
        pprint(equation_results, width=10)
        ends = True
        break



