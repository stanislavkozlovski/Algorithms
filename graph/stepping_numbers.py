#  https://www.interviewbit.com/problems/stepping-numbers/

class Solution:
    # @param A : integer
    # @param B : integer
    # @return a list of integers
    def stepnum(self, A, B):
        self.min_range = A
        self.max_range = B
        self.nums = []

        if self.is_in_range(0):
            self.nums.append(0)
        for num in range(1, 10):
            self.build_stepping_numbers(num)

        return sorted(self.nums)

    def build_stepping_numbers(self, num):
        if self.is_in_range(num):
            self.nums.append(num)
        if num > self.max_range:
            return

        last_digit = num % 10
        if last_digit == 0:
            self.build_stepping_numbers(num*10 + last_digit+1)
        elif last_digit == 9:
            self.build_stepping_numbers(num*10 + last_digit-1)
        else:
            self.build_stepping_numbers(num*10 + last_digit-1)
            self.build_stepping_numbers(num*10 + last_digit+1)

    def is_in_range(self, num):
        return self.min_range <= num <= self.max_range

print(Solution().stepnum(10, 20))