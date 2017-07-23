class FindStronglyConnectedComponents:
    def __init__(self, graph):
        self.graph = graph
        # self.visited = [False for _ in range(len(self.graph))]
        self.visited = {var: False for var in self.graph.keys()}

    def visit(self, vertex, visited: list, graph: dict, dfs_path: list):
        if not visited[vertex]:
            visited[vertex] = True
            for n in graph[vertex]:
                self.visit(n, visited, graph, dfs_path)
            dfs_path.append(vertex)

    def assign(self, vertex: int, root: int, strong_components: list, graph: dict):
        """ DFS on the reversed graph to build the strongly-connected components """
        if strong_components[vertex] == -1:
            strong_components[vertex] = root
            for n in graph[vertex]:
                self.assign(n, root, strong_components, graph)

    def reverse_graph(self, graph):
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
    def solve(self):
        # each index holds the index of the ROOT of the strongly-connected component it belongs to
        strong_components = {var: -1 for var in graph.keys()}
        dfs_path = []
        for vertex in graph.keys():
            self.visit(vertex, self.visited, self.graph, dfs_path)
        print(f'The DFS path is {dfs_path}')

        # reverse the graph
        reversed_graph = self.reverse_graph(graph)
        print("The graph has been reversed.")
        for el in dfs_path[::-1]:  # Iterate dfs path from the end
            self.assign(el, el, strong_components, reversed_graph)
        # print(strong_components)
        return strong_components


class TopologicalSort:
    def __init__(self, graph):
        self.graph = graph
        self.global_position = len(graph)
        self.positions = {var: -1 for var in graph.keys()}
        self.visited = set()

    def solve(self):
        for node in self.graph.keys():
            self.dfs(node)
        # sort our nodes
        count_vec = sorted(list(self.positions.items()), key=lambda x: x[1])
        topologically_sorted_nodes = [x[0] for x in count_vec]
        return topologically_sorted_nodes

    def dfs(self, node):
        """
        DFS down the graph and on the backtrack add a position to the node we're at.
        This way we know that each sink node (node with no further children) will be at the highest position as such maintaining topology
        """
        if node in self.visited:
            return
        self.visited.add(node)
        for child in self.graph[node]:
            self.dfs(child)

        self.positions[node] = self.global_position
        self.global_position -= 1


class Variable:
    def __init__(self, name, is_negation: bool):
        self.name = name
        self.boolean_value = None
        self.is_negation = is_negation

    def __str__(self):
        return f'{"!" if self.is_negation else ""}{self.name}'

    def __hash__(self):
        return hash(f'{self.name}{self.is_negation}')

    def __repr__(self):
        return self.__str__()

    def get_opposite_var(self):
        # if !x1 we return x1
        # if x1 we return !x1
        if self.is_negation:
            return self.name
        return f'!{self.name}'

clauses = [['x1', 'x2'], ['x1', '!x2'], ['!x1', 'x2']]


def create_variables(clauses: list) -> dict:
    """
    Creates a Variable object for each variable and one for its negation
    Returns a dictionary of variables in the following format
    {
      'x1': {'normal': Variable, 'negation': Variable }
    }
    """
    variables = {}

    for clause in clauses:
        for var in clause:
            if var.startswith('!'):
                var = var[1:]

            if var not in variables:
                variables[var] = {'normal': Variable(var, False), 'negation': Variable(var, True)}

    return variables


def fetch_variable(variables: dict, varname: str):
    if varname[0] == '!':
        return variables[varname[1:]]['negation']
    else:
        # normal variable
        return variables[varname]['normal']


def create_implication_graph(clauses: list, variables: dict):
    """
    Creates an implication graph of all the clauses
    """
    impl_graph = {}
    for clause in clauses:
        if len(clause) == 1:
            var1: Variable = fetch_variable(variables, varname=clause[0])
            opposite_var1 = fetch_variable(variables, varname=var1.get_opposite_var())

            if var1 not in impl_graph:
                impl_graph[var1] = set()
            if opposite_var1 not in impl_graph:
                impl_graph[opposite_var1] = set()

            # !x1 => x1
            impl_graph[opposite_var1].add(var1)
        elif len(clause) == 2:
            var1, var2 = fetch_variable(variables, varname=clause[0]), fetch_variable(variables, varname=clause[1])
            opposite_var1 = fetch_variable(variables, varname=var1.get_opposite_var())
            opposite_var2 = fetch_variable(variables, varname=var2.get_opposite_var())

            if var1 not in impl_graph:
                impl_graph[var1] = set()
            if opposite_var1 not in impl_graph:
                impl_graph[opposite_var1] = set()
            if var2 not in impl_graph:
                impl_graph[var2] = set()
            if opposite_var2 not in impl_graph:
                impl_graph[opposite_var2] = set()

            # !x1 => x2
            impl_graph[opposite_var1].add(var2)

            # !x2 => x1
            impl_graph[opposite_var2].add(var1)
        else:
            raise Exception('Clause should have length of 1 or 2 !')

    return impl_graph



"""
1. Construct the implication graph G
2. Find all Strongly Connected Components of G
3. For all variables X: if x and !x lie in the same SCC, return unsatisfiable.
4. Find a topological ordering of SCCs
5. For all SCCs C in reverse order:
    1. If literals of C are not assigned yet, set all of them to 1 and their negations to 0
"""
variables = create_variables(clauses)
graph = create_implication_graph(clauses, variables)
print(graph)
node_to_scc = FindStronglyConnectedComponents(graph).solve()

sccs = {}
for node, scc_parent in node_to_scc.items():
    if scc_parent not in sccs:
        sccs[scc_parent] = set()
    sccs[scc_parent].add(node)
    opp_node = fetch_variable(variables, node.get_opposite_var())
    if opp_node in sccs[scc_parent]:
        print('Unsatisfiable!')
        exit()


print(sccs)
rev_topologically_sorted_nodes = list(reversed(TopologicalSort(graph).solve()))
print(rev_topologically_sorted_nodes)
sccs_by_topological_order = [node_to_scc[node] for node in rev_topologically_sorted_nodes]

# Assign values
visited_sccs = set()
for node in sccs_by_topological_order:
    if node in visited_sccs:
        continue
    visited_sccs.add(node)
    for literal in sccs[node]:
        literal.boolean_value = 1
        # set opposite literal's value to 0
        opp_var = fetch_variable(variables, literal.get_opposite_var())
        opp_var.boolean_value = 0
        visited_sccs.add(opp_var)

# Print out the final values
for var_dict in variables.values():
    variable = var_dict['normal']
    opp_var = var_dict['negation']
    print(f'{opp_var.name}: {opp_var.boolean_value}')
    print(f'{variable.name}: {variable.boolean_value}')


