use std::fs::File;
use std::io::Read;
use std::collections::BinaryHeap;


#[derive(Debug)]
struct MinHeap {
    values: Vec<i32>
}

enum CommandTypes {
    ADD,
    REMOVE,
    PRINT_MIN,
}

fn get_command_type(i: i32) -> CommandTypes {
    match i {
        1 => CommandTypes::ADD,
        2 => CommandTypes::REMOVE,
        3 => CommandTypes::PRINT_MIN,
        _ => panic!("Invalid Command Type!"),
    }
}

impl MinHeap {
    fn len(&mut self) -> i32 {
        self.values.len() as i32
    }
    fn add(&mut self, val: i32) {
        self.values.push(val);
        let index: usize = self.values.len()-1;
        self.heapify_up(index);
    }

    fn remove(&mut self, val: i32) {
        // find the index of the value
        let index: usize = self.values.iter().position(|&x| x == val).unwrap();
        self.values.swap_remove(index);  // remove and replace with the last value
        self.heapify_down(index);
    }

    fn heapify_up(&mut self, original_index: usize) {
        if original_index == 0 {
            return;
        }
        let mut index = original_index;
        let mut parent_index: usize = (index - 1) / 2;

        while index != 0 && self.values[parent_index] > self.values[index] {
            
            // switch them up
            let temp: i32 = self.values[index];
            self.values[index] = self.values[parent_index];
            self.values[parent_index] = temp;

            index = parent_index;
            if index == 0 {
                break;
            }
            parent_index = (index - 1) / 2;
        }
    }

    fn heapify_down(&mut self, original_index: usize) {
        let mut index: usize = original_index;
        let max_len = self.values.len();
        let mut first_child_idx: usize = (2 * index) + 1;
        let mut second_child_idx: usize = (2 * index) + 2;
        
        loop {
            if first_child_idx < max_len && second_child_idx < max_len {
                let min_el_idx = self.get_min_element(first_child_idx, second_child_idx);
                if self.values[index] < self.values[min_el_idx] {
                    break;
                }
                self.swap_elements(index, min_el_idx);
                index = min_el_idx;
                first_child_idx = (2 * index) + 1;
                second_child_idx = (2 * index) + 2;
            } else if first_child_idx >= max_len {
                // no children
                break;
            } else {
                // only 1 child
                if self.values[index] < self.values[first_child_idx] {
                    break;
                }
                self.swap_elements(index, first_child_idx);
                break;
            }
        }
    }

    fn swap_elements(&mut self, idx_one: usize, idx_two: usize) {
        let temp: i32 = self.values[idx_one];
        self.values[idx_one] = self.values[idx_two];
        self.values[idx_two] = temp;
    }

    fn get_min_element(&self, idx_one: usize, idx_two: usize) -> usize {
        let first_is_smaller: bool = self.values[idx_one] < self.values[idx_two];
        match first_is_smaller {
            true => idx_one,
            false => idx_two,
        }
    }

        fn pop(&mut self) -> i32 {
        let min_element: i32 = self.values[0];

        self.values.swap_remove(0);  // remove and replace with the last value
        self.heapify_down(0);

        min_element
    }

    fn peek(&mut self) -> i32 {
        self.values[0]
    }
}


fn main() {
    let mut data = String::new();
    let mut f = File::open("input.txt").expect("Unable to open file");
    f.read_to_string(&mut data).expect("Unable to read string");
    let mut numbers: Vec<i32> = data.split("\n").map(|x| x.parse().unwrap()).collect();
    let mut medians: Vec<i32> = Vec::new();
    let mut numbersCount: i32 = 0;
    let mut max_heap: BinaryHeap<i32> = BinaryHeap::new();
    let mut min_heap = MinHeap{values: Vec::new()};

    for number in numbers {
        numbersCount += 1;
        let mut k: i32 = -1;
        if (numbersCount % 2 == 0) {
            k = numbersCount / 2;
        } else {
            k = (numbersCount + 1) / 2;
        }

        if (max_heap.len() == 0 && min_heap.len() == 0) {
            medians.push(number);
            max_heap.push(number);
        } else if (min_heap.len() == 0) {
            let min_num: i32 = max_heap.peek().unwrap().clone();
            if (number > min_num) {
                medians.push(min_num);
                min_heap.add(number);
            } else {
                // Need to swap both
                medians.push(number);
                min_heap.add(max_heap.pop().unwrap());  // old num is smaller so we want it to be in the max heap
                max_heap.push(number);  // number is bigger so we want IT to be in the min heap
            }
        } else if (max_heap.len() == 0) {
            let max_num: i32 = min_heap.peek();
            if (number < max_num) {
                max_heap.push(number);
                medians.push(number);
            } else {
                // Need to swap both
                medians.push(number);
                max_heap.push(min_heap.pop());
                min_heap.add(number);
            }
            
        } else {
            // Base case
            let lower_count: i32 = max_heap.len() as i32;
            let upper_count: i32 = min_heap.len() as i32;
            let min_upper: i32 = min_heap.peek();
            let max_lower: i32 = max_heap.peek().unwrap().clone();
            if (lower_count == upper_count) {
                if (min_upper < number) {
                    medians.push(min_upper);
                    max_heap.push(min_heap.pop());
                    min_heap.add(number);
                } else {
                    max_heap.push(number);
                    medians.push(max_heap.peek().unwrap().clone());
                }
                continue;
            } else if (lower_count > upper_count) {
                // let max_lower: i32 = max_heap.pop().unwrap().clone();

                if (max_lower < number) {
                    min_heap.add(number);  // both are now equal length
                    // TODO: Add median
                } else {
                    // Number goes to max_heap, old goes to min_heap
                    min_heap.add(max_heap.pop().unwrap().clone());
                    max_heap.push(number);
                    // TODO: Add median
                }
            } else if (upper_count > lower_count) {
                if (min_upper > number) {
                    max_heap.push(number); // both are now equal length
                    // TODO: Add median
                } else {
                    // number goes to min_heap, old goes to max
                    max_heap.push(min_heap.pop());
                    min_heap.add(number);
                    // TODO: Add median
                }
            }

            if (lower_count == k) {
                medians.push(max_heap.peek().unwrap().clone());
            } else {
                panic!("{:?} is lower count VS K: {:?}", lower_count, k);
            }
        }
    }
    println!("{:?}", medians.iter().fold(0, |a, b| a + b) % 10000);
    println!("{:?}", medians.len());
    
}   
    
