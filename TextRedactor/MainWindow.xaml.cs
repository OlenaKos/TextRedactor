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
using System.IO;
using Microsoft.Win32;


namespace TextRedactor
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string path = @"C:\Users\User\source\repos\TestText.txt";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void FileNew_Click(object sender, RoutedEventArgs e)
        {
            CreateNewFile(path);
        }

        private void CreateNewFile(string path)
        {
            ContentBox.Document.Blocks.Clear();
        }

        private void FileOpen_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory =  @"C:\Users\User\source\repos\";
            FlowDocument fldoc = new FlowDocument();
            if (openFileDialog.ShowDialog() == true)
            {
                Paragraph par1 = new Paragraph();
                par1.Inlines.Add(new Run(File.ReadAllText(openFileDialog.FileName)) );
                fldoc.Blocks.Add(par1);
                ContentBox.Document = fldoc;
                
            }
                //ContentBox.AppendText( File.ReadAllText(openFileDialog.FileName));
                
        }
        private void CopyDataToClipboard(FlowDocument flowDoc)

        {

            TextRange range = new TextRange(flowDoc.ContentStart,

                flowDoc.ContentEnd);

            using (Stream stream = new MemoryStream())

            {

                range.Save(stream, DataFormats.Rtf);

                Clipboard.SetData(DataFormats.Rtf,

                    Encoding.UTF8.GetString((stream as MemoryStream).ToArray()));

            }

        }

        private void FileSave_Click(object sender, RoutedEventArgs e)
        {

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = @"C:\Users\User\source\repos\";
            saveFileDialog.Filter = "Text file(*.txt)|*.txt|C# file(*.cs)|*.cs|XAML Files (*.xaml)|*.xaml|PDF files(*.pdf)|*.pdf";
            if (saveFileDialog.ShowDialog() == true)
                File.WriteAllText(saveFileDialog.FileName, ContentBox.Document.Blocks.FirstBlock.ToString());
        }

        private void TextCopy_Click(object sender, RoutedEventArgs e)
        {
            ContentBox.Copy();
        }
        private void TextInsert_Click(object sender, RoutedEventArgs e)
        {
            //FlowDocument fldoc = new FlowDocument();
            //    Paragraph par1 = new Paragraph();
            //    par1.Inlines.Add(new Run("g"));
            //    fldoc.Blocks.Add(par1);
            //ContentBox.Document = fldoc;
            ContentBox.Paste();
            
        }
        private void TextCut_Click(object sender, RoutedEventArgs e)
        {
            ContentBox.Cut();
        }

        private void TextColor_Click(object sender, RoutedEventArgs e)
        {
            ContentBox.Document.Foreground = new SolidColorBrush(Colors.Red);
        }
        private void TextFontWeight_Click(object sender, RoutedEventArgs e)
        {
            //    System.Windows.WinForms.FontDialog myFontDialog = new FontDialog();
            //ColorDialog cd = new ColorDialog();
        }
        private void TextFontSize_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
