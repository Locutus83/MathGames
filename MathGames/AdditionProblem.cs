using System;
using System.Collections.Generic;
using System.Text;

namespace MathGames
{
    sealed class AdditionProblem : MathProblem
    {
        public AdditionProblem(int difficulty)
        {
            /* 
             * TODO: Better Define difficulty levels.
             * 
             * Find a point to add in multiple lines.  
             * Maybe we should Make level 2 and higher have an additional Addend for each level?
             */
            do
            {
                _components.Add(_rand.Next(75*difficulty) + (_rand.Next(25*difficulty)));
            } while (_components.Count < difficulty || _components.Count < 2);

            _answer = 0;
            foreach (double component in _components)
            {
                _answer += component;
            }

            _components.Sort();
        }

        public override string ToString()
        {
            string.Format("{0,5:###.0}", _components[0]);
            string problemString = _components[0] + Environment.NewLine;

            for (int i = 1; i < _components.Count; i++)
            {
                problemString += "+    " + _components[i] + Environment.NewLine;
            }

            problemString += "____________";

            return problemString;
        }
    }
}
