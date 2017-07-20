# python3
n, m = map(int, input().split())
edges = [ list(map(int, input().split())) for i in range(m) ]


colors = {}
clauses = []
var_num = 1
def printEquisatisfiableSatFormula():
    global var_num
    for a, b in edges:
        # At least one colors of the node must be true
        if a not in colors:
            colors[a] = [var_num, var_num+1, var_num+2]
            # guarantee they're unique
            clauses.append(' '.join(str(p) for p in colors[a]) + ' 0')
            clauses.append('-{} -{} 0'.format(var_num, var_num+1))
            clauses.append('-{} -{} 0'.format(var_num, var_num+2))
            clauses.append('-{} -{} 0'.format(var_num+1, var_num+2))
            var_num+=3

        if b not in colors:
            colors[b] = [var_num, var_num+1, var_num+2]
            clauses.append('-{} -{} 0'.format(var_num, var_num+1))
            clauses.append('-{} -{} 0'.format(var_num, var_num+2))
            clauses.append('-{} -{} 0'.format(var_num+1, var_num+2))
            # clauses.append('{} {} 0'.format(var_num, var_num+1))
            # clauses.append('{} {} 0'.format(var_num+1, var_num+2))
            # clauses.append('{} {} 0'.format(var_num, var_num+2))
            var_num+=3
            clauses.append(' '.join(str(p) for p in colors[b]) + ' 0')

        # Assert each of the colors are not equal
        for idx in range(len(colors[a])):
            # A color and B color are not both 1 but can be 0 :)
            clauses.append('-{} -{} 0'.format(colors[a][idx], colors[b][idx]))


printEquisatisfiableSatFormula()
print(len(clauses), var_num+1)
print('\n'.join(p for p in clauses))
