using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weebul.Scripting;

namespace Weebul.Core.Model
{
    public class FightPlan
    {
        public FighterRoundPlan Default { get; set; }
        public FighterRoundPlan GetPlan(FightRoundVariables variables)
        {
            if(WeblScript != null)
            {
                return EvaluatePlan(variables);
            }
            return Default;
        }

     
        private FighterRoundPlan EvaluatePlan(FightRoundVariables frv)
        {
            if(WeblScript == null)
            {
                WeblScript = new WeblScript(); 
            }
            ScriptVariables variables = frv.ToScriptVariables();
            ParseResult res = WeblScript.ParseAndEvaluate(this.FightPlanText, variables);
            FighterRoundPlan roundPlan = FighterRoundPlan.Parse(res.Text);
            if (res.Cheat) roundPlan.Dirty = true;
            roundPlan.HitLineNumber = res.LineNumber; 
            return roundPlan; 
        }
        
        public WeblScript WeblScript { get; set; }

        public bool Validate()
        {
            return WeblScript.ValidatePlan(FightPlanText);
        }
        public string FightPlanText { get; set; }

        public FightPlan Copy()
        {
            return new FightPlan()
            {
                FightPlanText = this.FightPlanText,
                WeblScript = new WeblScript(),
                Default = this.Default
            };
        }
        
    }
}
