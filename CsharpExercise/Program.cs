using CsharpExercise.Lambda;
using CsharpExercise.Photos;
using System;

namespace CsharpExercise
{
    class Program
    {
        static void Main(string[] args)
        {
            SortString(new string[] { "aacd", "bbcd", "aaac", "abc" });
            SortIntegers(new int[] { 4, 3, 1, 2 });
            int[] Fibonics = FindFibonics(10);
            foreach (int i in Fibonics)
            {
                Console.Write(i + " ");
            }
            Console.WriteLine();
            int[] primeNumber = FindPrimeNumber(10);
            foreach (int i in primeNumber)
            {
                Console.Write(i + " ");
            }

            Console.WriteLine(solution("red2 blue5 black4 green1 gold3"));
            Console.WriteLine(solution1("red2 blue5 black4 green1 gold3"));
            Console.WriteLine("Reverse string program ->" + ReverseString("program"));
            Console.WriteLine("Second largest number among [4,2,6,12,1,3] is " + FindSecondLargest(new int[] { 4, 2, 6, 12, 1, 3 }));
            //Delegate
            PhotoLoader l = new PhotoLoader(new Photo());
            PhotoHandler h = new PhotoHandler();

            PhotoLoader.photohandling h1 = h.AdjustEffect;
            h1 += h.AdjustSaturation;
            h1 += h.AdjustTransparency;

            Action<Photo> h2 = h.AdjustEffect;
            h2 += h.AdjustSaturation;
            h2 += h.AdjustTransparency;

            l.furtherhandling(h1);
            l.furtherhandling(h2);


            //Lambda
            Controller c = new Controller();
            c.Process();

            //Event
            Event.Controller.SubscribeToPub();

        }
        //   Input: 10
        //   Output: 0 1 1 2 3 5 8 13 21 34 55
        private static int[] FindFibonics(int v)
        {
            // Fibonic numbers 0, 1, 1, 2, 3, 5, 8, 13
            int first = 0;
            int second = 1;
            int[] fibonics = new int[v + 1];
            int index = 0;
            int sum = 0;
            fibonics[0] = first;
            fibonics[1] = second;
            for (int i = 2; i <= v; i++)
            {
                sum = first + second;
                fibonics[i] = sum;
                first = second;
                second = sum;
            }
            return fibonics;

        }


        //input 4,2,6,12,1,3
        //output 6
        private static int FindSecondLargest(int[] input)
        {
            int max = 0, temp = 0;
            foreach (int i in input)
            {
                if (i > temp)
                {
                    temp = i;
                }
            }
            max = temp;
            temp = 0;
            foreach (int i in input)
            {
                if (i > temp && i != max)
                {
                    temp = i;

                }
            }
            return temp;

        }
        //   Input: str = 10
        //   Output: 2 3 5 7
        private static int[] FindPrimeNumber(int v)
        {
            int[] primenumber = new int[v];
            int matchcount = 0;
            bool isPrime = true;
            for (int i = 2; i <= v; i++)
            {
                isPrime = true;
                for (int j = 2; j <= v; j++)
                {
                    if (i != j && i % j == 0)
                    {
                        isPrime = false;
                        break;
                    }

                }
                if (isPrime)
                {
                    primenumber[matchcount] = i;
                    matchcount++;
                    isPrime = true;
                }

            }
            Array.Resize<int>(ref primenumber, matchcount);
            return primenumber;
        }

        //   Input: str = "red2 blue5 black4 green1 gold3"
        //   Output: "green red gold black blue"
        public static string solution(string s)
        {
            string[] str = s.Split(" ");
            int[] i = new int[str.Length];

            var table = new System.Collections.Hashtable();
            for (int ii = 0; ii < str.Length; ii++)
            {
                string ss = str[ii];
                // ss = ss.Substring(0,ss.Length );
                int index = Int32.Parse(ss.Substring(ss.Length - 1));
                table.Add(index, ss);
            }

            String returnstring = String.Empty;
            for (int ii = 1; ii < str.Length + 1; ii++)
            {
                returnstring += table[ii] + " ";
            }
            return returnstring.Trim();

        }

        //   Input: str = "red2 blue5 black4 green1 gold3"
        //   Output: "green red gold black blue"
        public static string solution1(string s)
        {
            string[] str = s.Split(" ");
            string returnstr = string.Empty;

            for (int i = 1; i <= str.Length; i++)
            {
                foreach (string s1 in str)
                {
                    if (int.Parse(s1.Substring(s1.Length - 1)) == i)
                        returnstr += s1.Substring(0, s1.Length - 1) + " ";
                }
            }
            return returnstr.Substring(0, returnstr.Length - 1);

        }

        public static string ReverseString(string input)
        {
            int i = input.Length;
            string returnstring = string.Empty;
            for (int u = i; u > 0; u--)
            {
                returnstring += input.Substring(u - 1, 1);
            }
            return returnstring;
        }
        public static void SortString(string[] input)
        {
            // Console.WriteLine((int)input[0].ToCharArray()[0]);
            string[] output = new string[input.Length];
            for (int j = 0; j < input.Length; j++)
            {
                for (int i = 0; i < input.Length; i++)
                {
                    if (input[j].CompareTo(input[i]) < 0)
                        swap(input, j, i);

                }
            }
            string str;
            for (int i = 0; i < input.Length; i++)
            {
                str = input[i];
                Console.WriteLine(str);
            }


        }

        public static void sort1(string[] input)
        {
            // Console.WriteLine((int)input[0].ToCharArray()[0]);
            string[] output = new string[input.Length];
            for (int j = 0; j < input.Length; j++)
            {
                int pos = 0;
                int chr = -1;
                for (int i = 0; i < 1000; i++)
                {
                    if (input[j].Length > i
                        && (int)input[j].ToCharArray()[i] > 0
                        && (int)input[j].ToCharArray()[i] > chr
                        && input[j] != null)
                    {
                        pos = j;
                    }
                }
                output[j] = input[pos];
                input[pos] = null;

            }
            string str;
            for (int i = 0; i < output.Length; i++)
            {
                str = output[i];
                Console.WriteLine(str);
            }


        }




        public static int[] SortIntegers(int[] input)
        {
            Console.WriteLine("Before Sorting");
            printArray(input);
            for (int i = 0; i < input.Length; i++)
            {
                for (int w = 0; w < input.Length; w++)
                {
                    if (input[i] > input[w])
                        swap(input, i, w);

                }
                printArray(input);
            }

            Console.WriteLine("After Sorting");
            printArray(input);
            return input;

        }

        private static void printArray(int[] input)
        {
            String str = String.Empty;
            for (int i = 0; i < input.Length; i++)
            {
                str += input[i] + " ";
            }

            Console.WriteLine(str);
        }

        private static void swap<T>(T[] input, int old_e, int new_e)
        {
            T temp;
            temp = input[old_e];
            input[old_e] = input[new_e];
            input[new_e] = temp;

        }



    }
}
