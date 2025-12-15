using BPCalculator;
using FluentAssertions;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace BPCalculator.Tests
{
    public class BloodPressureValidationTests
    {
        [Theory]
        [InlineData(69, 60)]
        [InlineData(200, 60)]
        [InlineData(100, 39)]
        [InlineData(100, 150)]
        public void Validation_Fails_For_OutOfRangeValues(int systolic, int diastolic)
        {
            var bp = new BloodPressure { Systolic = systolic, Diastolic = diastolic };
            var context = new ValidationContext(bp);
            var results = new System.Collections.Generic.List<ValidationResult>();

            var isValid = Validator.TryValidateObject(bp, context, results, true);

            isValid.Should().BeFalse();
            results.Should().NotBeEmpty();
        }

        [Fact]
        public void Validation_Succeeds_For_InRangeValues()
        {
            var bp = new BloodPressure { Systolic = 120, Diastolic = 80 };
            var context = new ValidationContext(bp);
            var results = new System.Collections.Generic.List<ValidationResult>();

            var isValid = Validator.TryValidateObject(bp, context, results, true);

            isValid.Should().BeTrue();
            results.Should().BeEmpty();
        }
    }
}
