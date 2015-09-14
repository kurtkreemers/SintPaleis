using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary2
{
    public class Voorstelling
    {
        private int nummerValue;

        public int Nummer
        {
            get { return nummerValue; }
            set { nummerValue = value; }
        }

        private DateTime dagValue;

        public DateTime Dag
        {
            get { return dagValue; }
            set { dagValue = value; }
        }
        private DateTime tijdValue;

        public DateTime Uur
        {
            get { return tijdValue; }
            set { tijdValue = value; }
        }
        private int maxValue;

        public int Max
        {
            get { return maxValue; }
            set { maxValue = value; }
        }

        public Voorstelling(int nummer, DateTime datum, DateTime uur, int max)
        {
            this.Nummer = nummer;
            this.Dag = datum;
            this.Uur = uur;
            this.Max = max;
        }
        public Voorstelling()
        {

        }
        
        
        
    }
}
