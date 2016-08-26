using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weebul.Core.Model
{
    public class PivotFightSimulator
    {

        public PivotFightSimulator(List<PivotFighter> groupOne, List<PivotFighter> groupTwo, int numberOfSims, FightOptions options)
        {
            this.PivotGroup1 = groupOne;            
            this.PivotGroup2 = groupTwo;
            this.Options = options;
            this.NumberOfSimsEach = numberOfSims;
            SetNumbers();
        }
        public void SetNumbers()
        {
            for(int i = 1; i<= PivotGroup1.Count; i++)
            {
                PivotGroup1[i - 1].Number = i; 
            }
            for(int i = 1; i<= PivotGroup2.Count; i++)
            {
                PivotGroup2[i - 1].Number = i; 
            }
        }
        public void FightAll()
        {
            this.Results = new PivotFightResultSet[PivotGroup1.Count, PivotGroup2.Count];
            int total = PivotGroup1.Count * PivotGroup2.Count;
            int processed = 0;
            for(int i = 0; i< PivotGroup1.Count; i++)
            {

                PivotFighter pf1 = PivotGroup1[i];
                FighterFight ff1 = new FighterFight(pf1.GetStats(), pf1.FightPlan);
                
                for(int j = 0; j< PivotGroup2.Count; j++)
                {
                    
                    PivotFighter pf2 = PivotGroup2[j];
                    FighterFight ff2 = new Model.FighterFight(pf2.GetStats(), pf2.FightPlan);
                    this.Results[i, j] = new PivotFightResultSet(FightTracker.PlayMultiple_ResultSet(this.NumberOfSimsEach, ff1, ff2, this.Options));
                    processed++;
                    Debug.Print(String.Format("Pivot sim: {0} of {1}", processed, total));
                }
            }
            SetBestWorstInRowColumn();
        }
        public void SetBestWorstInRowColumn()
        {
            if (PivotGroup2.Count > 1)
            {
                for (int i = 0; i < PivotGroup1.Count; i++)
                {
                    PivotFightResultSet[] row = GetRow(i);
                    row.OrderBy(r => r.WinPercentage).First().IsWorstInRow = true;
                    row.OrderByDescending(r => r.WinPercentage).First().IsBestInRow = true;
                }
            }
            if(PivotGroup1.Count > 1)
            {
                for(int i = 0; i< PivotGroup2.Count; i++)
                {
                    PivotFightResultSet[] column = GetColumn(i);
                    column.OrderBy(r => r.WinPercentage).First().IsWorstInColumn = true;
                    column.OrderByDescending(r => r.WinPercentage).First().IsBestInColumn = true;
                }
            }
        }
        public void PrintAll()
        {
            for (int i = 0; i < PivotGroup1.Count; i++)
            {
                for (int j = 0; j < PivotGroup2.Count; j++)
                {
                    PivotFighter f1 = PivotGroup1[i];
                    PivotFighter f2 = PivotGroup2[j];
                    Debug.Print(String.Format("{0} vs. {1}, {2}", f1.Name, f2.Name, this.Results[i,j].ToString()));
                }
            }
        }

        public Dictionary<PivotFighter, PivotFightResultSet> GetRowDictionary (int rowIndex)
        {
            var ret = new Dictionary<PivotFighter, PivotFightResultSet>();
            for(int i = 0; i< Results.GetLength(1); i++)
            {
                ret.Add(PivotGroup2[i], Results[rowIndex, i]);                
            }
            return ret; 
        }
        public PivotFightResultSet[] GetRow(int rowIndex)
        {
            var list = new List<PivotFightResultSet>();
            for (int i = 0; i < Results.GetLength(1); i++)
            {
                list.Add(Results[rowIndex, i]);
            }
            return list.ToArray();            
        }
        public PivotFightResultSet[] GetColumn(int columnIndex)
        {
            var list = new List<PivotFightResultSet>();
            for (int i = 0; i < Results.GetLength(0); i++)
            {
                list.Add(Results[i, columnIndex]);
            }
            return list.ToArray();
        }
        public void ExportToCsv(string csvFile)
        {
            using (StreamWriter writer = new StreamWriter(csvFile))
            {
                string line = String.Join(",", this.PivotGroup2.Select(c => c.Name));
                writer.WriteLine("," + line);
                for(int i = 0; i< PivotGroup1.Count; i++)
                {
                    line = PivotGroup1[i].Name + "," + string.Join(",", GetRow(i).Select(s => s.WinPercentage.ToString("0.00%")));
                    writer.WriteLine(line);
                }
            }
        }
        public PivotFightResultSet[,] Results { get; set; }

        public List<PivotFightResultSet> GetRowTotals()
        {
            var ret = new List<PivotFightResultSet>();
            for(int i = 0; i< PivotGroup1.Count; i++)
            {
                PivotFightResultSet[] row = GetRow(i);
                PivotFightResultSet set = PivotFightResultSet.CombineMany(row);
                ret.Add(set);
            }
            return ret;
                        
        }
        public List<PivotFightResultSet> GetColumnTotals()
        {
            var ret = new List<PivotFightResultSet>();
            for (int i = 0; i < PivotGroup2.Count; i++)
            {
                PivotFightResultSet[] row = GetColumn(i);
                PivotFightResultSet set = PivotFightResultSet.CombineMany(row);
                ret.Add(set);
            }
            return ret;
        }        
        public List<PivotFighter> PivotGroup1 { get; set; }

        public List<PivotFighter> PivotGroup2 { get; set; }

        public int NumberOfSimsEach { get; set; }

        public FightOptions Options { get; set; }
    }
}
