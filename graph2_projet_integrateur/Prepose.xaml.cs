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
    /// Interaction logic for Prepose.xaml
    /// </summary>
    public partial class Prepose : Window
    {
        public Prepose()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           var queryPatients =

           from p in MainWindow.myBDD.Patients
           select new { p.Nom, p.Prenom, p.NSS, Adresse = p.Adresse.Trim() + ", " + p.Ville.Trim() + ", " + p.Province.Trim() + ", " + p.CodePostal.Trim()
           , p.Telephone };

            dataGridPatient.DataContext = queryPatients.ToList();
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

        private void Ajouter_Click(object sender, RoutedEventArgs e)
        {
            PreposeAjouter prep = new PreposeAjouter();
            prep.Show();
            this.Close();
        }
        private void Admettre_Click(object sender, RoutedEventArgs e)
        { 
            if(dataGridPatient.SelectedIndex > -1)
            {
                //prendre le NSS de la sélection
                //chercher le patient avec ce NSS
                Patient p = new Patient(); //remplacer le "new Patient()"; par celui qu'on a été cherché avec le NSS
                PreposeAdmettre prep = new PreposeAdmettre(p);
                prep.Show();
                this.Close();
            }
        }
    }
}
