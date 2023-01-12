using System;
using System.Collections.Generic;
using System.Text;

namespace CsharpExercise.Event
{
    public class Subscriber
    {
        public void OnProcessCompleted (object obj, CustomizedEventArgs args)
        {
            Console.WriteLine("Subscriber receive photo with Saturation value as " + args.P.Saturation);
        }
    }
}
