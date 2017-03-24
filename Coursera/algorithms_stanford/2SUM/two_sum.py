"""
Hardest assignment so far
This is the optimal algorithm and it is the following:
    1. Sort the array
    2. Keep a pointer at the start and one at the end
    3. Get the sum of both numbers
        1. If the sum is less than -10k ot bigger than 10k, adjust the pointers
        2. If its in between, we've found a valid sum. Match it.
            1. Then, try other combinations with the same start until they stop producing valid sums
            2. Same thing with the end, keep end the same and modify that start until they stop producing valid sums
            3. increment start, decrement end

"""
nums = sorted([int(p) for p in open('input.txt', 'r').readlines()])

start, end = 0, len(nums)-1
count = 0
found_nums = {}

while start != end:
    start_num, end_num = nums[start], nums[end]
    probe_sum = start_num + end_num
    print(start, end)
    if probe_sum < -10_000:
        start += 1  # the start num is too small
    elif probe_sum > 10_000:
        end -= 1  # the end num is too big
    else:
        found_nums[probe_sum] = True
        count += 1

        # find all between
        new_start, new_end = start, end
        while True:
            # see if there are any more solutions starting with the same start
            new_end -= 1
            new_sum = nums[new_start] + nums[new_end]

            if -10_000 <= new_sum <= 10_000:
                count += 1
                found_nums[probe_sum] = True

            else:
                break

        new_start, new_end = start, end
        while True:
            # see if there are any more solutions starting with the same end
            new_start += 1
            new_sum = nums[new_start] + nums[new_end]

            if -10_000 <= new_sum <= 10_000:
                count += 1
                found_nums[probe_sum] = True

            else:
                break

        start += 1
        end -= 1

print(len(found_nums))
