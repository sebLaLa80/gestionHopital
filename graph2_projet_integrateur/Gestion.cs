using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace graph2_projet_integrateur
{
    public class Gestion
    {
        public bool VerifierNSS(Patient p)
        {
            foreach (var item in MainWindow.myBDD.Patients)
            {
                if (item.NSS.Equals(p.NSS))
                {
                    return false;
                }
            }

            return true;
        }

        public List<Lit> getLit(Patient p)
        {   //lits innoccupes
            IEnumerable<Lit> dispo = MainWindow.myBDD.Lits.Where(a => a.Occupe == false);

            DateTime age = (DateTime)p.DateNaissance;
            DateTime ajourdhui = DateTime.Now;
            DateTime seizeAns = age.AddYears(16);

            if (seizeAns > ajourdhui) //patient moins que 16 ans
            {
                IEnumerable<Lit> dispoPediatrie = dispo.Where(a => a.IDDepartement == "2"); //pour l'instant cest les lits en psychiatrie il faut update la DB
                //si des lits sont dispo dans le dp, on retourne cette liste
                if (dispoPediatrie.Count() > 0)
                {
                    return dispoPediatrie.ToList();
                }
            }
            //sinon on retourne la liste generique de lits dispo
            return dispo.ToList();
        }
    }
}
