using System;
using System.Collections.Generic;
using System.Text;

namespace CsharpExercise.Photos
{
    public class PhotoLoader
    {
        public delegate void photohandling(Photo p)  ;
        Photo _photo;
        public PhotoLoader(Photo p)
        {
            Console.WriteLine ("Start loading photos");
            _photo = p;
        } 

        public void furtherhandling(photohandling h)
        {
            h(_photo);
            Console.WriteLine("Effect: + " + _photo.Effect + " Saturation: " + _photo.Saturation + " Transparency: " + _photo.Transparancy);
        }

        public void furtherhandling(Action<Photo> h)
        {
            h(_photo);
            Console.WriteLine("Convert to Action<Photo> Effect: + " + _photo.Effect + " Saturation: " + _photo.Saturation + " Transparency: " + _photo.Transparancy);
        }
    }
}
