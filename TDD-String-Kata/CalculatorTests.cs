using System;
using System.Linq;
using NUnit.Framework;

namespace TTD_String_Kata
{
	[TestFixture]
	public class CalculatorTests
	{
		private Calculator _calculator;

		[SetUp]
		public void Setup()
		{
			_calculator = new Calculator();
		}

		[Test]
		public void Add_GivenNoValuesToAdd_ShouldReturnZero()
		{
			// Act
			var result = _calculator.Add(string.Empty);

			// Assert
			Assert.That(result, Is.Zero);
		}

		[Test]
		public void Add_GivenSingleValueToAdd_ShouldReturnGivenValue()
		{
			const string input = "1";

			// Act
			var result = _calculator.Add(input);

			// Assert
			Assert.That(result, Is.EqualTo(1));
		}

		[Test]
		public void Add_GivenTwoValuesToAdd_ShouldReturnSumOfBothValues()
		{
			// Arrange
			const string input = "1,2";
			var values = input.Split(',').Select(int.Parse);

			// Act
			var result = _calculator.Add(input);

			// Assert
			Assert.That(result, Is.EqualTo(values.Sum()));
		}

		[Test]
		public void Add_GivenNewlineDelimeter_ShouldReturnSix()
		{
			// Arrange
			const string input = "1\n2,3";

			// Act
			var result = _calculator.Add(input);

			// Assert
			Assert.That(result, Is.EqualTo(6));
		}

		[TestCase("//;\n1;2")]
		[TestCase("//,\n1,2")]
		public void Add_GivenSpecifiedDelimeter_ShouldReturnSum(string input)
		{
			// Act
			var result = _calculator.Add(input);

			// Assert
			Assert.That(result, Is.EqualTo(3));
		}

		[Test]
		public void Add_GivenUnrealNumber_ShouldThrowException()
		{
			// Arrange
			const string input = "//p\n-1p-2p-3";

			// Act, Assert
			var exception = Assert.Throws<Exception>(() => _calculator.Add(input));
			Assert.That(exception.Message, Is.EqualTo("Negatives not allowed: -1,-2,-3"));
		}
	}
}