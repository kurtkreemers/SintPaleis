using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary2
{
    public class Manager
    {
        public List<Voorstelling> GetVoorstelling()
        {
            List<Voorstelling> voorstellingen = new List<Voorstelling>();
            var manager = new DBmanager();
            using (var conAccess = manager.GetConnection())
            {
                using (var comGetVrst = conAccess.CreateCommand())
                {


                    comGetVrst.CommandType = CommandType.Text;
                    comGetVrst.CommandText = "SELECT vo_sleutel,vo_datum, vo_uur, vo_aantal_plaatsen from voorstellingen";

                    conAccess.Open();

                    using (var rdrAccess = comGetVrst.ExecuteReader())
                    {
                        Int32 voorstelNummerPos = rdrAccess.GetOrdinal("vo_sleutel");
                        Int32 voorstelDagPos = rdrAccess.GetOrdinal("vo_datum");
                        Int32 voorstelUurPos = rdrAccess.GetOrdinal("vo_uur");
                        Int32 voorstelPlaatsPos = rdrAccess.GetOrdinal("vo_aantal_plaatsen");

                        while (rdrAccess.Read())
                        {
                            voorstellingen.Add(new Voorstelling(rdrAccess.GetInt32(voorstelNummerPos), rdrAccess.GetDateTime(voorstelDagPos), rdrAccess.GetDateTime(voorstelUurPos), rdrAccess.GetInt32(voorstelPlaatsPos)));
                        }
                    }

                }
                return voorstellingen;
            }
        }
        public List<Klant> GetKlanten()
        {
            List<Klant> klanten = new List<Klant>();
            var manager = new DBmanager();
            using(var conAccess = manager.GetConnection())
            {
                using(var comGetKlant = conAccess.CreateCommand())
                {
                    comGetKlant.CommandType = CommandType.Text;
                    comGetKlant.CommandText = "SELECT re_voorstelling, re_aantal_volw, re_aantal_kind from reservaties";
                    //comGetKlant.CommandText = "SELECT * from reservaties";
                    conAccess.Open();


                    using(var rdrAccess = comGetKlant.ExecuteReader())
                    {
                        Int32 voorstelnrPos = rdrAccess.GetOrdinal("re_voorstelling");
                        Int32 aantalVolwPos = rdrAccess.GetOrdinal("re_aantal_volw");
                        Int32 aantalKindPos = rdrAccess.GetOrdinal("re_aantal_kind");
                   

                        while(rdrAccess.Read())
                        {
                            klanten.Add(new Klant(rdrAccess.GetInt32(voorstelnrPos), rdrAccess.GetInt32(aantalVolwPos), rdrAccess.GetInt32(aantalKindPos)));
                        }
                       
                    }
                }
            }
            return klanten;
        }
    }
}
