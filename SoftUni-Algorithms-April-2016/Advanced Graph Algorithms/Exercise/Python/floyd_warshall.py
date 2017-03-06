from copy import deepcopy

graph = [[0, 16, 13, 0, 0, 0],
         [0, 0, 10, 12, 0, 0],
         [0, 4, 0, 0, 14, 0],
         [0, 0, 9, 0, 0, 20],
         [0, 0, 0, 7, 0, 4],
         [0, 0, 0, 0, 0, 0]]
distances = []

for idx, row in enumerate(graph):
    new_row = []
    for idx_2, col in enumerate(row):
        new_row.append(col if col != 0 else float('Inf'))
    distances.append(new_row)

for k in range(len(graph)):
    for i in range(len(graph)):
        for j in range(len(graph)):
            new_distance = distances[i][k] + distances[k][j]
            if new_distance < distances[i][j]:
                distances[i][j] = new_distance
