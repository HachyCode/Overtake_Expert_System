using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overtake_Expert_System
{
    class IsClause : Clause
    {
        public IsClause(string variable, string value)
            : base(variable, value)
        {
            Condition = "=";
        }

        protected override IntersectionType Intersect(Clause rhs)
        {
            string v1 = _value;
            string v2 = rhs.Value;

            if (rhs is IsClause)
            {
                if (_value == rhs.Value)
                {
                    return IntersectionType.INCLUDE;
                }
                else
                {
                    return IntersectionType.MUTUALLY_EXCLUDE;
                }
            }
            else
            {
                return IntersectionType.UNKNOWN;
            }
        }
    }
}
