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
    /// Interaction logic for FormMedecin.xaml
    /// </summary>
    public partial class FormMedecin : Window
    {
        public FormMedecin()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(dataGridPatient.SelectedIndex > -1)
            {
                CongedierPatient();
            } else
            {
                MessageBox.Show("Veullez choisir un patient à qui donner le congé.");
            }

        }
        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            

            var queryPatients =

           from a in MainWindow.myBDD.Admissions
           join p in MainWindow.myBDD.Patients on a.NSS equals p.NSS
           join m in MainWindow.myBDD.Medecins on a.IDMedecin equals m.IDMedecin
           join l in MainWindow.myBDD.Lits on a.NumeroLit equals l.NumeroLit
           where l.Occupe == true
           select new { p.Nom, p.Prenom, p.NSS, a.DateAdmission, a.DateChirurgie, a.NumeroLit, nomMedecin = m.Nom };

            dataGridPatient.DataContext = queryPatients.ToList();
        }

        private void CongedierPatient()
        {

            if(dataGridPatient.SelectedIndex > -1)
            {

                dynamic row = dataGridPatient.SelectedItem;

                string selectedNSS = row.NSS;

                Patient p = MainWindow.myBDD.Patients.Single(a => a.NSS == selectedNSS);

                string IDadmission = p.Admissions.Last().IDAdmission.Trim();

                Admission ad = MainWindow.myBDD.Admissions.Single(a => a.IDAdmission == IDadmission);

                ad.Lit.Occupe = false;
                ad.DateDuConge = DateTime.Today;
                MainWindow.myBDD.SaveChanges();

                //calculer le prix
                int prix = 0;

                MessageBox.Show($"Le patient a été libéré! Sa facture s'élève à {prix}$");

            } else
            {
                MessageBox.Show("Vous devez sélectionner un patient!");
            }
        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            string recherche = txt_recherche.Text;

            var queryRecherche =

            from p in MainWindow.myBDD.Patients
            where p.Nom.Contains(recherche) ||
            p.Prenom.Contains(recherche) ||
            p.NSS.Contains(recherche)
            select new
            {
                p.Nom,
                p.Prenom,
                p.NSS,
                Adresse = p.Adresse.Trim() + ", " + p.Ville.Trim() + ", " + p.Province.Trim() + ", " + p.CodePostal.Trim()
            ,
                p.Telephone
            };

            dataGridPatient.DataContext = queryRecherche.ToList();
        }

        
    }
}
