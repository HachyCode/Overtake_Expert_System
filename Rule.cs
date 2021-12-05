using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overtake_Expert_System
{
    class Rule
    {
        protected Clause m_consequent = null;
        protected List<Clause> m_antecedents = new List<Clause>();
        protected bool m_fired = false;
        protected string m_name;
        protected int m_index = 0;

        public Rule(string name)
        {
            m_name = name;
        }
        public void AddAntecedent(Clause antecedent)
        {
            m_antecedents.Add(antecedent);
        }
        public void setConsequent(Clause consequent)
        {
            m_consequent = consequent;
        }

        public bool isTriggered(Memory memory)        {
            foreach (Clause antecedent in m_antecedents)
            {
                if (!memory.IsFact(antecedent))
                {
                    return false;
                }
            }

            return true;
        }

        public void fire(Memory memory)
        {
            if (!memory.IsFact(m_consequent))
            {
                memory.AddFact(m_consequent);
            }

            m_fired = true;
        }

        public bool isFired()
        {
            return m_fired;
        }

        public Clause getConsequent()
        {
            return m_consequent;
        }

        public void FirstAntecedent()
        {
            m_index = 0;
        }

        public bool HasNextAntecedents()
        {
            return m_index < m_antecedents.Count;
        }

        public Clause NextAntecedent()
        {
            Clause c = m_antecedents[m_index];
            m_index++;
            return c;
        }
    }
}
