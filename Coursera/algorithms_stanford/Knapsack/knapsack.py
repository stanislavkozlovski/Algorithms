with open('knapsack_1.txt', 'r') as f:
    lines = f.readlines()
    capacity, item_count = [int(p) for p in lines[0].split()]
    items, weights = [], []
    for line in lines[1:]:
        item_val, item_weight = [int(p) for p in line.split()]
        items.append(item_val)
        weights.append(item_weight)

# Matrix to store the best solution
item_choices = [[0 for _ in range(capacity)]]
# fill the item_choices matrix
for _ in range(len(items)):
    item_choices.append([0 for _ in range(capacity)])


for row_idx in range(1, len(item_choices)):
    item_value, item_weight = items[row_idx-1], weights[row_idx-1]
    for col_idx in range(capacity):
        # if col_idx ==
        if item_weight > col_idx:
            item_choices[row_idx][col_idx] = item_choices[row_idx-1][col_idx]
        else:
            item_choices[row_idx][col_idx] = max(item_choices[row_idx-1][col_idx], item_choices[row_idx-1][col_idx-item_weight] + item_value)


print(item_choices[len(item_choices)-1][len(item_choices[0])-1])