arr = [1, 2, 3, 1]
arr_sum = sum(arr)

dp = [None for _ in range(len(arr) + 1)]
for i in range(len(arr) + 1):
    dp[i] = [False for _ in range(arr_sum + 1)]

# initialize first column as true, 0 sum is possible with all elements
for i in range(len(arr) + 1):
    dp[i][0] = True


# fill the partition table in a bottom up manner
for i in range(1, len(arr) + 1):
    for j in range(1, arr_sum + 1):
        dp[i][j] = dp[i-1][j] # if ith element is excluded

        if arr[i-1] <= j:  # if there is any chance to reach this sum at all
            leftover_sum = j - arr[i-1]
            if dp[i-1][leftover_sum] == True:  # if ith element is included
                dp[i][j] = True

min_diff = float('inf')
# find the largest sum which is less than sum/2, prefering the largest
for sm in reversed(range(arr_sum//2 + 1)):
    if dp[len(arr)][sm] == True:
        min_diff = arr_sum - (2*sm)  # not sure how this is guaranteed to be correct
        break

print(min_diff)
