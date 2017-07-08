# python3

class Edge:
    def __init__(self, u, v, capacity):
        self.u = u
        self.v = v
        self.capacity = capacity
        self.flow = 0



# This class implements a bit unusual scheme for storing edges of the graph,
# in order to retrieve the backward edge for a given edge quickly.
class FlowGraph:
    def __init__(self, n):
        # List of all - forward and backward - edges
        self.edges = []
        # These adjacency lists store only indices of edges in the edges list
        self.graph = [[] for _ in range(n)]

    def add_edge(self, from_, to, capacity):
        # Note that we first append a forward edge and then a backward edge,
        # so all forward edges are stored at even indices (starting from 0),
        # whereas backward edges are stored at odd indices.
        forward_edge = Edge(from_, to, capacity)
        backward_edge = Edge(to, from_, 0)
        self.graph[from_].append(len(self.edges))
        self.edges.append(forward_edge)
        self.graph[to].append(len(self.edges))
        self.edges.append(backward_edge)

    def size(self):
        return len(self.graph)

    def get_ids(self, from_):
        return self.graph[from_]

    def get_edge(self, id):
        return self.edges[id]

    def add_flow(self, id, flow):
        # To get a backward edge for a true forward edge (i.e id is even), we should get id + 1
        # due to the described above scheme. On the other hand, when we have to get a "backward"
        # edge for a backward edge (i.e. get a forward edge for backward - id is odd), id - 1
        # should be taken.
        #
        # It turns out that id ^ 1 works for both cases. Think this through!
        self.edges[id].flow += flow
        self.edges[id ^ 1].flow -= flow  # decrease the backward edge's flow


def read_data():
    vertex_count, edge_count = map(int, input().split())
    graph = FlowGraph(vertex_count)
    for _ in range(edge_count):
        u, v, capacity = map(int, input().split())
        graph.add_edge(u - 1, v - 1, capacity)
    return graph


def bfs_closest_path(graph, from_, to):
    visited = set()
    to_visit = [from_]
    prev = [None for _ in graph.graph]

    while len(visited) != graph.size() and len(to_visit) != 0:
        curr_node = to_visit.pop(0)
        visited.add(curr_node)
        edges = graph.get_ids(curr_node)
        for edg_idx in edges:
            edg = graph.get_edge(edg_idx)
            if edg.v not in visited and edg.capacity > edg.flow:
                to_visit.append(edg.v)
                visited.add(edg.v)
                prev[edg.v] = (edg, edg_idx)
                if edg.v == to:
                    break
    if prev[to] is None:
        return []
    prev_edge, edge_idx = prev[to]
    path = []
    while True:
        path.append((prev_edge, edge_idx))

        if prev[prev_edge.u] is None:
            break
        prev_edge, edge_idx = prev[prev_edge.u]
    return list(reversed(path))


def max_flow(graph, from_, to):
    flow = 0
    while True:
        # Residual graph is constructed on flow increase

        # find s-t path
        path = bfs_closest_path(graph, from_, to)
        if len(path) == 0:
            return flow

        min_flow_increase = float('inf')
        for edg, edg_idx in path:
            curr_flow = edg.capacity - edg.flow

            if curr_flow < min_flow_increase:
                min_flow_increase = curr_flow

        # increase actual flow
        for edg, edg_idx in path:
            graph.add_flow(edg_idx, min_flow_increase)
        flow += min_flow_increase

    return flow


if __name__ == '__main__':
    graph = read_data()
    print(max_flow(graph, 0, graph.size() - 1))
