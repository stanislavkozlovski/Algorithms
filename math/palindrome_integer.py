# https://www.interviewbit.com/problems/palindrome-integer/
# Determine whether an integer is a palindrome. Do this without extra space.

from math import log10, floor


class Solution:
    # @param A : integer
    # @return a boolean value ( True / False )
    def isPalindrome(self, A):
        num = A
        if num < 0:
            return False
        elif num < 10:
            return True

        return num == self.create_reversed_num(num)


    def create_reversed_num(self, num):
        new_num = 0

        while num != 0:
            last_digit = num % 10
            new_num = (new_num * 10) + last_digit
            num //= 10

        return new_num

