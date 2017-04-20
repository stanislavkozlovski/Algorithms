best_independent_sets = {
    0: 1,
    1: 4,
}
paths = {
    # Hold the previous node of saved one
    0: -1,
    1: -1,
}
graph = [1, 4, 5, 4, 9, 12]


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
    while curr_node != -1:
        path.append(curr_node)
        curr_node = paths[curr_node]

    return list(reversed(path))

print(compute_paths(graph, 5))
print(reconstruct_path(paths, 5))