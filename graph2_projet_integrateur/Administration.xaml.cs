using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for Administration.xaml
    /// </summary>
    public partial class Administration : Window
    {
        public Administration()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           
           var queryPatients =

           from a in MainWindow.myBDD.Admissions
           join p in MainWindow.myBDD.Patients on a.NSS equals p.NSS
           join m in MainWindow.myBDD.Medecins on a.IDMedecin equals m.IDMedecin 
           select new { p.Nom, p.Prenom, p.NSS, a.DateAdmission, a.DateChirurgie, a.NumeroLit, nomMedecin = m.Nom};

           dataGrid1.DataContext = queryPatients.ToList();

           liste_medecin.DataContext = MainWindow.myBDD.Medecins.ToList();

        }

        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            dynamic row = dataGrid1.SelectedItem;
            string s_nss = row.NSS;


            var query =

            from p in MainWindow.myBDD.Patients
            join ar in MainWindow.myBDD.Assurances on p.IDAssurance equals ar.IDAssurance
            join ad in MainWindow.myBDD.Admissions on p.NSS equals ad.NSS
            join l in MainWindow.myBDD.Lits on ad.NumeroLit equals l.NumeroLit
            join d in MainWindow.myBDD.Departements on l.IDDepartement equals d.IDDepartement
            join tl in MainWindow.myBDD.TypeLits on l.IDType equals tl.IDType
            where p.NSS == s_nss
            select new { d.NomDepartement, tl.Description, ar.NomCompagnie, p.Adresse, p.Ville, p.CodePostal, p.Telephone };


            txt_depart.Text = query.First().NomDepartement;
            txt_lit.Text = query.First().Description;
            txt_assurance.Text = query.First().NomCompagnie;
            txt_adresse.Text = query.First().Adresse;
            txt_ville.Text = query.First().Ville;
            txt_cp.Text = query.First().CodePostal;
            txt_tel.Text = query.First().Telephone;

        }

        private void liste_medecin_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Medecin med = liste_medecin.SelectedItem as Medecin;
                txt_nom.Text = med.Nom.Trim();
                txt_prenom.Text = med.Prenom.Trim();
            }
            catch (Exception ex)
            {
                txt_nom.Text = String.Empty;
                txt_prenom.Text = String.Empty;
            }
            
            
        }

        private void ajouter_med_Click(object sender, RoutedEventArgs e)
        {
            Medecin newMedecin = new Medecin();
            newMedecin.Nom = txt_nom.Text;
            newMedecin.Prenom = txt_prenom.Text;
            newMedecin.IDMedecin = (MainWindow.myBDD.Medecins.Count() + 1).ToString();

           var result = MessageBox.Show($"Êtes-vous sur de vouloir ajouter {newMedecin.Prenom} {newMedecin.Nom}, numéro ID : {newMedecin.IDMedecin} ?"
                , "Confirmation", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    MainWindow.myBDD.Medecins.Add(newMedecin);
                    MainWindow.myBDD.SaveChanges();
                    MessageBox.Show("Médecin ajouté avec succès !");
                    liste_medecin.DataContext = MainWindow.myBDD.Medecins.ToList();

                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void modifier_med_Click(object sender, RoutedEventArgs e)
        {
            Medecin med = liste_medecin.SelectedItem as Medecin;
            med.Nom = txt_nom.Text;
            med.Prenom = txt_prenom.Text;

            try
            {
                MainWindow.myBDD.SaveChanges();
                MessageBox.Show("Médecin modifiée avec succes!");
                liste_medecin.DataContext = MainWindow.myBDD.Medecins.ToList();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void supprimer_med_Click(object sender, RoutedEventArgs e)
        {
            Medecin med = liste_medecin.SelectedItem as Medecin;

            var result = MessageBox.Show($"Êtes-vous sur de vouloir supprimer {med.Prenom} {med.Nom}, numéro ID : {med.IDMedecin} ?"
               , "Confirmation", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    txt_nom.Text = String.Empty;
                    txt_prenom.Text = String.Empty;
                    MainWindow.myBDD.Medecins.Remove(med);
                    MainWindow.myBDD.SaveChanges();
                    MessageBox.Show("Médecin supprimé avec succès !");
                    liste_medecin.DataContext = MainWindow.myBDD.Medecins.ToList();

                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }

        }
    }
}
