from collections import deque


def path_exists(graph, start, end, parents):
    """ BFS to tell if there is a valid path in between the two nodes"""
    visited = set()
    queue = deque()
    queue.append(start)
    while queue:
        node_a = queue.popleft()
        visited.add(node_a)
        for node_b, weight in enumerate(graph[node_a]):
            if weight > 0 and node_b not in visited:
                parents[node_b] = node_a
                # if node_b == end:
                #     return True
                queue.append(node_b)

    return end in visited


def edmonds_karp(graph, source, sink):
    parents = {}
    max_flow = 0

    while path_exists(graph, source, sink, parents):  # bfs to fill up the parents and check for a path
        curr_min_flow = float('Inf')

        # Traverse the path and get the minimum flow from it
        prev = parents[sink]
        node = sink
        while prev != source:
            edge_flow = graph[prev][node]
            if edge_flow < curr_min_flow:
                curr_min_flow = edge_flow
            node = prev
            prev = parents[prev]

        # Traverse the path and update the flow
        node = sink
        prev = parents[sink]
        while prev != source:
            graph[prev][node] -= curr_min_flow  # reduce the possible flow that the edge can take now
            graph[node][prev] += curr_min_flow  # not sure why, but it is technically correct
            node = prev
            prev = parents[prev]
        max_flow += curr_min_flow

    return max_flow

graph = [[0, 16, 13, 0, 0, 0],
         [0, 0, 10, 12, 0, 0],
         [0, 4, 0, 0, 14, 0],
         [0, 0, 9, 0, 0, 20],
         [0, 0, 0, 7, 0, 4],
         [0, 0, 0, 0, 0, 0]]
print(edmonds_karp(graph, 0, 5))