using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Practice4438_Task6
{
    /// <summary>
    /// Логика взаимодействия для DeliveryNoteWindow.xaml
    /// </summary>
    public partial class DeliveryNoteWindow : Window
    {

        public static DeliveryNote NoteToChange;

        public DeliveryNoteWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Метод возврата на главное окно
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackToMainWindow(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            MainWindow.WindowMain.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Метод удаления накладной
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveDeliveryNote(object sender, RoutedEventArgs e)
        {
            MainWindow.ToDelete = (sender as Button).DataContext as DeliveryNote;
            MainWindow.AcceptWindow.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Метод открытия окна для изменения накладной
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeDeliveryNote(object sender, RoutedEventArgs e)
        {
            NoteToChange = (sender as Button).DataContext as DeliveryNote;

            var window = MainWindow.DevChangeWindow;
            window.DeliveryPrice.Text = NoteToChange.Price.ToString();
            window.DeliveryDate.Text = NoteToChange.Date.ToString();
            window.DeliveryCount.Text = NoteToChange.Count.ToString();
            window.DeliveryDestination.ItemsSource = MainWindow.db.Destination.ToList();
            window.DeliveryDestination.SelectedItem = NoteToChange.Destination;
            window.DeliveryBuyer.ItemsSource = MainWindow.db.Buyer.ToList();
            window.DeliveryBuyer.SelectedItem = NoteToChange.Buyer;

            MainWindow.DevChangeWindow.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Метод закрытия окна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void close(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            e.Cancel = true;
            MainWindow.WindowMain.Visibility = Visibility.Visible;
        }
    }
}
