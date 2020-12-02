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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace graph2_projet_integrateur
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public static northern_lights_hospitalEntities1 myBDD;
        public static Gestion ges;

        public MainWindow()
        {
            InitializeComponent();
            myBDD = new northern_lights_hospitalEntities1();
            ges = new Gestion();
        }

        private void btn_connect_Click(object sender, RoutedEventArgs e)
        {

            string utilisateur = input_username.Text;
            string motPasse = input_password.Password;

            if (!String.IsNullOrEmpty(utilisateur) && !String.IsNullOrEmpty(motPasse))
            {
                
                if (utilisateur == "admin" && motPasse == "admin")
                {
                    Administration admin = new Administration();
                    admin.ShowDialog();

                }
                else if (utilisateur == "prep" && motPasse == "prep")
                {
                    Prepose prep = new Prepose();
                    prep.ShowDialog();
                }
                else if (utilisateur == "medecin" && motPasse == "medecin")
                {

                }
                else
                {
                    MessageBox.Show("Les informations saisies ne sont pas valides.",
                    "Attention", MessageBoxButton.OK,
                    MessageBoxImage.Information);

                    input_username.Text = String.Empty;
                    input_password.Password = String.Empty;
                    input_username.Focus();
                }
            }
            else
            {
                MessageBox.Show("Vous devez saisir votre nom d'utilisateur et votre mot de passe.",
                "Attention",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
                // Nous redonnons le focus à l'élément txtUtilisateur.
                input_username.Focus();
            }
        }
    }
}
