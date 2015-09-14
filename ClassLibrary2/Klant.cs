using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary2
{
    public class Klant
    {
        private int nummerValue;

        public int Nummer
        {
            get { return nummerValue; }
            set { nummerValue = value; }
        }

        private int aantalVolwValue;

        public int AantalVolw
        {
            get { return aantalVolwValue; }
            set { aantalVolwValue = value; }
        }
        private int aantalKindValue;

        public int AantalKind
        {
            get { return aantalKindValue; }
            set { aantalKindValue = value; }
        }
        private int aantalBabyValue;
        
        public Klant(int vrstelnr, int aantalvolw,int aantalkind)
        {
            this.Nummer = vrstelnr;
            this.AantalVolw = aantalvolw;
            this.AantalKind = aantalkind;
        }
        public Klant()
        {

        }
        
        
    }
}
