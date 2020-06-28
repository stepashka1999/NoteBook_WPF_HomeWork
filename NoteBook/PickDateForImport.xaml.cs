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
using System.Windows.Shapes;

namespace NoteBook
{
    /// <summary>
    /// Логика взаимодействия для PickDateForImport.xaml
    /// </summary>
    public partial class PickDateForImport : Window
    {
        public DateTime dateFrom {get; private set;}  // Начальная дата
        public DateTime dateTo { get; private set; }  // Конечная дата

        public PickDateForImport()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Обработчик события закрытия формы выбора даты в календаре
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FromDate_CalendarClosed(object sender, RoutedEventArgs e)
        {
            dateFrom = (DateTime) FromDate.SelectedDate;
        }

        /// <summary>
        /// Обработчик события закрытия формы выбора даты в календаре
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToDate_CalendarClosed(object sender, RoutedEventArgs e)
        {
            dateTo = (DateTime)ToDate.SelectedDate;
        }

        /// <summary>
        /// Обработчик события нажатия на кнопку ОК
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OK_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
