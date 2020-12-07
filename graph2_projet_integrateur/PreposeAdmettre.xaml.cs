using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace graph2_projet_integrateur
{
    /// <summary>
    /// Interaction logic for PreposeAdmettre.xaml
    /// </summary>
    public partial class PreposeAdmettre : Window
    {
        Patient patient; 
        public PreposeAdmettre(Patient p)
        {
            patient = p; 
            InitializeComponent();
        }

        private void Admettre_Click(object sender, RoutedEventArgs e)
        {

            if (VerificationData())
            {
                NouvelleAdmission(); 
            }

            Prepose prep = new Prepose();
            prep.ShowDialog();
            this.Close();
        }

        private void NouvelleAdmission()
        {
            string IDmedecin = RecupererIDMedecin(); 
            string IDadmission = RecupererAdmission();
            string chirurgie = RecupererChirurgie();
            DateTime dateAdmission = RecupererDateAdmission();
            Nullable<DateTime> dateChirurgie = RecupererDateChirurgie();
            //Date congé = null    
            string typeDuLit = RecupererLit();
            bool televiseur = (bool) ck_Televiseur.IsChecked ? true : false;
            bool telephone = (bool)ck_Telephone.IsChecked ? true : false;
            string PatientNSS = patient.NSS; 

            //Créer une nouvelle admission et assigner les valeurs

        }

        private string RecupererIDMedecin()
        {
            Medecin m = RecupererMedecin();
            return m.IDMedecin;
        }

        private string RecupererLit()
        {
            //récupérer le choix
            //faire les vérifications obligatoires: 
                //Lorsqu’un patient n’est pas couvert par une assurance privée : s’il n’y a plus de lits disponibles dans une chambre standard alors le préposé aux admissions peut sélectionner, sans aucun frais supplémentaire, la chambre semi-privée de son choix. Le préposé aux admissions peut sélectionner, sans frais supplémentaires, une chambre privée lorsque toutes les chambres semi-privées sont occupées
                //Les patients qui vont subir une chirurgie sont automatiquement affectés à une chambre du département de chirurgie si un lit correspondant au type choisi est disponible. Dans le cas contraire, l’utilisateur peut sélectionner un autre type de lit ou une autre chambre disponible
                //Les patients qui sont âgés de 16 ans ou moins qui ne sont pas admis pour une chirurgie sont automatiquement dirigés vers les chambres du département de pédiatrie lorsqu’un lit correspondant au type choisi est disponible. Dans le cas contraire, l’utilisateur peut sélectionner un autre type de lit ou une autre chambre disponible

            //sinon, vérifier s'il reste un lit de ce choix et le prendre. 
            //sinon, prendre le prochain lit disponible peu importe le choix. 
            
            return "";
        }

        private Nullable<DateTime> RecupererDateChirurgie()
        {
            Nullable<DateTime> date;
            DateTime? dateChoisie = date_Admission.SelectedDate;
            date = (Nullable<DateTime>)dateChoisie; 
            return date;
        }

        private DateTime RecupererDateAdmission()
        {
            DateTime date = (DateTime) date_Admission.SelectedDate;
            return date; 
        }

        private string RecupererChirurgie()
        {
            return cbo_Chirurgie.SelectedItem.ToString();
        }

        private string RecupererAdmission()
        {

            var query = 
                from admission in MainWindow.myBDD.Admissions group admission by admission.DateAdmission into a 
                select a.OrderByDescending(g => g.DateAdmission).FirstOrDefault(); 

            string idadmission = (Int32.Parse(query.FirstOrDefault().IDAdmission) + 1).ToString(); 

            return idadmission;
        }

        private bool VerificationData()
        {
            if (!VertificationMedecin())
            {
                MessageBox.Show("Erreur! Vous devez entrer un médecin, une date et un lit au minimum pour chaque admission");
                return false; 
            }

            if (!VertificationDate())
            {
                MessageBox.Show("Erreur! Vous devez entrer une date pour chaque admission");
                return false;
            }

            if (!VertificationLit())
            {
                MessageBox.Show("Erreur! Vous devez entrer un type de lit pour chaque admission");
                return false;
            }

            if (!VerificationLitDisponible())
            {
                MessageBox.Show("Erreur! Il n'y a plus aucun lit disponible pour de nouveaux patients.");
                return false; 
            }

            return true; 
        }

        private bool VertificationMedecin()
        {
            if(!(cbo_Medecin.SelectedIndex > -1))
            {
                return true;
            }
            return false; 
        }

        private bool VertificationDate()
        {
            if ((!date_Admission.SelectedDate.HasValue))
            {
                return true;
            }
            return false;
        }

        private bool VertificationLit()
        {
            if (!(cbo_Lit.SelectedIndex > -1))
            {
                return true;
            }
            return false;
        }

        private Medecin RecupererMedecin()
        {
            Medecin m = (Medecin) cbo_Medecin.SelectedItem;
            return m; 
        }

        private void Annuler_Click(object sender, RoutedEventArgs e)
        {
            Prepose prep = new Prepose();
            prep.ShowDialog();
            this.Close(); 
        }

        private bool VerificationLitDisponible()
        {
            var query =
                    from lits in MainWindow.myBDD.Lits
                    where lits.Occupe.Equals(0)
                    select new { lits.NumeroLit };

            int nombreDeLitDisponible = query.Count();

            if(nombreDeLitDisponible > 0)
            {
                return true;
            }
            return false; 
        }
    }
}
