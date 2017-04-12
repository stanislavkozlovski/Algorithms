# https://www.interviewbit.com/problems/max-non-negative-subarray
class Solution:
    # @param A : list of integers
    # @return a list of integers
    def maxset(self, A):
        nums = A
        i = 0
        bestest_sum = -1
        last_idx = None
        start_idx = None
        while i < len(nums):
            if nums[i] < 0:
                i += 1
                continue
            best_sum = nums[i]
            curr_sum = nums[i]
            best_end_idx = i

            # Round up the sum up till the next negative number or array end
            last_j = None
            for j in range(i+1, len(nums)):
                if nums[j] < 0:
                    break
                curr_sum += nums[j]
                if curr_sum >= best_sum:
                    best_sum = curr_sum
                    best_end_idx = j
                last_j = j

            if best_sum > bestest_sum:
                bestest_sum = best_sum
                last_idx = best_end_idx
                start_idx = i
            elif best_sum == bestest_sum and best_end_idx-i > last_idx-start_idx:
                last_idx = best_end_idx
                start_idx = i
            # update i by making it the last non negative number in the sequence
            i += 1
            if last_j is not None:
                i = last_j
        if start_idx is None:
            # no positive numbers
            return []

        return nums[start_idx:last_idx+1]
