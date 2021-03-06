using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overtake_Expert_System
{

    class Clause
    {
        protected string _variable;
        protected string _value;
        public string Condition { get; protected set; } = "=";
        public Clause(string variable, string value)
        {
            _variable = variable;
            _value = value;
        }
        public Clause(string variable, string condition, string value)
        {
            _variable = variable;
            _value = value;
            Condition = condition;
        }
        public IntersectionType MatchClause(Clause rhs)
        {
            if (_variable != rhs.Variable)
            {
                return IntersectionType.UNKNOWN;
            }

            return Intersect(rhs);
        }
        protected virtual IntersectionType Intersect(Clause rhs)
        {
            throw new NotImplementedException();
        }
        public String Variable
        {
            get { return _variable; }
        }
        public string Value
        {
            get { return _value; }
        }
        public override string ToString()
        {
            return _variable + " " + Condition + " " + _value;
        }
    }
}
