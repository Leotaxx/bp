using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BPCalculator.Telemetry;

// page model

namespace BPCalculator.Pages
{
    public class BloodPressureModel : PageModel
    {
        [BindProperty]                              // bound on POST
        public BloodPressure BP { get; set; }
        private readonly IBPTelemetry _telemetry;

        public BloodPressureModel(IBPTelemetry telemetry)
        {
            _telemetry = telemetry;
        }

        // setup initial data
        public void OnGet()
        {
            BP = new BloodPressure() { Systolic = 100, Diastolic = 60 };
        }

        // POST, validate
        public IActionResult OnPost()
        {
            // extra validation
            if (!(BP.Systolic > BP.Diastolic))
            {
                ModelState.AddModelError("", "Systolic must be greater than Diastolic");
            }
            if (ModelState.IsValid)
            {
                _telemetry.TrackSubmission(BP);
            }
            return Page();
        }
    }
}
