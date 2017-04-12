class Edge:
    def __init__(self, a, b, weight):
        self.a = a
        self.b = b
        self.weight = weight

    def __gt__(self, other):
        return self.weight > other.weight


def read_input() -> [Edge]:
    edges = []
    nodes = {}
    with open('prim_input.txt') as f:
        lines = f.readlines()
        for line in lines[1:]:
            node_a, node_b, weight = [int(p) for p in line.split()]
            cur_edge =Edge(node_a, node_b, weight)
            edges.append(cur_edge)
            if node_a not in nodes:
                nodes[node_a] = []
            if node_b not in nodes:
                nodes[node_b] = []
            nodes[node_a].append(cur_edge)
            nodes[node_b].append(cur_edge)
    return edges, nodes


vertices = {1}
from queue import PriorityQueue

pq = PriorityQueue()
edges, nodes = read_input()
ov_cost = 0
for edge in nodes[1]:
    pq.put(edge)
while not pq.empty():
    edg = pq.get()
    if edg.a not in vertices:
        ov_cost += edg.weight
        vertices.add(edg.a)
        for edg in nodes[edg.a]:
            pq.put(edg)
    elif edg.b not in vertices:
        ov_cost += edg.weight
        vertices.add(edg.b)
        for edg in nodes[edg.b]:
            pq.put(edg)

print(ov_cost)
print(len(vertices))