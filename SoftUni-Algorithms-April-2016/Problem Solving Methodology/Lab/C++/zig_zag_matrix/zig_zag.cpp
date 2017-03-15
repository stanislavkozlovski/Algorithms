#include <iostream>
#include <unordered_map>
#include <tuple>
// Code to let me use tuples in unordered_map
namespace std{
    namespace
    {

        // Code from boost
        // Reciprocal of the golden ratio helps spread entropy
        //     and handles duplicates.
        // See Mike Seymour in magic-numbers-in-boosthash-combine:
        //     http://stackoverflow.com/questions/4948780

        template <class T>
        inline void hash_combine(std::size_t& seed, T const& v)
        {
            seed ^= std::hash<T>()(v) + 0x9e3779b9 + (seed<<6) + (seed>>2);
        }

        // Recursive template code derived from Matthieu M.
        template <class Tuple, size_t Index = std::tuple_size<Tuple>::value - 1>
        struct HashValueImpl
        {
          static void apply(size_t& seed, Tuple const& tuple)
          {
            HashValueImpl<Tuple, Index-1>::apply(seed, tuple);
            hash_combine(seed, std::get<Index>(tuple));
          }
        };

        template <class Tuple>
        struct HashValueImpl<Tuple,0>
        {
          static void apply(size_t& seed, Tuple const& tuple)
          {
            hash_combine(seed, std::get<0>(tuple));
          }
        };
    }

    template <typename ... TT>
    struct hash<std::tuple<TT...>> 
    {
        size_t
        operator()(std::tuple<TT...> const& tt) const
        {                                              
            size_t seed = 0;                             
            HashValueImpl<std::tuple<TT...> >::apply(seed, tt);    
            return seed;                                 
        }                                              

    };
}
using namespace std;
class Dir {
    public:
        enum Direction {UP, DOWN};
    
};
int** buildMatrix(int rowCount, int colCount);
int findPrevBestPath(unordered_map<tuple<int, int>, int> bestPaths, int row, int startCol, int colCount, enum Dir::Direction dir);
unordered_map<tuple<int, int>, int> findBestPath(int** matrix, int rows, int cols, enum Dir::Direction dir);


int main() {
    /*
    4
    4
    2 4 5 6
    9 7 1 5
    8 7 7 9
    8 2 6 4
    Partially solved, recovering the path is trivial and so is reading the input with commas
    */
    int rowCount, colCount;
    cin >> rowCount >> colCount;
    int** matrix = buildMatrix(rowCount, colCount);
    unordered_map<tuple<int, int>, int> bestPaths = findBestPath(matrix, rowCount, colCount, Dir::DOWN);

    int max = 0;
    for (size_t i = 0; i < rowCount; i++) {
        if (max < bestPaths[make_tuple(i, colCount-1)]) {
            max = bestPaths[make_tuple(i, colCount-1)];
        }
    }
    cout << max << endl;
    return 0;
}

unordered_map<tuple<int, int>, int> findBestPath(int** matrix, int rows, int cols, enum Dir::Direction startDir) {
    std::unordered_map<tuple<int, int>, int> bestPaths;
    // fill the first row in the unordered map
    for (int i = 0; i < rows; i++) {
        tuple<int, int> coords = make_tuple(i, 0);
        bestPaths[coords] = matrix[i][0];
    }

    /* Goes through every cell in the columns and calculates the best to it by adding the best path of the previous column + the cell's value */
    for (int col = 1; col < cols; col++) {
        for (int row = 0; row < rows; row++) {
            if (startDir == Dir::UP) {
                // Looks for the upper cells in the prev col
                bestPaths[make_tuple(row, col)] = findPrevBestPath(bestPaths, row-1, col-1, rows, startDir) + matrix[row][col];
            } else {
                // Looks for the lower cells in the prev col
                bestPaths[make_tuple(row, col)] = findPrevBestPath(bestPaths, row+1, col-1, rows, startDir) + matrix[row][col];
            }
        }
        // Reverse the direction
        startDir = startDir == Dir::UP ? Dir::DOWN : Dir::UP;
    }
    return bestPaths;
}
int findPrevBestPath(unordered_map<tuple<int, int>, int> bestPaths, int startRow, int col, int maxRow, enum Dir::Direction dir) {
    /* Goes through the best paths in the previous column in regards to the given direction and returns the best sum */
    int best = 0;
    if (dir == Dir::UP) {
        for (int row = 0; row <= startRow && row < maxRow; row++) {
            tuple<int, int> coords = make_tuple(row, col);
            if (bestPaths[coords] > best) {
                best = bestPaths[coords];
            }
        }
    } else {
        for (int row = startRow; row < maxRow; row++) {
            tuple<int, int> coords = make_tuple(row, col);
            if (bestPaths[coords] > best) {
                best = bestPaths[coords];
            }
        }
    }
    return best;
}
int** buildMatrix(int rowCount, int colCount) {
    int** matrix = new int*[rowCount];

    for (size_t i = 0; i < rowCount; i++) {
        int* row = new int[colCount];
        for (size_t j = 0; j < colCount; j++) {
            cin >> row[j];
        }
        matrix[i] = row;
    }

    return matrix;
}