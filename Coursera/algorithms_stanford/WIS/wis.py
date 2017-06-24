"""
Computes the maximum weight independent set of a graph
"""
import sys

sys.setrecursionlimit(2000)
best_independent_sets = {
    # Holds the best IS for the given index
}
paths = {
    # Holds the previous node of saved one
}


def main():
    graph = construct_graph()
    global paths, best_independent_sets
    paths[0], paths[1] = -1, -1
    best_independent_sets[0], best_independent_sets[1] = graph[0], graph[1]

    print(compute_paths(graph=graph, idx=len(graph)-1))
    max_set_weight = max(best_independent_sets.values())
    print(max_set_weight)
    for idx, val in best_independent_sets.items():
        if val == max_set_weight:
            idx_of_last_el = idx
            break

    best_paths = set(reconstruct_path(paths, idx_of_last_el))
    print(best_paths)
    inquired_indices = [1, 2, 3, 4, 17, 117, 517, 997]
    result = ''.join('1' if inq_idx-1 in best_paths else '0' for inq_idx in inquired_indices)
    print(result)
    print(list(reversed(reconstruct_from_best_is(list(best_independent_sets.values())))))


def construct_graph():
    with open('test.input.txt', 'r') as f:
        graph = [int(p) for p in f.readlines()[1:]]
    return graph


def compute_paths(graph, idx):
    global best_independent_sets, paths
    if idx in best_independent_sets:
        return best_independent_sets[idx]
    # Compute the best IS without the current node
    best_without = compute_paths(graph, idx-1)
    # Compute the best IS with the current node
    best_curr = compute_paths(graph, idx-2) + graph[idx]
    # Save the current path as the best of both (we either take current node or dont)
    best_independent_sets[idx] = max(best_without, best_curr)
    paths[idx] = idx-2 if best_curr > best_without else idx-1
    return best_independent_sets[idx]


def reconstruct_path(paths, idx):
    path = [idx]
    curr_node = paths[idx]
    prev_node = idx
    while curr_node != -1:
        # if abs(curr_node-prev_node) > 1:
        path.append(curr_node)
        prev_node = curr_node
        curr_node = paths[curr_node]

    return list(reversed(path))

if __name__ == '__main__':
    main()
