using System;
using System.Collections.Generic;
using System.Text;

namespace CsharpExercise.Event
{
    public class Controller
    {
        public static void SubscribeToPub()
        {
            var p = new Publisher(new Photos.Photo());
            var s = new Subscriber();
            p.OnCompleteEvent = s.OnProcessCompleted;

            p.ProcessPhoto();
        }
    }
}
