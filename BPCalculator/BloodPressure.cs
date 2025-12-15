using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace BPCalculator
{
    // BP categories
    public enum BPCategory
    {
        [Display(Name = "Low Blood Pressure")] Low,
        [Display(Name = "Ideal Blood Pressure")] Ideal,
        [Display(Name = "Pre-High Blood Pressure")] PreHigh,
        [Display(Name = "High Blood Pressure")] High
    };

    public enum PulsePressureCategory
    {
        [Display(Name = "Narrow Pulse Pressure")] Narrow,
        [Display(Name = "Normal Pulse Pressure")] Normal,
        [Display(Name = "Wide Pulse Pressure")] Wide
    };

    public class BloodPressure
    {
        public const int SystolicMin = 70;
        public const int SystolicMax = 190;
        public const int DiastolicMin = 40;
        public const int DiastolicMax = 100;

        public const int LowPulseThreshold = 30;
        public const int HighPulseThreshold = 60;

        [Range(SystolicMin, SystolicMax, ErrorMessage = "Invalid Systolic Value")]
        public int Systolic { get; set; }                       // mmHG

        [Range(DiastolicMin, DiastolicMax, ErrorMessage = "Invalid Diastolic Value")]
        public int Diastolic { get; set; }                      // mmHG

        // difference between systolic and diastolic
        public int PulsePressure => Systolic - Diastolic;

        // calculate BP category
        public BPCategory Category
        {
            get
            {
                if (Systolic >= 140 || Diastolic >= 90)
                {
                    return BPCategory.High;
                }

                if (Systolic >= 120 || Diastolic >= 80)
                {
                    return BPCategory.PreHigh;
                }

                if (Systolic < 90 || Diastolic < 60)
                {
                    return BPCategory.Low;
                }

                return BPCategory.Ideal;
            }
        }

        public PulsePressureCategory PulseCategory
        {
            //add a comment line trigger new feature push 
            get
            {
                if (PulsePressure < LowPulseThreshold)
                {
                    return PulsePressureCategory.Narrow;
                }

                if (PulsePressure > HighPulseThreshold)
                {
                    return PulsePressureCategory.Wide;
                }

                return PulsePressureCategory.Normal;
            }
        }
    }
}
