using System;
using System.Collections.Generic;
using System.IO;

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
			//Console.WriteLine($"{string.Join(", ", f)}");
			return f[ ^ 1];
		}
		// use levenshtein distance algorithm to calculate
		// how similar two strings are as a percentage (higher is closer)
		public static double Levenshtein_distance_percentage(string s, string t) {
			//from https://github.com/vlang/v/blob/59378dce46c6d7c5dc712d5119f52559729239f1/vlib/strings/similarity.v
			//Copyright Alexander Medvednikov (https://github.com/medvednikov) && joe-conigliaro (https://github.com/joe-conigliaro)
			int distance = LevenshteinDistance2(s, t);
			int len;
			if (s.Length >= t.Length) {
				len = s.Length;
			} else {
				len = t.Length;
			}
			return (1.00 - (double)distance / (double)len) * 100.00;
		}
	}

	class DiceCoefficient {
		// implementation of Sørensen–Dice coefficient.
		// find the similarity between two strings.
		// returns coefficient between 0.0 (not similar) and 1.0 (exact match).
		static public double Dice_Coefficient(string s, string t) {
			//from https://github.com/vlang/v/blob/59378dce46c6d7c5dc712d5119f52559729239f1/vlib/strings/similarity.v
			//Copyright Alexander Medvednikov (https://github.com/medvednikov) && joe-conigliaro (https://github.com/joe-conigliaro)
			if (s.Length == 0 || t.Length == 0) { return 0.00; }
			if (s == t) { return 1.00; }
			if (t.Length < 2 || s.Length < 2) { return 0.00; }
			string a, b;
			if (s.Length > t.Length) { a = s; } else { a = t; }
			if (a == s) { b = t; } else { b = s; }
			Dictionary<string, int> first_bigrams = new Dictionary<string, int>();
			for (int i = 0; i < a.Length - 1; i++) {
				string bigram = a[i..(i + 2)];
				int q;
				if (first_bigrams.ContainsKey(bigram)) {
					q = first_bigrams[bigram] + 1;
				} else { q = 1; }
				first_bigrams[bigram] = q;
			}
			int intersection_size = 0;
			for (int i = 0; i < b.Length - 1; i++) {
				string bigram = b[i..(i + 2)];
				int count;
				if (first_bigrams.ContainsKey(bigram)) { count = first_bigrams[bigram] + 1; } else { count = 0; }
				if (count > 0) {
					first_bigrams[bigram] = count - 1;
					intersection_size++;
				}
			}
			return (2.00 * (double)intersection_size) / ((double)a.Length + (double)b.Length - 2);
			}
		}
		class Program {
			static void Main() {
				Console.WriteLine(Levenshtein.LevenshteinDistance("kitten", "sitting"));
				Console.WriteLine(Levenshtein.LevenshteinDistance2("kitten", "sitting"));
				Console.WriteLine(Levenshtein.Levenshtein_distance_percentage("kitten", "sitting"));

				Console.WriteLine(DiceCoefficient.Dice_Coefficient("kitten", "sitting"));
			}
		}
	}