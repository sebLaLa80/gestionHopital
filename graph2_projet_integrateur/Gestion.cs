using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
