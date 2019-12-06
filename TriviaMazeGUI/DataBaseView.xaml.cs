using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
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
using System.Data;
using System.Data.SQLite;

namespace TriviaMazeGUI
{
    /// <summary>
    /// Interaction logic for DataBaseView.xaml
    /// </summary>
    public partial class DataBaseView : Window
    {
        //Information on how to do this was taken from this source:
        //https://medium.com/@mehanix/lets-talk-security-salted-password-hashing-in-c-5460be5c3aae and:
        //https://stackoverflow.com/questions/52146528/how-to-validate-salted-and-hashed-password-in-c-sharp

        private byte[] salt;
        private string sallt;
        private RNGCryptoServiceProvider rng;
        private Regex check;
        private SQLiteConnection SQLconnection;

        public DataBaseView()
        {
            InitializeComponent();
        }

        /* private void NumInput_Click(object sender, RoutedEventArgs e)
         {
             string temp = "";
             string password = "";
             string l = "";

             MessageBox.Show(temp);
             check = new Regex(@"^(?!.*([a-z])\1{3})(?=.*[0-9])(?=.*[A-Z])(?=.*[a-z])(?=.*[!@#$%^&*?])[\w!@#$%^&*?]{10,}$");
             if (_in.Text != null)
             {
                 if (check.IsMatch(_in.Text))
                 {
                     rng = new RNGCryptoServiceProvider();
                     rng.GetBytes(salt = new byte[16]);
                     sallt = Convert.ToBase64String(salt); 

                     for (int i = 0; i < 16; i++)
                     {
                         temp += salt[i].ToString() + "-";
                     }

                     var pbkdf2 = new Rfc2898DeriveBytes(_in.Text, salt, 10000);
                     password = Convert.ToBase64String(pbkdf2.GetBytes(30));
                    //byte[] hash = pbkdf2.GetBytes(20);
                    // byte[] hashBytes = new byte[36];
                     //l = Convert.ToBase64String(hash);
                     //MessageBox.Show(password);
                 }
                 else
                 {
                     MessageBox.Show("Incorrect");
                 }
                 System.IO.File.WriteAllText(@"C:\Users\Matthew\Desktop\CSCD\CSCD350\FN2\CSCD350_TriviaMaze\Salt.txt", temp);
                 System.IO.File.WriteAllText(@"C:\Users\Matthew\Desktop\CSCD\CSCD350\FN2\CSCD350_TriviaMaze\Password.txt", password);
             }
         }*/
        private void NumInput_Click(object sender, RoutedEventArgs e)
        {
            string passwordHash = "";
            string pw = "";
            string temp = "";
            check = new Regex(@"^(?!.*([a-z])\1{3})(?=.*[0-9])(?=.*[A-Z])(?=.*[a-z])(?=.*[!@#$%^&*?])[\w!@#$%^&*?]{10,}$");
            if (_in.Password != null)
            {
                if (check.IsMatch(_in.Password))
                {
                    SQLconnection = new SQLiteConnection(MainWindow.ConnectionInfo);
                    SQLconnection.Open();
                    using (SQLiteCommand ins = new SQLiteCommand(@"SELECT * FROM Password", SQLconnection))
                    {
                        using (SQLiteDataReader read = ins.ExecuteReader())
                        {
                            if (read.Read())
                            {
                                pw = @read["Salt"].ToString();
                                passwordHash = @read["Encrypted"].ToString();
                            }
                            read.Close();

                        }

                    }
                    string[] theSalt = pw.Split('-');
                    salt = new byte[16];

                    for (int i = 0; i < 16; i++)
                    {
                        byte k = Convert.ToByte(theSalt[i]);
                        salt[i] = k;
                    }

                    var en = new Rfc2898DeriveBytes(_in.Password, salt, 1000);
                    temp = Convert.ToBase64String(en.GetBytes(30));
                    if (temp.Equals(passwordHash))
                    {
                        AddTrueFalseToTable();
                        AddMultipleChoiceToTable();
                        AddShortAnswerToTable();

                        answers.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    MessageBox.Show("You Entered the wrong password");
                }

            }

        }

        private void AddTrueFalseToTable()
        {
            DataTable tf = new DataTable();
            tf.Columns.Add(new DataColumn("Question"));
            tf.Columns.Add(new DataColumn("Answer"));

            using (SQLiteCommand ins = new SQLiteCommand(@"SELECT * FROM TrueFalse", SQLconnection))
            {
                using (SQLiteDataReader read = ins.ExecuteReader())
                {
                    while (read.Read())
                    {
                        DataRow dr = tf.NewRow();
                        dr["Question"] = read["Question"].ToString();
                        dr["Answer"] = read["Answer"].ToString();
                        tf.Rows.Add(dr);
                    }

                    read.Close();
                }
            }
            TrueFalse.ItemsSource = tf.DefaultView;
            TrueFalse.DataContext = tf;

        }
        private void AddMultipleChoiceToTable()
        {
            DataTable mc = new DataTable();
            mc.Columns.Add(new DataColumn("Question"));
            mc.Columns.Add(new DataColumn("Answer"));
            using (SQLiteCommand ins = new SQLiteCommand(@"SELECT * FROM MultipleChoice", SQLconnection))
            {
                using (SQLiteDataReader read = ins.ExecuteReader())
                {
                    while (read.Read())
                    {
                        DataRow dr = mc.NewRow();
                        dr["Question"] = read["Question"].ToString();
                        dr["Answer"] = read["Answer"].ToString();
                        mc.Rows.Add(dr);
                    }

                    read.Close();
                }
            }
            MultipleChoice.ItemsSource = mc.DefaultView;
            MultipleChoice.DataContext = mc;
        }
        private void AddShortAnswerToTable()
        {
            DataTable sc = new DataTable();
            sc.Columns.Add(new DataColumn("Question"));
            sc.Columns.Add(new DataColumn("Answer"));
            using (SQLiteCommand ins = new SQLiteCommand(@"SELECT * FROM ShortAnswer", SQLconnection))
            {
                using (SQLiteDataReader read = ins.ExecuteReader())
                {
                    while (read.Read())
                    {
                        DataRow dr = sc.NewRow();
                        dr["Question"] = read["Question"].ToString();
                        dr["Answer"] = read["Answer"].ToString();
                        sc.Rows.Add(dr);
                    }

                    read.Close();
                }
            }
            ShortAnswer.ItemsSource = sc.DefaultView;
            ShortAnswer.DataContext = sc;
        }
    }
}
