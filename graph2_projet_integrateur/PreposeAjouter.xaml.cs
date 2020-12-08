using System;
using System.Collections;
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
    /// Interaction logic for PreposeAjouter.xaml
    /// </summary>
    public partial class PreposeAjouter : Window
    {
        public PreposeAjouter()
        {
            InitializeComponent();
        }

        private void btn_ajouter_Click(object sender, RoutedEventArgs e)
        {
            AjouterPatient(); 
        }

        private void AjouterPatient()
        {
            if (VerificationData())
            {
                Patient nouveauPatient = new Patient();

                nouveauPatient.NSS = txt_nss.Text;
                nouveauPatient.DateNaissance = dpick_ddn.SelectedDate;
                nouveauPatient.Nom = txt_nom.Text;
                nouveauPatient.Prenom = txt_prenom.Text;
                nouveauPatient.Adresse = txt_adresse.Text;
                nouveauPatient.Ville = txt_ville.Text;
                nouveauPatient.Province = txt_province.Text;
                nouveauPatient.CodePostal = txt_cp.Text;
                nouveauPatient.Telephone = txt_telephone.Text;
                Assurance nouvelle = cbox_assurance.SelectedItem as Assurance;
                nouveauPatient.IDAssurance = nouvelle.IDAssurance;

                if (MainWindow.ges.VerifierNSS(nouveauPatient))
                {
                    try
                    {
                        MainWindow.myBDD.Patients.Add(nouveauPatient);
                        MainWindow.myBDD.SaveChanges();
                        MessageBox.Show("Patient ajouté avec succès !");
                        Prepose prep = new Prepose();
                        prep.Show();
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }
                else
                {
                    MessageBox.Show("Ce NAS est déja existant !");
                }

            } else
            {
                MessageBox.Show("Vous devez entrer toutes les informations correspondantes pour ce nouveau patient.");
            }
        }

        private bool VerificationData()
        {
            
            if(String.IsNullOrEmpty(txt_nss.Text))
            {
                return false; 
            }
            if (!dpick_ddn.SelectedDate.HasValue)
            {
                return false; 
            }
            if(String.IsNullOrEmpty(txt_nom.Text))
            {
                return false; 
            }
            if (String.IsNullOrEmpty(txt_prenom.Text))
            {
                return false;
            }
            if (String.IsNullOrEmpty(txt_adresse.Text))
            {
                return false;
            }
            if (String.IsNullOrEmpty(txt_ville.Text))
            {
                return false;
            }
            if (String.IsNullOrEmpty(txt_province.Text))
            {
                return false;
            }
            if (String.IsNullOrEmpty(txt_cp.Text))
            {
                return false;
            }
            if (String.IsNullOrEmpty(txt_telephone.Text))
            {
                return false;
            }
            if (!(cbox_assurance.SelectedIndex > -1))
            {
                return false;
            }
            return true; 
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            System.Data.DataTable tbl = new System.Data.DataTable();

            Assurance aucune = new Assurance();
            aucune.IDAssurance = null;
            aucune.NomCompagnie = "Aucune";

            var Assurances = MainWindow.myBDD.Assurances.ToList();
            cbox_assurance.Items.Add(aucune);
            foreach (var item in Assurances)
            {
                cbox_assurance.Items.Add(item);
            }
        }
    }
}
