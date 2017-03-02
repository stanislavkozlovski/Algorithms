unsorted_array = [3, 44, 38, 5, 47, 15, 36, 26]

for i in range(len(unsorted_array)):
    sorted_element = unsorted_array[i]
    idx_to_insert = i

    for j in reversed(range(0, i)):
        if sorted_element < unsorted_array[j]:
            # Found a better index to insert, shift the bigger element to the right
            # and add it's index as the new index for our number
            idx_to_insert -= 1
            unsorted_array[j+1] = unsorted_array[j]

    unsorted_array[idx_to_insert] = sorted_element

print(unsorted_array)
