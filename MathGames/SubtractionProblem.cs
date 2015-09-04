using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace MathGames
{
    sealed class SubtractionProblem : MathProblem
    {
        public SubtractionProblem(int difficulty)
        {
            /* 
             * TODO: Better Define difficulty levels.
             * 
             * Maybe, we should have a switch case like this:
             * 1 and 2 are below.
             * 
             * 3 adds borrowing (still 2 digits)
             * 
             * 4 adds a 3rd digit (with borrowing)
             * 
             * 5 adds negative number (still up to 3 digits)
             */
            double first, second;
            do
            {
                first = 0;
                second = 0;

                double digits;
                if (difficulty == 5)
                {
                    digits = 2;
                }
                else if (difficulty > 3)
                {
                    digits = 3;
                }
                else
                {
                    digits = difficulty;
                }

                for (double i = 0; i < digits; i++)
                {
                    double a = _rand.Next(9);
                    double b = _rand.Next(9);

                    if (difficulty >= 3)
                    {
                        first += a * Math.Pow(10, i);
                        second += b * Math.Pow(10, i);
                    }
                    else
                    {
                        if (a < b)
                        {
                            second += a*Math.Pow(10, i);
                            first += b*Math.Pow(10, i);
                        }
                        else
                        {
                            first += a*Math.Pow(10, i);
                            second += b*Math.Pow(10, i);
                        }
                    }

                    if (difficulty < 5)
                    {
                        if (first < second)
                        {
                            double tmp = first;
                            first = second;
                            second = tmp;
                        }
                    }
                }

                _answer = first - second;

            } while (_answer == 0 || second == 0); 

            _components.Add(first);
            _components.Add(second);
        }

        public override string ToString()
        {
            return "   " + _components[0] + Environment.NewLine + 
                "-  " + _components[1] + Environment.NewLine + 
                "____________";
        }
    }
}
