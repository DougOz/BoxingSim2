using NUnit.Framework;
using Weebul.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weebul.Core.Helpers;
using System.Diagnostics;

namespace Weebul.Core.Model.Tests
{
    [TestFixture]
    public class RoundTests
    {
        [Test]
        [TestCase(80, 80, 0.05, 3)]
        [TestCase(80, 78, 0.05, 3)]
        [TestCase(80, 76, 0.05, 3)]
        public void GetRoundScoreNoStunTest(int score1, int score2, double judgeLuck, double tieThreshold)
        {

            Resources.RoundTieThreshold = tieThreshold; 

            int numWin = 0;
            int numLoss = 0;
            int numTie = 0;

            int numTrials = 10000;
            
            for(int i = 1; i<= numTrials; i++)
            {
                FightScore fs = Round.GetRoundScoreNoStun(score1, score2, judgeLuck);
                if(fs.Fighter1Score > fs.Fighter2Score)
                {
                    numWin++; 
                }
                else if (fs.Fighter2Score > fs.Fighter1Score)
                {
                    numLoss++;

                }
                else
                {
                    numTie++;
                }
            }
            Debug.Print(String.Format("{0}-{1} start, luck: {2}, tie:{3}, Win: {4} Tie: {5} Loss: {6}", score1, score2, judgeLuck, tieThreshold, numWin, numTie, numLoss));
            




        }
    }
}