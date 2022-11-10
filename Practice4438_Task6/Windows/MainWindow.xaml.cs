using System;
using System.Linq;
using System.Windows;

using Word = Microsoft.Office.Interop.Word; 

namespace Practice4438_Task6
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static DeliveryNote ToDelete;
        public static MainWindow WindowMain;
        public static DeliveryNoteWindow DevNoteWindow = new DeliveryNoteWindow();
        public static StorageAddWindow StorageAddWindow = new StorageAddWindow();
        public static StorageWindow WindowStorage = new StorageWindow();
        public static AcceptWindow AcceptWindow = new AcceptWindow();
        public static DeliveryNoteChangeWindow DevChangeWindow = new DeliveryNoteChangeWindow();
        public static Entities db = new Entities();

        public MainWindow()
        {
            InitializeComponent();
            WindowMain = this;
        }

        /// <summary>
        /// Метод выхода из приложения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LeaveFromApp(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        /// <summary>
        /// Метод перехода на окно работы с накладными
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GoToDeliveryNoteWindow(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            DevNoteWindow.DeliveryNoteList.ItemsSource = db.DeliveryNote.ToList();
            DevNoteWindow.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Метод перехода на окно работы со складами
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GoDestinationWindow(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            WindowStorage.StorageList.ItemsSource = db.Destination.ToList();
            WindowStorage.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Метод генерации отчёта о складах
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GenerateDocClick(object sender, RoutedEventArgs e)
        {
            Word.Application app = new Word.Application();
            Word.Document    doc = app.Documents.Add();

            var paragraph                    = doc.Paragraphs.Add();
            paragraph.Format.LineSpacingRule = Word.WdLineSpacing.wdLineSpace1pt5;
            paragraph.Format.SpaceAfter      = 0;
            paragraph.Alignment              = Word.WdParagraphAlignment.wdAlignParagraphCenter;

            var range        = paragraph.Range;
            range.Text       = "Список пунктов доставки";
            range.Font.Name  = "Roboto";
            range.Font.Size  = 22;
            range.Bold       = 1;
            range.InsertParagraphAfter();

            paragraph = doc.Paragraphs.Add();

            var table = doc.Tables.Add(paragraph.Range, db.Destination.ToList().Count + 1, 4);
            table.Borders.InsideLineStyle       = table.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
            table.Range.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;

            table.Cell(1, 1).Range.Text = "№";
            table.Cell(1, 2).Range.Text = "Населенный пункт";
            table.Cell(1, 3).Range.Text = "Регион";
            table.Cell(1, 4).Range.Text = "Страна отправления";

            table.Rows[1].Range.Font.Size = 14;
            table.Rows[1].Range.Bold = 1;

            int row = 2;
            foreach (Destination item in db.Destination)
            {
                table.Cell(row, 1).Range.Text = item.Id.ToString();
                table.Cell(row, 2).Range.Text = item.Locality;
                table.Cell(row, 3).Range.Text = item.Region;
                table.Cell(row, 4).Range.Text = item.Country.Name;
                row++;
            }

            table.Range.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
            table.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
            table.Range.ParagraphFormat.LineSpacingRule = Word.WdLineSpacing.wdLineSpaceSingle;
            table.Range.ParagraphFormat.SpaceAfter = 0.0f;
            table.AutoFitBehavior(Word.WdAutoFitBehavior.wdAutoFitContent);

            paragraph = doc.Paragraphs.Add();
            paragraph = doc.Paragraphs.Add();
            paragraph.Format.LineSpacingRule = Word.WdLineSpacing.wdLineSpace1pt5;
            paragraph.Format.SpaceAfter = 0;
            paragraph.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

            range            = paragraph.Range;
            range.Text       = string.Format("Всего пунктов доставки: {0}", row-1);
            range.Font.Name  = "Roboto";
            range.Font.Size  = 14;
            range.Bold       = 0;
            range.InsertParagraphAfter();

            doc.SaveAs2(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Report.pdf", Word.WdSaveFormat.wdFormatPDF);
            app.Visible = true;
        }

        /// <summary>
        /// Метод обработки закрытия окна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closed(object sender, System.EventArgs e)
        {
            AcceptWindow.Close();
            DevNoteWindow.Close();
            StorageAddWindow.Close();
            WindowStorage.Close();
            DevChangeWindow.Close();
        }
    }
}
