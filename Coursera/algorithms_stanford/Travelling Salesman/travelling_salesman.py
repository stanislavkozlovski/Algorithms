""" Build the graph

                  _____0____
                /     |    \
           10C /      |20C  \ 15C
             /  _____ 3____  \
            /  /25C     30C\  \
           1__/_____________\__2
                    35C


The minimum cost path is 0-1-3-2-0 = 10 + 25 + 30 + 15 = 80

"""
from itertools import combinations

# a matrix representation of the graph
graph = [
    [0, 10, 15, 20],
    [10, 0, 35, 25],
    [15, 35, 0, 30],
    [20, 25, 30, 0]
]

nodes = [0, 1, 2, 3]

# build the A dp matrix
A = {}
for i in range(1, len(nodes)+1):
    for possible_set in combinations(nodes, i):
        if 0 not in possible_set:
            continue
        for node in nodes:
            # path with possible set to the end node is infinity for now
            A[(possible_set, node)] = float('inf')

# base case
A[((0,), 0)] = 0

for subproblem_size in range(2, len(nodes)+1):
    for valid_set in (set(possible_set[0]) for possible_set in A.keys() if len(possible_set[0]) == subproblem_size):
        for node in (node for node in nodes if node != 0 and node in nodes):
            new_set = valid_set - {node}
            minimum_path_cost = A[(tuple(new_set), node)]

            for k in nodes:
                new_path_cost = A[(tuple(new_set), k)] + graph[k][node]
                if minimum_path_cost > new_path_cost:
                    minimum_path_cost = new_path_cost

            A[(tuple(valid_set), node)] = minimum_path_cost

# now find the minimum path that goes through all the nodes
all_nodes_set = {0, 1, 2, 3}
possible_pairs = [((0, 1, 2, 3), possible_end_node) for possible_end_node in nodes]
# find the minimum path and pair
end_node, min_cost = None, float('inf')
for pair in possible_pairs:
    end_node = pair[1]
    path_cost = A[pair]
    path_cost += graph[end_node][0]  # add in the last path back
    if path_cost < min_cost:
        min_cost = path_cost
        end_node = end_node

minimum_tsp_cost = min_cost
print(minimum_tsp_cost)
