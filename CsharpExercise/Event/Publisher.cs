using CsharpExercise.Photos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CsharpExercise.Event
{
    public class Publisher
    {
        private readonly Photo photo;
        public EventHandler<CustomizedEventArgs> OnCompleteEvent;

        public Publisher(Photo photo)
        {
            this.photo = photo;
        }

        public void ProcessPhoto()
        {
            this.photo.Saturation = 1000000;
            System.Threading.Thread.Sleep(3000);
            RaiseEvent(this.photo);
        }

        protected virtual void RaiseEvent(Photo p)
        {
            if (OnCompleteEvent != null)
                OnCompleteEvent(this, new CustomizedEventArgs(p));
        }
    }
}
