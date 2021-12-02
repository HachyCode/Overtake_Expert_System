using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overtake_Expert_System
{
    class Memory
    {
        public List<Clause> facts = new List<Clause>();
        public Memory() { }

        public void ClearFacts()
        {
            facts.Clear();
        }
        public void AddFact(Clause fact)
        {
            facts.Add(fact);
        }
        public bool IsFact(Clause c)
        {
            foreach (Clause fact in facts)
            {
                if (fact.MatchClause(c) == IntersectionType.INCLUDE)
                {
                    return true;
                }
            }

            return false;
        }
        public bool IsNotFact(Clause c)
        {
            foreach (Clause fact in facts)
            {
                if (fact.MatchClause(c) == IntersectionType.MUTUALLY_EXCLUDE)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
