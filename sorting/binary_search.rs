use std::io;

fn binary_search(array: &Vec<i32>, value: i32, mut start_index: usize, mut end_index: usize) -> Option<usize> {
    if start_index == end_index || end_index < 0 || start_index >= array.len() {
        return None;
    }
    let midIndex = start_index + ((end_index - start_index) / 2);
    
    let foundValue = array[midIndex];
    if foundValue == value {
        return Some(midIndex);
    } else if foundValue > value {
        // go left
        end_index = midIndex;
        return binary_search(array, value, start_index, end_index)
    } else {
        // go right
        start_index = midIndex + 1;
        return binary_search(array, value, start_index, end_index)
    }
}

fn main() {

}

#[cfg(test)]
mod tests {
    use super::binary_search;
    
    #[test]
    fn test_all_numbers() {
        let mut numbers = vec![1,2,3,4,5,6,7,8,9,10];
        for num in -10..11 {
            let el_position = numbers.iter().position(|x| x.clone()==num);
            match el_position {
                Some(x) => assert!(x == binary_search(&numbers, num, 0, numbers.len()).unwrap() ),
                None => assert!(None == binary_search(&numbers, num, 0, numbers.len())),
            }
        }
    }
}

