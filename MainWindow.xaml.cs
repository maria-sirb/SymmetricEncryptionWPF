using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Security.Cryptography;

namespace SymmetricEncryption
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
           
        }

        private void ButtonAddFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            if(fileDialog.ShowDialog() == true)
            {
                textEditor.Text = File.ReadAllText(fileDialog.FileName);
            }
        }

        private void BtnEncrypt_Click(object sender, RoutedEventArgs e)
        {
            string text =  CryptoAlgorithms.EncryptText(algorithm.Text, key.Text, textEditor.Text,cipherMode.Text, paddingMode.Text);
            encryptedText.Text = text;
        }

        
    }
}
