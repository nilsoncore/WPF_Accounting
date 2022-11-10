using System.Linq;
using System.Windows;

namespace Practice4438_Task6
{
    /// <summary>
    /// Логика взаимодействия для DeliveryNoteAddWindow.xaml
    /// </summary>
    public partial class StorageAddWindow : Window
    {
        public StorageAddWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Метод обработки закрытия окна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void close(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = Visibility.Hidden;
        }
        
        /// <summary>
        /// Метод добавления склада
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddStorage(object sender, RoutedEventArgs e)
        {
            Destination dest = new Destination();

            // Locality
            try
            {
                dest.Locality = DestinationLocality.Text;
            }
            catch
            {
                MessageBox.Show("Вы ввели неверный номер хранилища \nОн должен находиться в текстовом формате");
                return;
            }

            // Region
            try
            {
                dest.Region = DestinationRegion.Text;
            }
            catch
            {
                MessageBox.Show("Вы ввели неверный адрес хранилища \nОн должен находиться в текстовом формате");
                return;
            }

            // CountryId
            try
            {
                dest.Country = (DestinationCountry.SelectedItem as Country);
                dest.CountryId = dest.Country.Id;
            }
            catch
            {
                MessageBox.Show("Вы ввели неверную площадь хранилища \nОна должна находиться в целочисленном формате");
                return;
            }

            MainWindow.db.Destination.Add(dest);
            MainWindow.db.SaveChanges();

            MainWindow.WindowStorage.StorageList.ItemsSource = MainWindow.db.Destination.ToList();
            this.Visibility = Visibility.Hidden;
            MessageBox.Show("Вы успешно добавили склад с номером: " + dest.Id);
        }
    }
}
