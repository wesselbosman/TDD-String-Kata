using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace TTD_String_Kata
{
	public class Calculator
	{
		private const string DelimeterFlag = "//";
		private readonly List<char> _delimeter = new List<char> { ',', '\n' };

		public int Add(string input)
		{
			if (string.IsNullOrWhiteSpace(input))
				return 0;

			if (input.StartsWith(DelimeterFlag))
			{
				input = UseCustomDelimeterFormat(input, DelimeterFlag, out var customDelimeter);
				_delimeter.Add(customDelimeter);
			}
				
			var values = input.Split(_delimeter.ToArray())
				.Select(int.Parse)
				.ToArray();

			var negativeValues = values.Where(x => x < 0)
				.ToList();

			if (!negativeValues.Any())
				return values.Sum();

			var errorMessage = $"Negatives not allowed: {string.Join(",", negativeValues.ConvertAll(i => i.ToString()))}";
			throw new Exception(errorMessage);
		}

		public static string UseCustomDelimeterFormat(string input, string delimeterFlag, out char customDelimeter)
		{
			var decoratorStrings = new List<string> { delimeterFlag, "\n" };

			customDelimeter = input
				.ToCharArray()
				.ElementAt(delimeterFlag.Length);

			decoratorStrings.ForEach(s => input = input.Replace(s, string.Empty));

			var regex = new Regex(Regex.Escape(customDelimeter.ToString()));
			input = regex.Replace(input, string.Empty, 1);

			return input;
		}
	}
}