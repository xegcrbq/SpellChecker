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

namespace SpellChecker
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

        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            string fileText ="";
            string[] fileSplittedText;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
                fileText = File.ReadAllText(openFileDialog.FileName);
            fileText = fileText.Replace("\r", "");
            fileSplittedText = fileText.Split("===");
            if (fileSplittedText.Length == 3)
            {
                dictionary.Text = (fileSplittedText[0].Last() == '\n') ? fileSplittedText[0].Remove(fileSplittedText[0].Length - 1, 1) : fileSplittedText[0];
                textData.Text = (fileSplittedText[1].Last() == '\n') ? fileSplittedText[1].Remove(fileSplittedText[1].Length - 1, 1) : fileSplittedText[1];
                if (textData.Text[0] == '\n')
                    textData.Text = textData.Text.Remove(0, 1);
            }
            else
            {
                dictionary.Text = "incorrect input file!";
                textData.Text = "incorrect input file!";
            }
                
            output.Text = "";


        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            Spelling sp = new Spelling(dictionary.Text);
            output.Text = sp.Start(textData.Text);
        }
    }
}
