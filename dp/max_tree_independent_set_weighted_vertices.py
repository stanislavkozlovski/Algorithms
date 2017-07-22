"""
Given a Tree with Vertices which have weights, find the maximum weighted independent set
    (no vertices in the set should be directly connected)
"""

"""
node name: node weight
    ____1:3___________
   /     |            \
  2:5    3:1          4:6
  |      |            /  \
 5:2    6:3         7:7   8:2
                  /  |  \
               9:1 10:2 11:2
               
  The optimal solution here is taking nodes 2, 4, 6, 9, 10, 11
"""
node_weights = {
    1: 3,
    2: 5,
    3: 1,
    4: 6,
    5: 2,
    6: 3,
    7: 7,
    8: 2,
    9: 1,
    10: 2,
    11: 1
}
tree = {
    1: {2, 3, 4},
    2: {5},
    3: {6},
    4: {7, 8},
    5: {},
    6: {},
    7: {9, 10, 11},
    8: {},
    9: {},
    10: {},
    11: {}
}

# Dict to hold key: node - value: tuple(max_weight, independent_set)
best_sets = {}

def get_max_set(node: int):
    global best_sets, node_weights
    if node not in best_sets:
        if len(tree[node]) == 0:
            # No children, independent set is obvious
            best_sets[node] = (node_weights[node], {node})
            return best_sets[node]

        # Node has children, get the max of his children's independent set or of his children's children
        children_max_is = (0, set())
        for child in tree[node]:
            child_max_is = get_max_set(child)
            child_is = children_max_is[1]
            child_is.update(child_max_is[1])
            children_max_is = (children_max_is[0] + child_max_is[0],  child_is)
            
        grandchildren_max_is = (node_weights[node], {node})
        for child in tree[node]:
            for grandchild in tree[child]:
                child_max_is = get_max_set(grandchild)
                current_is = grandchildren_max_is[1]
                current_is.update(child_max_is[1])
                grandchildren_max_is = (grandchildren_max_is[0] + child_max_is[0], current_is)

        if grandchildren_max_is[0] > children_max_is[0]:
            # grandchildren IS is better
            best_sets[node] = grandchildren_max_is
        else:
            # children IS is better
            best_sets[node] = children_max_is
    return best_sets[node]

print(get_max_set(1))
