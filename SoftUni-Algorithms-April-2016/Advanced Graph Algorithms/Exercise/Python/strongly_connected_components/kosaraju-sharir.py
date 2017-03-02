graph = {
    0: [1, 11, 13],
    1: [6],
    2: [0],
    3: [4],
    4: [3, 6],
    5: [13],
    6: [0, 11],
    7: [12],
    8: [6, 11],
    9: [0],
    10: [4, 6, 10],
    11: [],
    12: [7],
    13: [2, 9]
}
# graph = {
#     0: [1],
#     1: [2, 4],
#     2: [3],
#     3: [1],
#     4: []
# }


def visit(vertex, visited: list, graph: dict, dfs_path: list):
    if not visited[vertex]:
        visited[vertex] = True
        for n in graph[vertex]:
            visit(n, visited, graph, dfs_path)
        dfs_path.append(vertex)


def assign(vertex: int, root: int, strong_components: list, graph: dict):
    """ DFS on the reversed graph to build the strongly-connected components """
    if strong_components[vertex] == -1:
        strong_components[vertex] = root
        for n in graph[vertex]:
            assign(n, root, strong_components, graph)


def reverse_graph(graph):
    new_graph = {}
    for node, children in graph.items():
        if node not in new_graph:
            new_graph[node] = []
        for child in children:
            if child not in new_graph:
                new_graph[child] = []
            new_graph[child].append(node)
    return new_graph

"""
1 - For every unvisited node, use DFS recursively to traverse all nodes, storing the overall DFS path
2 - Reverse every edge in the graph
3 - For every node in the DFS path which has not been assigned to a component,
        start another DFS recursively assigning to all of its children the connected component root.
"""
visited = [False for _ in range(len(graph))]

# each index holds the index of the ROOT of the strongly-connected component it belongs to
strong_components = [-1 for _ in range(len(graph))]

dfs_path = []
for vertex in graph.keys():
    visit(vertex, visited, graph, dfs_path)
print(f'The DFS path is {dfs_path}')

# reverse the graph
reversed_graph = reverse_graph(graph)
print("The graph has been reversed.")
for el in dfs_path[::-1]:  # Iterate dfs path from the end
    assign(el, el, strong_components, reversed_graph)
print(strong_components)
