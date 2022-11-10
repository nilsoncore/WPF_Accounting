using System;
using System.Linq;
using System.Windows;

namespace Practice4438_Task6
{
    /// <summary>
    /// Логика взаимодействия для DeliveryNoteChangeWindow.xaml
    /// </summary>
    public partial class DeliveryNoteChangeWindow : Window
    {
        public DeliveryNoteChangeWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Метод изменения Накладной
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeDeliveryNote(object sender, RoutedEventArgs e)
        {
            bool success = false;
            foreach(DeliveryNote item in MainWindow.db.DeliveryNote)
            {
             
                if(item.Id == DeliveryNoteWindow.NoteToChange.Id)
                {
                    DeliveryNote note = new DeliveryNote();

                    // Destination
                    try
                    {
                       note.Destination = (DeliveryDestination.SelectedItem as Destination);
                    }
                    catch
                    {
                        MessageBox.Show("Возникла непредвиденная ошибка");
                        break;
                    }

                    // Buyer
                    try
                    {
                       note.Buyer = (DeliveryBuyer.SelectedItem as Buyer);
                    }
                    catch
                    {
                        MessageBox.Show("Возникла непредвиденная ошибка");
                        break;
                    }

                    // Date
                    try
                    {
                       note.Date = DateTime.Parse(DeliveryDate.Text);
                    }
                    catch
                    {
                        MessageBox.Show("Вы неверно ввели Дату \nВерный формат даты ДД:ММ:ГГ 00:00:00");
                        break;
                    }

                    // Price
                    try
                    {
                        note.Price = decimal.Parse(DeliveryPrice.Text);
                    }
                    catch
                    {
                        MessageBox.Show("Вы неверно ввели Цену \nНеобходимо использовать тип Decimal(денежный)");
                        break;
                    }

                    // Count
                    try
                    {
                        note.Count = int.Parse(DeliveryCount.Text);
                    }
                    catch
                    {
                        MessageBox.Show("Вы неверно ввели Количество \nНеобходимо использовать тип Int(целочисленный)");
                        break;
                    }

                    item.Price = note.Price;
                    item.Date = note.Date;
                    item.Count = note.Count;
                    item.Destination = note.Destination;
                    item.DestinationId = note.Destination.Id;
                    item.Buyer = note.Buyer;
                    item.BuyerId = note.Buyer.Id;
                    success = true;
                }
            }

            if (success)
            {
                MessageBox.Show("Вы успешно изменили накладную");
                this.Visibility = Visibility.Hidden;
                MainWindow.db.SaveChanges();
                MainWindow.DevNoteWindow.DeliveryNoteList.ItemsSource = MainWindow.db.DeliveryNote.ToList();
            }
        }

        /// <summary>
        /// Метод закрытия окна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void close(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = Visibility.Hidden;
        }
    }
}
