using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class FizzBussTests
    {
        [Test]
        public void GetOutput_WhenDivisableBy3_ShouldReturnFizz()
        {
            var result = FizzBuzz.GetOutput(3);

            Assert.That(result, Is.EqualTo("Fizz"));
        }

        [Test]
        public void GetOutput_WhenDivisableBy5_ShouldReturnBuss()
        {
            var result = FizzBuzz.GetOutput(5);

            Assert.That(result, Is.EqualTo("Buzz"));
        }

        [Test]
        public void GetOutput_WhenDivisableBy3And5_ShouldReturnFizzBuss()
        {
            var result = FizzBuzz.GetOutput(15);

            Assert.That(result, Is.EqualTo("FizzBuzz"));
        }

        [Test]
        public void GetOutput_WhenNotDivisableBy3Or5_ShouldReturnSameNumber()
        {
            var result = FizzBuzz.GetOutput(4);

            Assert.That(result, Is.EqualTo(4.ToString()));
        }

        [Test]
        [TestCase(3, "Fizz")]
        [TestCase(5, "Buzz")]
        [TestCase(15, "FizzBuzz")]
        public void GetOutput_WhenCalled_ReturnsFizzWhenDivisibleBy3_ReturnBussWhenDivisableBy5_ReturnFizzBussWhenDivisableBy3And5(int input, string expectedResult)
        {
            var result = FizzBuzz.GetOutput(input);

            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}
