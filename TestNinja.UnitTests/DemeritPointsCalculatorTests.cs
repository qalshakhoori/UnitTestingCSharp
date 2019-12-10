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
    public class DemeritPointsCalculatorTests
    {
        private DemeritPointsCalculator demeritPointsCalculator;
        [SetUp]
        public void Setup()
        {
            demeritPointsCalculator = new DemeritPointsCalculator();
        }

        [Test]
        [TestCase(0,0)]
        [TestCase(64, 0)]
        [TestCase(65, 0)]
        [TestCase(69, 0)]
        public void CalculateDemeritPoints_BelowOrEqualSpeedLimitOrSpeedNotGreaterThanSpeedLimitBy5_ReturnZero(int speed, int expectedResult)
        {
            var result = demeritPointsCalculator.CalculateDemeritPoints(speed);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void CalculateDemeritPoints_AboveSpeedLimit_ReturnOnePointForEachFiveKM()
        {
            const int SPEEDLIMIT = 65;
            var speed = 100;
            var result = demeritPointsCalculator.CalculateDemeritPoints(speed);

            Assert.That(result, Is.EqualTo((speed-SPEEDLIMIT)/5));
        }

        [Test]
        [TestCase(-10)]
        [TestCase(301)]
        public void CalculateDemeritPoints_BelowZeroOrAbove300_ThroughAnArgumentOutOfRangeException(int speed)
        {
            Assert.That(() => demeritPointsCalculator.CalculateDemeritPoints(speed)
                        , Throws.Exception.TypeOf<ArgumentOutOfRangeException>());
        }
    }
}
