#  https://www.interviewbit.com/problems/find-duplicate-in-array/
class Solution:
    # @param A : tuple of integers
    # @return an integer
    def repeatedNumber(self, A):
        nums = sorted(A)
        for i in range(len(nums)-1):
            if nums[i] == nums[i+1]:
                return nums[i]
        return -1