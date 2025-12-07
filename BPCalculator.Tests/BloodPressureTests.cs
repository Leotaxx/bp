using BPCalculator;
using FluentAssertions;
using Xunit;

namespace BPCalculator.Tests
{
    public class BloodPressureTests
    {
        [Theory]
        [InlineData(150, 95, BPCategory.High)]
        [InlineData(130, 85, BPCategory.PreHigh)]
        [InlineData(85, 55, BPCategory.Low)]
        [InlineData(115, 70, BPCategory.Ideal)]
        public void Category_ComputesExpectedValues(int systolic, int diastolic, BPCategory expected)
        {
            var bp = new BloodPressure { Systolic = systolic, Diastolic = diastolic };

            bp.Category.Should().Be(expected);
        }

        [Theory]
        [InlineData(120, 95, PulsePressureCategory.Narrow)]
        [InlineData(120, 60, PulsePressureCategory.Normal)]
        [InlineData(150, 80, PulsePressureCategory.Wide)]
        public void PulsePressure_ComputesCategory(int systolic, int diastolic, PulsePressureCategory expected)
        {
            var bp = new BloodPressure { Systolic = systolic, Diastolic = diastolic };

            bp.PulseCategory.Should().Be(expected);
        }
    }
}
