# python3
n, m = map(int, input().split())
edges = {node: set() for node in range(1, n+1)}
for _ in range(m):
    node_a, node_b = [int(p) for p in input().split()]
    if node_a not in edges:
        edges[node_a] = set()
    if node_b not in edges:
        edges[node_b] = set()

    edges[node_a].add(node_b)
    edges[node_b].add(node_a)


class Variable:
    def __init__(self, var_number, vertex, path_step):
        self.var_num = var_number
        self.vertex = vertex
        self.path_step = path_step
    #
    # def __str__(self):
    #     return f'Vertex {self.vertex} VarNum {self.var_num} at step {self.path_step}'
    #
    # def __repr__(self):
    #     return self.__str__()


def get_variable_by_step(node, variables):
    return {var_obj.path_step: var_obj for var_obj in variables[node]}

var_num = 1
clauses = []
# This solution prints a simple satisfiable formula
# and passes about half of the tests.
# Change this function to solve the problem.
def printEquisatisfiableSatFormula():
    global var_num
    # Create a hypothetical path 1-2-3-4-5..n for each Vertex
    variables = {}
    for vertex in range(1, n+1):
        variables[vertex] = []
        for path_node in range(1, n+1):
            variables[vertex].append(Variable(var_num, vertex, path_node))
            var_num += 1

        clauses.append(' '.join(str(p.var_num) for p in variables[vertex]) + ' 0')
        # assert that only one is visited
        for idx, var1 in enumerate(variables[vertex]):
            for var2 in variables[vertex][idx+1:]:
                clauses.append('-{} -{} 0'.format(var1.var_num, var2.var_num))

    # For each step, assert that there is one there :)
    for step in range(1, n+1):
        variables_at_step = []
        # Assert that AT LEAST ONE is there
        for key, vars_list in variables.items():
            for var in vars_list:
                if var.path_step == step:
                    variables_at_step.append(var)
        clauses.append(' '.join(str(p.var_num) for p in variables_at_step) + ' 0')

        #  Assert that only one node is here
        for idx, var1 in enumerate(variables_at_step):
            for var2 in variables_at_step[idx+1:]:
                clauses.append('-{} -{} 0'.format(var1.var_num, var2.var_num))

    # Assert each NODE CANNOT go to that which it has no edge to
    for node in range(1, n+1):
        # A list of dictionaries, key - step value: object
        node_steps = get_variable_by_step(node, variables)
        nodes_it_has_no_connection_to = [get_variable_by_step(opposite_node, variables) for opposite_node in range(1, n+1)
                                         if opposite_node not in edges[node] and opposite_node != node]
        for step in range(1, n):
            var_step = node_steps[step]
            for opp_node_steps in nodes_it_has_no_connection_to:
                opp_step = opp_node_steps[step+1]
                # assert there is no consecutive step between the two, as they are not connected
                clause = '-{} -{} 0'.format(var_step.var_num, opp_step.var_num)
                clauses.append(clause)

                # clauses.append('-{} -{} 0'.format(var_step.var_num, opp_step_2.var_num))

printEquisatisfiableSatFormula()
print('{} {}'.format(len(clauses), var_num-1))
print('\n'.join(clauses))
