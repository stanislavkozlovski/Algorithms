def create_matrix(n):
    adjacency_matrix = []

    for i in range(n):
        adjacency_matrix.append([float('inf') for _ in range(n)])
        adjacency_matrix[i][i] = 0

    return adjacency_matrix

def floyd_marshall(matrix):
    for k in range(len(matrix)):
        for i in range(len(matrix)):
            for j in range(len(matrix)):
                if matrix[i][j] > matrix[i][k] + matrix[k][j]:
                    matrix[i][j] = matrix[i][k] + matrix[k][j]


node_count = int(input().split('Nodes: ')[-1])
edge_count = int(input().split('Edges: ')[-1])
adjacency_matrix = create_matrix(node_count)

for _ in range(edge_count):
    node_a, node_b, weight = [int(p) for p in input().split()]
    adjacency_matrix[node_a][node_b] = weight
    adjacency_matrix[node_b][node_a] = weight

floyd_marshall(adjacency_matrix)
print(f'Shortest paths matrix:\n {"  ".join(str(p) for p in range(node_count))}\n{"-"*node_count*3}')
print("\n".join(" ".join(str(p) for p in row) for row in adjacency_matrix))