/* The Kosaraju-Sharir algorithm for finding the strongly-connected components in a graph. */
use std::collections::HashMap;
use std::collections::HashSet;

fn dfs(graph: &HashMap<i32, Vec<i32>>, 
       start_node: i32, 
       visited: &mut HashSet<i32>, 
       dfs_path: &mut Vec<i32>) -> () {
    if visited.contains(&start_node) {
        return;
    }
    visited.insert(start_node);

    for child in &graph[&start_node] {
        
        if !visited.contains(child) {
            dfs(graph, child.clone(), visited, dfs_path);
        }
    }
    
    dfs_path.push(start_node);
    ()
}

fn reverse_graph(graph: &HashMap<i32, Vec<i32>>) -> HashMap<i32, Vec<i32>> {
    let mut reversed_graph: HashMap<i32, Vec<i32>> = HashMap::new();
    
    for (node, children) in graph.iter() {
        for child in children {
            reversed_graph.entry(child.clone()).or_insert(vec![]).push(node.clone());
        }
        // Create entries for all the nodes so we don't get key errors
        if !reversed_graph.contains_key(node) {
            reversed_graph.insert(node.clone(), vec![]);
        }
    }

    reversed_graph
}

fn build_components(node: i32, graph: &HashMap<i32, Vec<i32>>, assigned_nodes: &mut HashSet<i32>, current_component: &mut HashSet<i32>) -> () {
    // DFS Algorithm for  traversing the reversed graph
    if assigned_nodes.contains(&node) {
        return
    }

    current_component.insert(node);
    assigned_nodes.insert(node);
    for child in &graph[&node] {
        if !assigned_nodes.contains(child) {
            build_components(child.clone(), graph, assigned_nodes, current_component)
        }
    }
    ()
}
fn main() {
    let graph: HashMap<i32, Vec<i32>> = [
        (0, vec![1, 11, 13]),
        (1, vec![6]),
        (2, vec![0]),
        (3, vec![4]),
        (4, vec![3, 6]),
        (5, vec![13]),
        (6, vec![0, 11]),
        (7, vec![12]),
        (8, vec![6, 11]),
        (9, vec![0]),
        (10, vec![4, 6, 10]),
        (11, vec![]),
        (12, vec![7]),
        (13, vec![2, 9])
    ].iter().cloned().collect();
    let mut dfs_path: Vec<i32> = Vec::new();
    let mut visited: HashSet<i32> = HashSet::new();
    // 1. Fill the DFS Path
    for node in graph.keys() {
        dfs(&graph, *node, &mut visited, &mut dfs_path)
    }
    
    
    // 2. Reverse the graph
    let reversed_graph: HashMap<i32, Vec<i32>> = reverse_graph(&graph);
    
    // 3. Iterate over the reversed graph and build the components
    let mut strongly_connected_components: Vec<HashSet<i32>> = vec![];
    let mut assigned_nodes: HashSet<i32> = HashSet::new(); // holds the nodes that have been assigned a component

    for node in dfs_path.iter().rev() {
        let mut new_component: HashSet<i32> = HashSet::new();

        build_components(node.clone(), &reversed_graph, &mut assigned_nodes, &mut new_component);

        if new_component.len() > 0 {
            strongly_connected_components.push(new_component);
        }
    }

    println!("{:?}", strongly_connected_components);
}
