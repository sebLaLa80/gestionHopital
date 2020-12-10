﻿using System;
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
            Lit litAdmission = DeterminerLit(RecupererLit(), chirurgie, GetAge(patient));
            string numeroDuLit = litAdmission.NumeroLit.ToString();
            bool televiseur = (bool) ck_Televiseur.IsChecked ? true : false;
            bool telephone = (bool)ck_Telephone.IsChecked ? true : false;
            string PatientNSS = patient.NSS;

            Admission nouvelleAdmission = new Admission();
            nouvelleAdmission.IDAdmission = IDadmission;
            nouvelleAdmission.DateAdmission = dateAdmission;
            nouvelleAdmission.IDMedecin = IDmedecin;
            nouvelleAdmission.ChirurgieProgrammee = chirurgie;
            nouvelleAdmission.DateChirurgie = dateChirurgie;
            nouvelleAdmission.Lit = litAdmission;
            nouvelleAdmission.NumeroLit = numeroDuLit;
            nouvelleAdmission.Telephone = telephone;
            nouvelleAdmission.Televiseur = televiseur;
            nouvelleAdmission.Patient = patient;

            MainWindow.myBDD.Admissions.Add(nouvelleAdmission);
            MainWindow.myBDD.SaveChanges();
            MessageBox.Show("Admission ajouté avec succès !");

        }

        private int GetAge(Patient patient)
        {
            DateTime dateNaissance = patient.DateNaissance.Value;
            DateTime dateAujourdhui = DateTime.Today;
            int age = dateAujourdhui.Year - dateNaissance.Year; 
            if(dateNaissance > dateAujourdhui.AddYears(-age))
            {
                age--;
            }

            return age; 
        }

        private Lit DeterminerLit(string typeDeLit, string chirurgie, int agePatient)
        {
            List<Lit> listeLit = RecupererListeLit();

            foreach(Lit l in listeLit)
            {
                if(agePatient <= 16)
                {
                    if (l.TypeLit.Equals("Pédiatrie"))
                    {
                        l.Occupe = true;
                        return l; 
                    }
                } else
                {
                    if (l.Departement.Equals(chirurgie))
                    {
                        l.Occupe = true;
                        return l;
                    }
                }
            }

            MessageBox.Show("Il n'y a plus de lit disponible pour cette opération ou ce patient!");

            return null;
        }

        private List<Lit> RecupererListeLit()
        {

            var queryLits = MainWindow.myBDD.Lits.Where(Occupe => Occupe.Equals(0));

            List<Lit> listeLitNonOccupee = queryLits.ToList();

            return listeLitNonOccupee; 

        }

        private string RecupererIDMedecin()
        {
            Medecin m = RecupererMedecin();
            return m.IDMedecin;
        }

        private string RecupererLit()
        {
            string typeDeLit = cbo_Lit.Text;
            return typeDeLit;
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var patients = new List<Patient>();
            patients.Add(patient);
            
            dgInfoPatient.DataContext = patients;

            cbo_Medecin.DataContext = MainWindow.myBDD.Medecins.ToList();

            cbo_Lit.DataContext = MainWindow.ges.getLit(patient);
        }
    }
}
