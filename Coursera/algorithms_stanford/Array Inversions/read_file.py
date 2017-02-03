"""
Programming Assignment #2:
This file contains all of the 100,000 integers between 1 and 100,000 (inclusive) in some order, with no integer repeated.

Your task is to compute the number of inversions in the file given, where the ith row of the file indicates the ith entry of an array.
"""
from count_array_inversions import merge_sort_inversions


file = open('numbers.txt').readlines()
numbers = [int(part.strip()) for part in file]
print(merge_sort_inversions(numbers)[1])
#  2407905288
