import unittest
from huffman_encoding import HuffmanTree, Node
from huffman_encoding import PriorityQueue


class FunctionalHuffmanTests(unittest.TestCase):
    def setUp(self):
        words = [HuffmanTree(root=Node(value='A', frequency=2)), HuffmanTree(root=Node(value='B', frequency=2)),
                 HuffmanTree(root=Node(value='C', frequency=3)), HuffmanTree(root=Node(value='D', frequency=6)),
                 HuffmanTree(root=Node(value='E', frequency=6)), HuffmanTree(root=Node(value='F', frequency=8)),
                 HuffmanTree(root=Node(value='G', frequency=2)), HuffmanTree(root=Node(value='H', frequency=2)),
                 HuffmanTree(root=Node(value='I', frequency=3)), HuffmanTree(root=Node(value='J', frequency=6)),
                 HuffmanTree(root=Node(value='K', frequency=6)), HuffmanTree(root=Node(value='L', frequency=8)),
                 HuffmanTree(root=Node(value='M', frequency=2)), HuffmanTree(root=Node(value='N', frequency=2)),
                 HuffmanTree(root=Node(value='O', frequency=3)), HuffmanTree(root=Node(value='P', frequency=6)),
                 HuffmanTree(root=Node(value='Q', frequency=6)), HuffmanTree(root=Node(value='R', frequency=8)),
                 HuffmanTree(root=Node(value='S', frequency=2)), HuffmanTree(root=Node(value='T', frequency=2)),
                 HuffmanTree(root=Node(value='U', frequency=3)), HuffmanTree(root=Node(value='V', frequency=6)),
                 HuffmanTree(root=Node(value='W', frequency=6)), HuffmanTree(root=Node(value='X', frequency=8)),
                 HuffmanTree(root=Node(value='Y', frequency=3)), HuffmanTree(root=Node(value='Z', frequency=6))
                 ]

        sorted_words = PriorityQueue(elements=words)

        #  build the tree
        while len(sorted_words) > 1:
            # Take the both trees with the lowest frequency
            first_tree = sorted_words.extract_min()
            second_tree = sorted_words.extract_min()
            # Merge them
            first_tree.merge(second_tree)
            # first_tree.merge(second_tree)
            # Add the merged tree back into the priority queue
            sorted_words.add(first_tree)

        self.huffman_tree = sorted_words.extract_min()

    def test_sentence_encode_decode(self):
        sentence = "YOUCANSMELLTHEPROPANEWHENYOUPASSME"
        self.assertEqual(self.huffman_tree.decode_word(self.huffman_tree.encode_word(sentence)), sentence)

    def test_custom_built_repeating_str(self):
        a_binary_code = self.huffman_tree.encode_word("A")
        self.assertEqual(self.huffman_tree.decode_word(a_binary_code*10), "A"*10)


if __name__ == '__main__':
    unittest.main()
