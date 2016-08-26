using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weebul.Util;

namespace Weebul.Core.Helpers
{
    public class RandomGen
    {

        [ThreadStatic]
        private static ThreadSafeRandom _random_Gen;



        public static ThreadSafeRandom RANDOM_GEN
        {
            get
            {
                if(_random_Gen == null)
                {
                    _random_Gen = new ThreadSafeRandom();
                }
                return _random_Gen;
            }
        }

        public static double NextDouble()
        {
            return RANDOM_GEN.NextDouble();
        }

        public static double GetRandomNormal(double mean, double stdDev)
        {

            double u1 = NextDouble();
            double u2 = NextDouble(); 
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);
            double randNormal = mean + stdDev * randStdNormal;
            return randNormal;

        }

        public static bool CheckRandom(double percentChance)
        {
            return RANDOM_GEN.NextDouble() < percentChance;
        }
    }
}
