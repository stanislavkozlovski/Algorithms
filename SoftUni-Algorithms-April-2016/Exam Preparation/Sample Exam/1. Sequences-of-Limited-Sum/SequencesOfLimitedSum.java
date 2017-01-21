import java.util.Scanner;

public class SequencesOfLimitedSum {
	static StringBuilder allSequences = new StringBuilder();
	static int[] seq;

	public static void main(String[] args) {
		Scanner scan = new Scanner(System.in);
		int maxSum = scan.nextInt();
		seq = new int[maxSum];
        generateSequences(maxSum, 0);
		System.out.println(allSequences);
	}

	static void generateSequences(int maxSum, int index) {
		for (int num = 1; num <= maxSum; num++) {
			seq[index] = num;
			if (maxSum >= 0) {
				// Print seq[0...index]
				for (int i = 0; i <= index; i++) {
					allSequences.append(seq[i]);
					if (i < index) {
						allSequences.append(" ");
					}
				}
				allSequences.append("\n");
			}
			generateSequences(maxSum - num, index + 1);
		}
	}
}
