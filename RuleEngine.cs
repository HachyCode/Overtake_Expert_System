using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overtake_Expert_System
{
    class RuleEngine
    {
        protected List<Rule> m_rules = new List<Rule>();
        protected Memory m_memory = new Memory();

        public RuleEngine() { }

        public void AddRule(Rule rule)
        {
            m_rules.Add(rule);
        }
        public void ClearFacts()
        {
            m_memory.ClearFacts();
        }
        public void AddFact(Clause c)
        {
            m_memory.AddFact(c);
        }
        public void Infer()
        {
            List<Rule> cs = null;
            do
            {
                cs = Match();
                if (cs.Count > 0)
                {
                    if (!FireRule(cs))
                    {
                        break;
                    }
                }
            } while (cs.Count > 0);
        }
        protected List<Rule> Match()
        {
            List<Rule> cs = new List<Rule>();
            foreach (Rule rule in m_rules)
            {
                if (rule.isTriggered(m_memory))
                {
                    cs.Add(rule);
                }
            }
            return cs;
        }
        protected bool FireRule(List<Rule> conflictingRules)
        {
            bool hasRule2Fire = false;
            foreach (Rule rule in conflictingRules)
            {
                if (!rule.isFired())
                {
                    hasRule2Fire = true;
                    rule.fire(m_memory);
                }
            }

            return hasRule2Fire;
        }
        public Memory Facts
        {
            get
            {
                return m_memory;
            }
        }
        public Clause Infer (string goal_variable, List<Clause> unproved_conditions = null)
        {
            if (unproved_conditions == null)
                unproved_conditions = new List<Clause>();
            Clause conclusion = null;
            List<Rule> goal_stack = new List<Rule>();

            foreach (Rule rule in m_rules)
            {
                Clause consequent = rule.getConsequent();
                if (consequent.Variable == goal_variable)
                {
                    goal_stack.Add(rule);
                }
            }

            foreach (Rule rule in m_rules)
            {
                rule.FirstAntecedent();
                bool goal_reached = true;
                while (rule.HasNextAntecedents())
                {
                    Clause antecedent = rule.NextAntecedent();
                    if (!m_memory.IsFact(antecedent))
                    {
                        if (m_memory.IsNotFact(antecedent)) //conflict with memory
                        {
                            goal_reached = false;
                            break;
                        }
                        else if (IsFact(antecedent, unproved_conditions)) //deduce to be a fact
                        {
                            m_memory.AddFact(antecedent);
                        }
                        else //deduce to not be a fact
                        {
                            goal_reached = false;
                            break;
                        }
                    }
                }

                if (goal_reached)
                {
                    conclusion = rule.getConsequent();
                    break;
                }
            }

            return conclusion;
        }
        protected bool IsFact(Clause goal, List<Clause> unproved_conditions)
        {
            List<Rule> goal_stack = new List<Rule>();

            foreach (Rule rule in m_rules)
            {
                Clause consequent = rule.getConsequent();
                IntersectionType it = consequent.MatchClause(goal);
                if (it == IntersectionType.INCLUDE)
                {
                    goal_stack.Add(rule);
                }
            }

            if (goal_stack.Count == 0)
            {
                unproved_conditions.Add(goal);
            }
            else
            {
                foreach (Rule rule in goal_stack)
                {
                    rule.FirstAntecedent();
                    bool goal_reached = true;
                    while (rule.HasNextAntecedents())
                    {
                        Clause antecedent = rule.NextAntecedent();
                        if (!m_memory.IsFact(antecedent))
                        {
                            if (m_memory.IsNotFact(antecedent))
                            {
                                goal_reached = false;
                                break;
                            }
                            else if (IsFact(antecedent, unproved_conditions))
                            {
                                m_memory.AddFact(antecedent);
                            }
                            else
                            {
                                goal_reached = false;
                                break;
                            }
                        }
                    }

                    if (goal_reached)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
