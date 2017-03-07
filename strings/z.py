string = 'aabxaabxcaabxaabxay'


def generate_z_array(string) -> []:
    z_list = [None for _ in string]
    z_list[0] = 0

    i, j = 0, 1
    left, right = 0, 0
    current_count = 0
    while j < len(string):
        print(f'Comparing {string[i]} with {string[j]}')
        if string[i] == string[j]:
            current_count += 1
            i += 1
        else:
            to_skip = False
            z_list[j - current_count] = current_count

            left = j - current_count + 1
            right = j
            left_point = 1
            while left < right:
                old_value = z_list[left_point]
                if old_value + left < right:
                    z_list[left] = old_value
                    left += 1
                    left_point += 1
                else:
                    i = left_point

                    current_count = old_value
                    if left + current_count >= len(string):
                        print((len(string)-1) - left)
                        print('shte uspeem')
                        current_count = (len(string)-1) - left
                    to_skip = True

                    break
                    pass
                pass
            if to_skip: continue
            current_count = 0
            i = 0

            print(f'Z Box at {left}:{right}')

            if string[i] == string[j]:
                current_count += 1
                i += 1
            else:
                z_list[j - current_count] = current_count
        j += 1
    return z_list
