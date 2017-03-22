"""
Given a distance ‘dist, count total number of ways to cover the distance with 1, 2 and 3 steps.
Input:  n = 3
Output: 4
Below are the four ways
 1 step + 1 step + 1 step
 1 step + 2 step
 2 step + 1 step
 3 step

Input:  n = 4
Output: 7
"""
saved_ways = {0: 0, 1: 1, 2: 2, 3: 4}


def naive_get_ways_to_cover(dist: int):
    if 1 >= dist >= 0:
        return dist
    if dist == 2:
        return 2
    if dist == 3:
        return 4
    return naive_get_ways_to_cover(dist-1) + naive_get_ways_to_cover(dist-2) + naive_get_ways_to_cover(dist-3)


def get_ways_to_cover(dist: int):
    """
    The naive algorithm splits all possible ways
    Similar to the naive recursive fibonacci solution, we get a lot of repeating values that we compute over and over
    That is why we save our computations in a dictionary
    """
    if dist in saved_ways:
        return saved_ways[dist]
    for step in [1, 2, 3]:
        dist_after_step = dist - step
        if dist_after_step not in saved_ways:
            saved_ways[dist_after_step] = get_ways_to_cover(dist_after_step)

    # if dist-1 not in saved_ways:
    #     saved_ways[dist-1] = get_ways_to_cover(dist-1)
    # if dist-2 not in saved_ways:
    #     saved_ways[dist-2] = get_ways_to_cover(dist - 2)
    # if dist-3 not in saved_ways:
    #     saved_ways[dist-3] = get_ways_to_cover(dist - 3)

    saved_ways[dist] = saved_ways[dist-1] + saved_ways[dist-2] + saved_ways[dist-3]

    return saved_ways[dist]

n = int(input())

print(get_ways_to_cover(n))
print(naive_get_ways_to_cover(n))
