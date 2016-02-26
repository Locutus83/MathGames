using System;
using System.Collections.Generic;
using System.Text;

namespace MathGames
{
    class DivisionProblem : MathProblem
    {
        public DivisionProblem(int difficulty)
        {
            /* 
             * TODO: Convert to Division.
             */

            // Only work on 2 numbers per day.  This will produce one of two sequential numbers between 0 and 12.
            _components.Add((DateTime.Now.DayOfYear + _rand.Next(2*difficulty))%13 + 1);

            _answer = _rand.Next(12);

            _components.Add(_components[0] * _answer);
        }

        public override string ToString()
        {
            return "   " + _components[1] + Environment.NewLine + 
                "÷  " + _components[0] + Environment.NewLine + 
                "____________";
        }
    }
}
