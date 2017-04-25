# https://en.wikipedia.org/wiki/Sequence_alignment
def reconstruct_sequences(orig_first_seq, orig_second_seq, penalties):
    """
    Returns the resulting sequences in the optimal sequence alignment!
    """
    start_r, start_c = len(orig_first_seq), len(orig_second_seq)
    f_seq, s_seq = '', ''
    while start_r != 0 and start_c != 0:
        # Fill in the remaining gaps if one sequence has ended
        if start_r == 0:
            s_seq += start_c * '-'
            break
        elif start_c == 0:
            f_seq += start_r * '-'
            break

        words_match_cost = penalties[start_r-1][start_c-1]
        f_seq_gap_cost = penalties[start_r][start_c-1]
        s_seq_gap_cost = penalties[start_r-1][start_c]
        if words_match_cost <= f_seq_gap_cost and words_match_cost <= s_seq_gap_cost:
            # words match is optimal!
            f_seq += orig_first_seq[start_r-1]
            s_seq += orig_second_seq[start_c-1]
            start_r -= 1
            start_c -= 1
        elif f_seq_gap_cost <= words_match_cost and s_seq_gap_cost <= words_match_cost:
            # first sequence gap is optimal
            f_seq += '-'
            s_seq += orig_second_seq[start_c-1]
            start_c -= 1
        else:
            # second sequence gap is optimal
            s_seq += '-'
            f_seq += orig_first_seq[start_r-1]
            start_r -= 1
    return ''.join(reversed(f_seq)), ''.join(reversed(s_seq))


def create_penalties_matrix(first_seq: str, second_seq: str, gap_cost: int):
    """
    return a 2D matrix, i representings the letters for the first sequence and j - for the second
    """
    penalties = []

    for _ in range(len(first_seq) + 1):
        penalties.append([None for _ in range(len(second_seq) + 1)])

    # initialize the matrix
    # the penalty cost for the Ith letter compared to the 0th of the other sequence is exactly I gaps,
    # as there must be so many gaps to match both letters
    for i in range(len(first_seq) + 1):
        penalties[i][0] = gap_cost * i
    for i in range(len(second_seq) + 1):
        penalties[0][i] = gap_cost * i

    return penalties


first_seq = "AGGGCT"
second_seq = "AGGCA"
gap_cost = 10
mismatch_cost = 20
penalties = create_penalties_matrix(first_seq, second_seq, gap_cost)

for i in range(1, len(penalties)):
    for j in range(1, len(penalties[0])):
        cost = mismatch_cost if first_seq[i-1] != second_seq[j-1] else 0
        # case 1 - words either match or dont
        words_match = cost + penalties[i-1][j-1]
        # case 2 - there is a gap at the first sequence
        first_seq_gap = gap_cost + penalties[i][j - 1]
        # case 3 - there is a gap at the second sequence
        second_seq_gap = gap_cost + penalties[i - 1][j]
        # the optimal solution will never have two gaps at the same space, it doesnt make sense
        penalties[i][j] = min(min(words_match, first_seq_gap), second_seq_gap)

# OPTIMAL SOLUTION IS
# AGGGCT
# AGG-CA
# total penalty - 10 + 20 = 30
from pprint import pprint
pprint(penalties)
answer = penalties[-1][-1]
resulting_first_seq, resulting_second_seq = reconstruct_sequences(first_seq, second_seq, penalties)
print(f'Resulting sequences are \n{resulting_first_seq}\n{resulting_second_seq}')