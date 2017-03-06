def build_prefix_array(word: str):
    prefix_array = [0 for _ in word]
    i, j = 0, 1
    while j < len(word):
        if word[j] == word[i]:  # character match!
            i += 1  # advance both i and j
            prefix_array[j] = prefix_array[j-1] + 1  # update the length
        else:
            prefix_pointer = i - 1
            i = prefix_array[prefix_pointer]

            while i > 0 and word[j] != word[i]:  # go back to identical suffixes until we get a char match
                prefix_pointer = i-1
                i = prefix_array[prefix_pointer]
            if word[j] == word[i]:
                # We have managed to recover some sort of pattern
                prefix_array[j] = prefix_array[prefix_pointer] + 1
        j += 1
    return prefix_array

text = 'abcxabcdabxabcdabcdabcy'
pattern = 'abcdabcy'
prefix_array = build_prefix_array(pattern)

def find_match(text, pattern, prefix_array):
    j = 0
    for i in range(len(text)):
        if text[i] == pattern[j]:
            j += 1
            if j == len(pattern):
                # Found a match!
                return i - len(pattern) + 1
        else:
            # non-match, try to continue off
            while j > 0:
                j = prefix_array[j-1]
                if text[i] == pattern[j]:
                    # Managed to recover some preffix and continue matching!
                    j += 1
                    break

    if j == len(pattern):
        # Found a match!
        return i - len(pattern)

substring_start_index = find_match(text, pattern, prefix_array)