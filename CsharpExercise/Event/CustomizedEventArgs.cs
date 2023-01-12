using CsharpExercise.Photos;

namespace CsharpExercise.Event
{
    public class CustomizedEventArgs
    {
        public CustomizedEventArgs(Photo p)
        {
            P = p;
        }

        public Photo P { get; }
    }
}