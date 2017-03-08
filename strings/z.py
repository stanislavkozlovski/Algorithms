string = 'aabxaabxcaabxaabxay'


def generate_z_array(string) -> []:
    """
    Generate a Z array for a given string in linear O(n) time using DP
    """
    z_list = [None for _ in string]
    z_list[0] = 0

    i, j = 0, 1
    left, right = 0, 0
    current_count = 0
    while j < len(string):
        print(f'Comparing {string[i]} with {string[j]}')

        if string[i] == string[j]:
            # There is a match, increment our counter and i
            current_count += 1
            i += 1
        else:
            # We've broken our match streak, save the results and try to use DP to reuse older results
            to_skip = False

            # Save the current results
            z_list[j - current_count] = current_count

            # Form our Z-Box with left-right bounds
            left = j - current_count + 1
            right = j
            left_pointer = 1  #  holds track of what index we're at in the Z box and in the part we're reusing
            while left < right:  # iterate through z-box
                old_value = z_list[left_pointer]
                if old_value + left < right:
                    # We can simply reuse the old calculation and we do so
                    z_list[left] = old_value
                    left += 1
                    left_pointer += 1
                else:
                    # We cannot fully reuse the old calculation since it goes out of the z-box
                    # this means we need to further compare characters
                    i = left_pointer

                    current_count = old_value
                    # Check if we're going out of bounds of our string and if so, simply reset the count to the characters that remain
                    if left + current_count >= len(string):
                        print('shte uspeem')
                        current_count = (len(string)-1) - left
                    to_skip = True
                    break
            if to_skip: continue  # we want to continue matching

            # We've broken our match streak
            current_count = 0
            i = 0

            # We reset i and check if we can start a new match from here
            if string[i] == string[j]:
                current_count += 1
                i += 1
        j += 1
    return z_list
