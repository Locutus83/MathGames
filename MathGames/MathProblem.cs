using System;
using System.Collections.Generic;
using System.Text;

namespace MathGames
{
    class MathProblem
    {
        protected Random _rand;
        protected List<double> _components;
        protected double _answer;

        public List<double> Components { get { return _components; } }
        public double Answer { get { return _answer; } }

        public MathProblem()
        {
            _rand = new Random();
            _components = new List<double>();
        }
    }
}
