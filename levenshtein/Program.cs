using System;

//https://en.wikipedia.org/wiki/Levenshtein_distance

namespace levenshtein {
	class Levenshtein {
		/* Recursive

			This is a straightforward, but inefficient,
			recursive C# implementation of a LevenshteinDistance function
			that takes two strings, s and t and returns the Levenshtein distance between them:
		*/
		public static int LevenshteinDistance(string s, string t) {
			int cost;
			//base case :empty strings
			if (s.Length == 0) { return t.Length; }
			if (t.Length == 0) { return s.Length; }
			// test if last characters of the strings match
			if (s[ ^ 1] == t[ ^ 1]) {
				cost = 0;
			} else {
				cost = 1;
			}
			// return minimum of delete char from s, delete char from t, and delete char from both
			return Math.Min(
				Math.Min(
					LevenshteinDistance(s[0.. ^ 1], t) + 1,
					LevenshteinDistance(s, t[0.. ^ 1]) + 1
				), LevenshteinDistance(s[0.. ^ 1], t[0.. ^ 1]) + cost

			);
		}
		//Iterative with full matrix
		// use levenshtein distance algorithm to calculate
		// the distance between between two strings (lower is closer)
		public static int LevenshteinDistance2(string s, string t) {
			//from https://github.com/vlang/v/blob/59378dce46c6d7c5dc712d5119f52559729239f1/vlib/strings/similarity.v
			//Copyright Alexander Medvednikov (https://github.com/medvednikov) && joe-conigliaro (https://github.com/joe-conigliaro)
			int[] f = new int[t.Length + 1];
			for (int i = 0; i < s.Length; i++) {
				int jj = 1;
				int fjj1 = f[0];
				f[0]++;
				for (int j = 0; j < t.Length; j++) {
					//if (i > s.Length - 1 || j > t.Length - 1) { continue; }
					int mn;
					if (f[jj] + 1 <= f[jj - 1] + 1) {
						mn = f[jj] + 1;
					} else {
						mn = f[jj - 1] + 1;
					}
					if (s[i] != t[j]) {
						if (mn >= fjj1 + 1) { mn = fjj1 + 1; }
					} else {
						if (mn >= fjj1) { mn = fjj1; }
					}
					fjj1 = f[jj];
					f[jj] = mn;
					jj++;
				}
			}
			Console.WriteLine($"{string.Join(", ", f)}");
			return f[f.Length - 1];
		}
	}
	class Program {
		static void Main() {
			Console.WriteLine(Levenshtein.LevenshteinDistance("kitten", "sitting"));
			Console.WriteLine(Levenshtein.LevenshteinDistance2("kitten", "sitting"));
		}
	}
}