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
        //https://medium.com/@mehanix/lets-talk-security-salted-password-hashing-in-c-5460be5c3aae
        private byte[] salt;
        private RNGCryptoServiceProvider rng;
        private int attempts;
        private Regex check;
        public DataBaseView()
        {
            InitializeComponent();
            GenerateSalt();
            //SetUpTrueFalse();
            AddTrueFalseToTable();
            AddMultipleChoiceToTable();
            AddShortAnswerToTable();
            attempts = 0;
        }

        private void GenerateSalt()
        {
            rng = new RNGCryptoServiceProvider();
            rng.GetBytes(salt = new byte[16]);  
        }
        private void NumInput_Click(object sender, RoutedEventArgs e)
        {
            check = new Regex(@"^(?!.*([a-z])\1{3})(?=.*[0-9])(?=.*[A-Z])(?=.*[a-z])(?=.*[!@#$%^&*?])[\w!@#$%^&*?]{10,}$");
            if (_in.Text != null)
            {
                if (check.IsMatch(_in.Text))
                {
                    var pbkdf2 = new Rfc2898DeriveBytes(_in.Text, salt, 10000);
                    byte[] hash = pbkdf2.GetBytes(20);
                    byte[] hashBytes = new byte[36];
                    Array.Copy(salt, 0, hashBytes, 0, 16);
                    Array.Copy(hash, 0, hashBytes, 16, 20);
                    string password = Convert.ToBase64String(hashBytes);
                    MessageBox.Show(password);
                }
                else
                {
                    MessageBox.Show("Incorrect");
                }
            }
        }

        private void AddTrueFalseToTable()
        {
            DataTable tf = new DataTable();
            tf.Columns.Add(new DataColumn("Question"));
            tf.Columns.Add(new DataColumn("Answer"));
            using (SQLiteCommand ins = new SQLiteCommand(@"SELECT * FROM TrueFalse", MainWindow.Instance.getConnection))
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
            using (SQLiteCommand ins = new SQLiteCommand(@"SELECT * FROM MultipleChoice", MainWindow.Instance.getConnection))
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
            using (SQLiteCommand ins = new SQLiteCommand(@"SELECT * FROM ShortAnswer", MainWindow.Instance.getConnection))
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
