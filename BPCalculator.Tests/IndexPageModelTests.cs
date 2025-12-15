using BPCalculator.Pages;
using BPCalculator.Telemetry;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace BPCalculator.Tests
{
    public class IndexPageModelTests
    {
        private class TelemetryStub : IBPTelemetry
        {
            public int Calls { get; private set; }
            public BloodPressure? Last { get; private set; }

            public void TrackSubmission(BloodPressure bp)
            {
                Calls++;
                Last = bp;
            }
        }

        [Fact]
        public void OnGet_SetsDefaultBloodPressure()
        {
            var telemetry = new TelemetryStub();
            var pageModel = new BloodPressureModel(telemetry);

            pageModel.OnGet();

            pageModel.BP.Should().NotBeNull();
            pageModel.BP.Systolic.Should().Be(100);
            pageModel.BP.Diastolic.Should().Be(60);
        }

        [Fact]
        public void OnPost_InvalidWhenSystolicNotGreaterThanDiastolic()
        {
            var telemetry = new TelemetryStub();
            var pageModel = new BloodPressureModel(telemetry)
            {
                BP = new BloodPressure { Systolic = 80, Diastolic = 85 }
            };

            var result = pageModel.OnPost();

            result.Should().BeOfType<PageResult>();
            pageModel.ModelState.IsValid.Should().BeFalse();
            pageModel.ModelState.ErrorCount.Should().Be(1);
            telemetry.Calls.Should().Be(0);
        }

        [Fact]
        public void OnPost_ValidTriggersTelemetry()
        {
            var telemetry = new TelemetryStub();
            var pageModel = new BloodPressureModel(telemetry)
            {
                BP = new BloodPressure { Systolic = 125, Diastolic = 80 }
            };

            var result = pageModel.OnPost();

            result.Should().BeOfType<PageResult>();
            pageModel.ModelState.IsValid.Should().BeTrue();
            telemetry.Calls.Should().Be(1);
            telemetry.Last.Should().NotBeNull();
            telemetry.Last!.Category.Should().Be(BPCategory.PreHigh);
        }
    }
}
