using System;
using System.Linq;

namespace WslGUI
{
    public static class ByteSizeCalculator
    {
        private const int FACTOR = 1024;

        public static double Compute(long value, SizeScale scale)
        {
            return value / Math.Pow(FACTOR, (int)scale);
        }

        public static double DetectScaleAndCompute(long value, out SizeScale scale)
        {
            var scales = 
                Enum
                    .GetValues(typeof(SizeScale))
                    .Cast<SizeScale>()
                    .OrderBy(_ => (int)_);

            foreach (SizeScale itrScale in scales)
            {
                double result = Compute(value, itrScale);

                if (result < FACTOR)
                {
                    scale = itrScale;
                    return result;
                }
            }

            throw new NotImplementedException("Missing scale");
        }
    }
}
