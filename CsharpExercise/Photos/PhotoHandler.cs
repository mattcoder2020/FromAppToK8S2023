using System;
using System.Collections.Generic;
using System.Text;

namespace CsharpExercise.Photos
{
    internal class PhotoHandler
    {
        internal void AdjustSaturation(Photo p)
        {
            p.Saturation = 100;
        }

        internal void AdjustEffect(Photo p)
        {
            p.Effect = 1000;
        }

        internal void AdjustTransparency (Photo p)
        {
            p.Transparancy = 10000;
        }
    }
}
