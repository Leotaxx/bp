using BPCalculator;
using FluentAssertions;
using Xunit;

namespace BPCalculator.Tests
{
    public class BloodPressureTests
    {
        [Theory]
        [InlineData(150, 95, BPCategory.High)]
        [InlineData(140, 75, BPCategory.High)]       // systolic boundary
        [InlineData(125, 90, BPCategory.High)]       // diastolic boundary
        [InlineData(130, 85, BPCategory.PreHigh)]
        [InlineData(118, 80, BPCategory.PreHigh)]    // diastolic pre-high boundary
        [InlineData(85, 55, BPCategory.Low)]
        [InlineData(100, 59, BPCategory.Low)]        // diastolic low boundary
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

        [Theory]
        [InlineData(80, 40, 40)]
        [InlineData(120, 80, 40)]
        [InlineData(150, 90, 60)]
        public void PulsePressure_ComputesDifference(int systolic, int diastolic, int expected)
        {
            var bp = new BloodPressure { Systolic = systolic, Diastolic = diastolic };

            bp.PulsePressure.Should().Be(expected);
        }
    }
}
