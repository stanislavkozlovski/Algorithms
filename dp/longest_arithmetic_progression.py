"""
Find longest Arithmetic Progression in an integer array and return its length. More formally, find longest sequence of indeces, 0 < i1 < i2 < … < ik < ArraySize(0-indexed) such that sequence A[i1], A[i2], …, A[ik] is an Arithmetic Progression. Arithmetic Progression is a sequence in which all the differences between consecutive pairs are the same, i.e sequence B[0], B[1], B[2], …, B[m - 1] of length m is an Arithmetic Progression if and only if B[1] - B[0] == B[2] - B[1] == B[3] - B[2] == … == B[m - 1] - B[m - 2].
Examples
1) 1, 2, 3(All differences are equal to 1)
2) 7, 7, 7(All differences are equal to 0)
3) 8, 5, 2(Yes, difference can be negative too)

Samples
1) Input: 3, 6, 9, 12
Output: 4
"""
# https://www.interviewbit.com/problems/longest-arithmetic-progression
nums = [int(p) for p in input().split()]
last_idx_and_length = {}
"""
last_idx_and_length is a nested dictionary, where it holds as key - the idx of the number
    and as value - a dictionary which holds as key: the difference and as value a tuple of the (last_idx and length)
in 1, 2, 3, 3's last_idx and length would be :
3: { 1: (1, 2) }
"""

"""
For each number, go back and save the best paths with the difference of the number with the previous numbers,
saving the best length on identical differences
"""
for idx, num in enumerate(nums):
    last_idx_and_length[idx] = {}
    idx_pointer = idx - 1

    while idx_pointer >= 0:
        difference = num - nums[idx_pointer]

        # get the length of the current sequence
        seq_length = 1
        if difference in last_idx_and_length[idx_pointer]:
            # if the previous number (idx-pointer) we're at has a length with such difference, we get that one
            # else its 1
            seq_length = last_idx_and_length[idx_pointer][difference][1]

        if difference in last_idx_and_length[idx]:
            # We have already found another difference like this one and we need to compare their lengths to save the best one!
            last_length = last_idx_and_length[idx][difference][1]
            new_length = seq_length + 1
            if new_length > last_length:
                last_idx_and_length[idx][difference] = (idx_pointer, seq_length + 1)
        else:
            # this is the only difference we have found so far and we save it
            last_idx_and_length[idx][difference] = (idx_pointer, seq_length + 1)
        idx_pointer -= 1

# print the max distance
print(max([last_idx_and_length[orig_idx][last_idx][1] for orig_idx in range(len(nums)) for last_idx in last_idx_and_length[orig_idx].keys()]
          or [1]))

