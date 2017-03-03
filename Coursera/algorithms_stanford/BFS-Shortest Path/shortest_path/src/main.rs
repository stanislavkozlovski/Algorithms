use std::collections::HashMap;
use std::collections::HashSet;
use std::collections::VecDeque;


fn main() {
    let sample_graph: HashMap<&str, Vec<&str>> = 
        [("S", vec!["A", "B"]),
        ("A", vec!["S", "B", "D"]),
        ("B", vec!["S", "A", "C", "D"]),
        ("C", vec!["B", "E"]),
        ("D", vec!["A", "B", "E"]),
        ("E", vec!["D", "C"])].iter().cloned().collect();

    println!("{}", find_shortest_path(sample_graph, "S", "E"));

}

fn find_shortest_path(graph: HashMap<&str, Vec<&str>>, start_node: &str, end_node: &str) -> i32 {
    /* 
        Finds the shortest path from one node to another, given a starting node.
    */
    let mut shortest_paths: HashMap<&str, i32> = HashMap::new();
    let mut visited: HashSet<&str> = HashSet::new();
    shortest_paths.insert(start_node, 0);  // the distance from the start is 0

    let mut nodes_to_visit: VecDeque<&str> = ["S"].iter().cloned().collect();
    // use bfs to traverse every node
    while nodes_to_visit.len() > 0 {
        let current_node = nodes_to_visit.pop_front().unwrap();

        for child in &graph[current_node] {
            if !visited.contains(child) {
                visited.insert(child);
                // the shortest path to the child is the shortest path to the current one + one step
                let shortest_path_to_child = shortest_paths[current_node] + 1;
                shortest_paths.insert(child, shortest_path_to_child);
                nodes_to_visit.push_back(child);  // append the child to the queue
            }
        }
    }

    shortest_paths.entry(end_node).or_insert(-1).clone()
}
