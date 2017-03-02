graph = {
    'A': ['B', 'C'],
    'B': ['A', 'C'],
    'C': ['B', 'A', 'D'],
    'D': ['C', 'E'],
    'E': ['D', 'G', 'F'],
    'F': ['E', 'G', 'H'],
    'G': ['E', 'F'],
    'H': ['F']
}
# graph = {'A': ['B', 'D'],
#          'B': ['A', 'C'],
#          'C': ['B'],
#          'D': ['A']}
vis_time = {key: None for key in graph.keys()}
low_time = {key: None for key in graph.keys()}
parents = {key: None for key in graph.keys()}

start_node = 'D'  # 'B'
visited = {start_node}
time = 0
articulation_points = []


def dfs_ap(node, vis_time, low_time, parents, visited, articulation_points):
    """
    Linear time recursive algorithm
    :param node: The current node
    :param vis_time: A dictionary holding the visited (time of discovery) for each node
    :param low_time: A dictionary holding the low_time for each node
    :param parents: A dictionary holding the parent of each node
    :param visited: A set holding all the nodes we've traversed through
    :param articulation_points: A list of all the currently found articulation points
    """
    global time
    is_articulation_point = False
    vis_time[node] = time
    low_time[node] = time
    visited.add(node)
    real_children_count = 0
    for child in graph[node]:
        if parents[node] == child:  # We're trying to visit the node we came from
            continue

        if child not in visited:
            real_children_count += 1
            parents[child] = node
            time += 1
            dfs_ap(child, vis_time, low_time, parents, visited, articulation_points)
            if low_time[child] >= vis_time[node]:
                is_articulation_point = True
            low_time[node] = min(low_time[node], low_time[child])
        else:
            low_time[node] = min(low_time[node], vis_time[child])

    if is_articulation_point or (parents[node] is None and real_children_count >= 2):
        articulation_points.append(node)

dfs_ap(start_node, vis_time, low_time, parents, visited, articulation_points)
print(articulation_points)