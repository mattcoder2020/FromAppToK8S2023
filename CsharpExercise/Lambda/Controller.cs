using CsharpExercise.Photos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CsharpExercise.Lambda
{
    public class Controller
    {
        List<Photo> _photos;
        public Controller()
        {
            _photos = new List<Photo>();
            _photos.Add(new Photo { Transparancy = 1, Effect = 2, Saturation = 3 });
            _photos.Add(new Photo { Transparancy = 1, Effect = 3, Saturation = 6 });
            _photos.Add(new Photo { Transparancy = 1, Effect = 4, Saturation = 9});
         }

        public void Process()
        {
            //Predicate is a type of delegate with signature of bool as return value  type
            var subset = _photos.FindAll(EffectGreaterThan2);
            var subset1 = _photos.FindAll(e => e.Saturation > 5);
                       
            foreach (var p in subset)
            {
                Console.WriteLine(p.Effect);
            }
            foreach (var p in subset1)
            {
                Console.WriteLine(p.Saturation);
            }
        }

        public bool EffectGreaterThan2(Photo p)
        {
            return p.Effect > 2;
        }
    }
}
