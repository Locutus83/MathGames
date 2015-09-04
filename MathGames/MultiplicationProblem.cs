using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace MathGames
{
    sealed class MultiplicationProblem : MathProblem
    {
        public MultiplicationProblem(int difficulty)
        {
            /* 
             * TODO: Better Define difficulty levels.
             */

            // Only work on 2 numbers per day.  This will produce one of two random numbers between 0 and 12.
            double first = (DateTime.Now.DayOfYear + _rand.Next(2))%13;

            double second = _rand.Next(12);

            _answer = first*second;

            // Randomize the order of the multipliers.
            if (_rand.Next(2) == 1)
            {
                _components.Add(first);
                _components.Add(second);
            }
            else
            {
                _components.Add(second);
                _components.Add(first);
            }
        }

        public override string ToString()
        {
            return "   " + _components[0] + Environment.NewLine + 
                "x  " + _components[1] + Environment.NewLine + 
                "____________";
        }
    }
}
