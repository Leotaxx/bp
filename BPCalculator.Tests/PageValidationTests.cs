using BPCalculator.Pages;
using BPCalculator.Telemetry;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Xunit;

namespace BPCalculator.Tests
{
    public class PageValidationTests
    {
        private class TelemetryStub : IBPTelemetry
        {
            public int Calls { get; private set; }
            public void TrackSubmission(BloodPressure bp) => Calls++;
        }

        [Fact]
        public void OnPost_Fails_When_PulsePressure_Negative()
        {
            var telemetry = new TelemetryStub();
            var pageModel = new BloodPressureModel(telemetry)
            {
                BP = new BloodPressure { Systolic = 80, Diastolic = 120 }
            };

            var result = pageModel.OnPost();

            result.Should().BeOfType<PageResult>();
            pageModel.ModelState.IsValid.Should().BeFalse();
            pageModel.ModelState.ErrorCount.Should().BeGreaterThan(0);
            telemetry.Calls.Should().Be(0);
        }
    }
}
