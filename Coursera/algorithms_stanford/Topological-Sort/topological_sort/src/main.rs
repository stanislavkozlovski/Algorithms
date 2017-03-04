use std::collections::HashMap;
use std::collections::HashSet;


struct PositionWrapper {
    // Wrapper so that we can pass it by reference and change it at our will
    position: i32,
}
fn main() {
    let graph: HashMap<i32, Vec<i32>> = [
        (1, vec![2, 3]),
        (2, vec![4]),
        (3, vec![5]),
        (4, vec![6, 7, 5]),
        (5, vec![8]),
        (6, vec![]),
        (7, vec![8]),
        (8, vec![])
    ].iter().cloned().collect();
    // Keep a count of the global position of each node
    let mut global_position: PositionWrapper = PositionWrapper{position: graph.len() as i32};  // start at the last position
    let mut positions: HashMap<i32, i32> = HashMap::new();
    let mut visited: HashSet<i32> = HashSet::new();

    for node in graph.keys() {
        dfs(&graph, &mut visited, node.clone(), &mut positions, &mut global_position);
    }
    
    // Sort our nodes
    let mut count_vec: Vec<(&i32, &i32)> = positions.iter().collect();
    count_vec.sort_by(|a, b| b.1.cmp(&a.1));
    let topologically_sorted_nodes: Vec<i32> = count_vec.iter().map(|a| a.0.clone()).collect();

    println!("{:?}", topologically_sorted_nodes);
    
}

fn dfs(graph: &HashMap<i32, Vec<i32>>, visited: &mut HashSet<i32>, node: i32, positions: &mut HashMap<i32, i32>, current_position: &mut PositionWrapper) -> () {
    /*
     DFS down the graph and on the backtrack add a position to the node we're at.
     This way we know that each sink node(node with no further children) will be at the highest position and as such maintaing topology
    */
    if visited.contains(&node) {
        return;
    }
    visited.insert(node);

    for child in &graph[&node] {
        dfs(graph, visited, child.clone(), positions, current_position);
    }

    positions.insert(node, current_position.position);
    current_position.position -= 1;
    ()
}
