using System.Windows;
using System.Linq;

namespace Practice4438_Task6
{
    /// <summary>
    /// Логика взаимодействия для MachineWindow.xaml
    /// </summary>
    public partial class StorageWindow : Window
    {
        public StorageWindow()
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
        /// Метод обработки события нажатия на кнопку "Добавить" с окна StorageWindow
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddDeliveryNote(object sender, RoutedEventArgs e)
        {
            MainWindow.StorageAddWindow.Visibility = Visibility.Visible;
            MainWindow.StorageAddWindow.DestinationLocality.Text = "";
            MainWindow.StorageAddWindow.DestinationRegion.Text = "";
            MainWindow.StorageAddWindow.DestinationCountry.Text = "";
            MainWindow.StorageAddWindow.DestinationCountry.ItemsSource = MainWindow.db.Country.ToList();
        }

        /// <summary>
        /// Метод обработки закрытия окна
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
