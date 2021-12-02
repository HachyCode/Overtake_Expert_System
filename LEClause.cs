using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overtake_Expert_System
{
    class LEClause : Clause
    {
        public LEClause(string variable, string value)
            : base(variable, value)
        {
            Condition = "<=";
        }

        protected override IntersectionType Intersect(Clause rhs)
        {
            string v1 = _value;
            string v2 = rhs.Value;

            double a = 0;
            double b = 0;

            if (double.TryParse(v1, out a) && double.TryParse(v2, out b))
            {
                if (rhs is LEClause)
                {
                    
                    if (b <= a)
                    {
                        return IntersectionType.INCLUDE;
                    }
                    else
                    {
                        return IntersectionType.UNKNOWN;
                    }
                }
                else if (rhs is LessClause)
                {
                    
                    if (b <= a)
                    {
                        return IntersectionType.INCLUDE;
                    }
                    else
                    {
                        return IntersectionType.UNKNOWN;
                    }
                }
                else if (rhs is IsClause)
                {
                    
                    if (b <= a)
                    {
                        return IntersectionType.INCLUDE;
                    }
                    else
                    {
                        return IntersectionType.MUTUALLY_EXCLUDE;
                    }
                }
                else if (rhs is GEClause)
                {
                    
                    if (b > a)
                    {
                        return IntersectionType.MUTUALLY_EXCLUDE;
                    }
                    else
                    {
                        return IntersectionType.UNKNOWN;
                    }
                }
                else if (rhs is GreaterClause)
                {
                    
                    if (b >= a)
                    {
                        return IntersectionType.MUTUALLY_EXCLUDE;
                    }
                    else
                    {
                        return IntersectionType.UNKNOWN;
                    }
                }
                else
                {
                    return IntersectionType.UNKNOWN;
                }
            }
            else
            {
                return IntersectionType.UNKNOWN;
            }


        }
    }
}
