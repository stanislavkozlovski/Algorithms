import random
"""
The non-random quick sort chooses the start element as an index and swaps every element that is after and less than it.
Example:
    [10, 20, 5, 3]
    pivot = 10. i=0, j=0
    i points to the closest bigger element after 10
    j is used to traverse the array
    # 1. j=1, 20 > 10, we're OK
    # 2. j=2, 5 < 10, we need to swap. We swap 5 with the closest bigger number to 10, which is 20
        swap i with j, basically (20 with 5)
        # 2.1. We end up with [10, 5, 20, 3]
               We then need to swap it with the 10 itself
        swap i with pivot
        # 2.2. [5, 10, 20, 3]
        pivot is now at i, so we make i = i+1
    i = 2, j = 3 on next iter
    [3, 10, 20, 5]
    # 3. 3 < 10, we need to swap again.
        # 3.1.
             swap i with j (20 with 3)
             [5, 10, 3, 20]
        # 3.2.
             swap i with pivot index
             [5, 3, 10, 20]
    NOTE: In this scenario, the array is small and almost sorted.
        What's left now, is to recursively do the same thing for the arrays
            - LEFT of the pivot
            - RIGHT of the pivot
        And recursively downwards in them and so on and so on until we reach an array of size 1 or less, which
            is obviously sorted
"""
START, END, MEDIAN = 1, 2, 3


def _non_random_quick_sort(array, start, end, sort_type: int):
    """
    Pick the start as a pivot and partition the array around it, so that everything on the left is less
        and everything on the right is bigger
    Supports three kinds of pivot picking strategies:
            1. Start - always picks the first element
            2. End - always picks the last element
            3. Median - picks the middle element out of Start, Middle and End
    """
    if end <= start:
        # Array is of length 1 or less, meaning it is sorted!
        return

    if sort_type == END:
        # swap with start
        array[start], array[end] = array[end], array[start]
    elif sort_type == MEDIAN:
        # find the median between start, mid and end number
        length = (end-start)+1

        med_idx = (length // 2) - 1 if length % 2 == 0 else length // 2
        med = array[med_idx]
        first = array[start]
        end_el = array[end]
        piv_index = {
            med: med_idx,
            first: start,
            end_el: end
        }
        # swap with start
        median = piv_index[list(sorted([med, first, end_el]))[1]]
        array[start], array[median] = array[median], array[start]

    pivot_idx = start
    pivot_item = array[pivot_idx]
    i, j = start + 1, start + 1
    # i points to the first item after pivot
    # j points to the element we're at

    while j <= end:
        if array[j] < pivot_item:  # element after pivot is smaller, we need to swap
            # swap with element after pivot
            # ex: pivot=5, i=10, [2, 3, 5, 10, 1]
            array[i], array[j] = array[j], array[i]
            # ex: [2, 3, 5, 1, 10]
            # swap with pivot
            array[pivot_idx], array[i] = array[i], array[pivot_idx]
            # ex: [2, 3, 1, 5, 10]
            pivot_idx = i
            i = pivot_idx + 1
        j += 1

    # recursion up until we hit partition every part
    _non_random_quick_sort(array, start, pivot_idx-1, sort_type)  # do the same for the left part
    _non_random_quick_sort(array, pivot_idx+1, end, sort_type)  # do the same for the right part


def non_random_quick_sort(array: list, sort_type: int):
    _non_random_quick_sort(array, 0, len(array)-1, sort_type)
    return array


print(non_random_quick_sort([33, 31, 23], START))
print(non_random_quick_sort([3, 3, 25, 4, -1120, 1, 2, -3, -10, 250, -402], START))
print(non_random_quick_sort([3, 3, 25, 4, -1120, 1, 2, -3, -10, 250, -402], END))
print(non_random_quick_sort([3, 3, 25, 4, -1120, 1, 2, -3, -10, 250, -402], MEDIAN))
