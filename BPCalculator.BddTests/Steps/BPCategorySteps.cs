using BPCalculator;
using FluentAssertions;
using TechTalk.SpecFlow;

namespace BPCalculator.BddTests.Steps
{
    [Binding]
    public class BPCategorySteps
    {
        private readonly BloodPressure _bp = new BloodPressure();

        [Given("I enter a systolic value of (.*)")]
        public void GivenIEnterASystolicValueOf(int systolic)
        {
            _bp.Systolic = systolic;
        }

        [Given("I enter a diastolic value of (.*)")]
        public void GivenIEnterADiastolicValueOf(int diastolic)
        {
            _bp.Diastolic = diastolic;
        }

        [When("I compute the blood pressure category")]
        public void WhenIComputeTheBloodPressureCategory()
        {
            // intentionally empty; computation happens in assertion
        }

        [Then("the result should be (.*)")]
        public void ThenTheResultShouldBe(string category)
        {
            _bp.Category.ToString().Should().Be(category);
        }
    }
}
