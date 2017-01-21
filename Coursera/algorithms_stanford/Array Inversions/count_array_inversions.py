"""
Given an array, count the number of inversion in it
An inversion is when an element at the smaller index i is greater than j
so:
Indexes:
    i < j
Inversion:
    arr[i] > arr[j]
"""


def merge(left, right) -> (list, int):
    """
    Merge both arrays and count the SPLIT inversions between the two
    we note when we add a number from the right part. When we do such a thing, we know that there are X inversions wth that number,
     where X is the number of numbers left in the LEFT array (because those that are left are bigger than the number we are adding from the right)
    """
    inversions = 0
    i, j = 0, 0
    new_arr = []

    for _ in range(len(left) + len(right)):
        if ((i < len(left) and j < len(right)  # both are in bounds
             and left[i] < right[j])  # the left is smaller
                or j >= len(right)):  # or if we have exhausted the right part
            new_arr.append(left[i])
            i += 1
        else:
            new_arr.append(right[j])
            j += 1
            if i < len(left):
                inversions += len(left) - i

    return new_arr, inversions


def merge_sort_inversions(arr):
    """
    We split by 3 types of inversions:
        1. Left inversions - i,j < len(arr)//2
        2. Right inversions - i,j >= len(arr)//2
        3. Split inversions - i < len(arr)//2 and j >= len(arr)//2
    We do a merge sort to effectively calculate the number of inversions.
     On every swap we increment the inversions (those are either left/right inversions) and on
     the merge, we note when we add a number from the right part. When we do such a thing, we know that there are X inversions wth that number,
     where X is the number of numbers left in the LEFT array (because those that are left are bigger than the number we are adding from the right)

     O(n)
    """
    inversions = 0
    if len(arr) <= 1:
        return arr, inversions
    if len(arr) == 2:
        if arr[0] > arr[1]:
            inversions += 1
            arr[0], arr[1] = arr[1], arr[0]
        return arr, inversions

    mid = len(arr) // 2
    left_part, left_inversions = merge_sort_inversions(arr[:mid])
    right_part, right_inversions = merge_sort_inversions(arr[mid:])

    # Merge them and get the split inversions
    merged_array, split_inversions = merge(left_part, right_part)

    return merged_array, left_inversions + split_inversions + right_inversions


def brute_force(arr: list):
    """
    O(n^2)
    """
    inversion_count = 0

    for idx, val in enumerate(arr):
        for idx_2 in range(idx+1, len(arr)):
            if arr[idx] > arr[idx_2]:
                inversion_count += 1

    return inversion_count


def main():
    arr = [1, 3, 5, 2, 4, 6]
    expected_inversion_count = 3
    assert brute_force(arr) == expected_inversion_count
    assert merge_sort_inversions(arr)[1] == expected_inversion_count

    arr = [6, 5, 4, 3, 2, 1]
    expected_inversion_count = 15
    assert brute_force(arr) == expected_inversion_count
    assert merge_sort_inversions(arr)[1] == expected_inversion_count


if __name__ == '__main__':
    main()
