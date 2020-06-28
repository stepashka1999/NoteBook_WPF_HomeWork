using Microsoft.Win32;
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

namespace NoteBook
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Notebook notebook = new Notebook();  //Класс записной книжки
        int thisIndex = 0;  // Индекс текущей записи
        public MainWindow()
        {
            InitializeComponent();
        }

        #region Open/Save/New File
       
        /// <summary>
        /// Обработчик события нажатия на кнопку Open File
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "XML Files |*.xml";
            
            if (MessageBox.Show("Все несохраненные записи будут утерены. Открыть?", "Внимание!", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                var res = ofd.ShowDialog();

                if (res == true)
                {
                    var path = ofd.FileName;
                    notebook.Open(path);
                }
                ShowRecord(thisIndex);
            }
        }

        /// <summary>
        /// Обработчик события нажатия на кнопку Save File
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveFile_Click(object sender, RoutedEventArgs e)
        {
            if (notebook.Length != -1)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "XML Files |*.xml";
                var res = sfd.ShowDialog();

                if (res == true)
                {
                    var path = sfd.FileName;
                    notebook.Save(path);
                }
            }
            else
            {
                MessageBox.Show("Отсутствуют записи для сохранения.");
            }
        }
                
        /// <summary>
        /// Обрпаботчик события нажатия на кнопку New File
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewFile_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Все несохраненные записи будут утерены. Создать?", "Внимание!", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                notebook.NewNoteBook();
                thisIndex = 0;
                ClearContent();
            }
        }
        
        /// <summary>
        /// Добавляет данные из файла к текущему списку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddTo_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "XML Files |*.xml";

            if (MessageBox.Show("Все несохраненные записи будут утерены. Открыть?", "Внимание!", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                var res = ofd.ShowDialog();

                if (res == true)
                {
                    var path = ofd.FileName;
                    notebook.Open(path, true);
                }
                ShowRecord(thisIndex);
            }
        }
        
        #endregion


        #region Left/Right slide

        /// <summary>
        /// Обработчик события слайда влево
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Left_Slide_bttn_Click(object sender, RoutedEventArgs e)
        {
            if (notebook.Length != -1)
            {
                thisIndex--;
                if (thisIndex < 0) thisIndex = notebook.Length - 1;

                ShowRecord(thisIndex);
            }
        }

        /// <summary>
        /// Обработчик события слайда вправо
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Right_Slide_bttn_Click(object sender, RoutedEventArgs e) 
        {
            if (notebook.Length != -1)
            {
                thisIndex++;
                if (thisIndex > notebook.Length - 1) thisIndex = 0;

                ShowRecord(thisIndex);
            }
        }

        #endregion

        #region Clear/New/Save/Delete Record

        /// <summary>
        /// Обработчик события очистки интерфейса
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearRecord_bttn_Click(object sender, RoutedEventArgs e)
        {
            ClearContent();
        }
               
        /// <summary>
        /// Обработчик события нажатия на кнопку New File
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewRecord_Click(object sender, RoutedEventArgs e)
        {
            thisIndex = notebook.Length;
            ClearContent();
        }

        /// <summary>
        /// Обработчик события нажатия на кнопку Save Record
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveRecord_Click(object sender, RoutedEventArgs e)
        {
            if(notebook.Length == -1)
            {
                notebook.NewNoteBook();
                notebook.AddRecord(Title_tb.Text, Writer_Content_tb.Text, Content_tb.Text);
            }
            else if(thisIndex < notebook.Length)
            {
                notebook.ChangeRecord(thisIndex, Title_tb.Text, Writer_Content_tb.Text, Content_tb.Text);
            }
            else
            {
                notebook.AddRecord(Title_tb.Text, Writer_Content_tb.Text, Content_tb.Text);
            }

            thisIndex = notebook.Length - 1;
            ShowRecord(thisIndex);
        }
        
        /// <summary>
        /// Удаляет текущую запись из списка
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteRecord_Click(object sender, RoutedEventArgs e)
        {
            notebook.Delete(thisIndex);
            if(notebook.Length - 1 > 0)
            {
                thisIndex = notebook.Length - 1;
                ShowRecord(thisIndex);
            }
            else
            {
                thisIndex = 0;
                ClearContent();
            }

        }

        #endregion
        

        /// <summary>
        /// Очищает интерфейс
        /// </summary>
        private void ClearContent()
        {
            Title_tb.Text = "";
            Writer_Content_tb.Text = "";
            Content_tb.Text = "";

            ID_Content_lbl.Content = thisIndex;
            Symbols_Content_lbl.Content = 0;
            Records_Contetnt_lbl.Content = notebook.Length;

            CreateTime_lbl.Content = "";
            ChangeTime_lbl.Content = "";
        }

        /// <summary>
        /// Отображение записи в интерфейсе
        /// </summary>
        /// <param name="index"></param>
        private void ShowRecord(int index)
        {
            var thisRec = notebook[index];

            Title_tb.Text = thisRec.Title;
            Writer_Content_tb.Text = thisRec.Writer;
            Content_tb.Text = thisRec.Content;

            ID_Content_lbl.Content = index + 1;
            Symbols_Content_lbl.Content = thisRec.Content.Length;
            Records_Contetnt_lbl.Content = notebook.Length;

            CreateTime_lbl.Content = thisRec.DateOfCreate.ToString();
            
            if(thisRec.DateOfCreate == thisRec.DateOfChanges)
            {
                ChangeTime_lbl.Content = "Не редактировалось";
            }
            else
            {
                ChangeTime_lbl.Content = thisRec.DateOfChanges;
            }

        }

        #region SortBy

        /// <summary>
        /// Обработчик события нажатия на кнопку Sort -> Date -> Create Date UP
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortByCreationDateUp_Click(object sender, RoutedEventArgs e)
        {
            notebook.SortBy(11);
            ShowRecord(thisIndex);
        }

        /// <summary>
        /// Обработчик события нажатия на кнопку Sort -> Date -> Create Date DOWN
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortByCreationDateDown_Click(object sender, RoutedEventArgs e)
        {
            notebook.SortBy(12);
            ShowRecord(thisIndex);
        }

        /// <summary>
        /// Обработчик события нажатия на кнопку Sort -> Date -> Change Date UP
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortByChangeDateUp_Click(object sender, RoutedEventArgs e)
        {
            notebook.SortBy(13);
            ShowRecord(thisIndex);
        }

        /// <summary>
        /// Обработчик события нажатия на кнопку Sort -> Date -> Create Date DOWN
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortByChangingDateDown_Click(object sender, RoutedEventArgs e)
        {
            notebook.SortBy(14);
            ShowRecord(thisIndex);
        }

        /// <summary>
        /// Обработчик события нажатия на кнопку Sort -> Content -> Length UP
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortByContentLengthUp_Click(object sender, RoutedEventArgs e)
        {
            notebook.SortBy(21);
            ShowRecord(thisIndex);
        }

        /// <summary>
        /// Обработчик события нажатия на кнопку Sort -> Content -> Length DOWN
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortByContentLengthDown_Click(object sender, RoutedEventArgs e)
        {
            notebook.SortBy(22);
            ShowRecord(thisIndex);
        }

        /// <summary>
        /// Обработчик события нажатия на кнопку Sort -> Writer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortByWriter_Click(object sender, RoutedEventArgs e)
        {
            notebook.SortBy(3);
            ShowRecord(thisIndex);
        }

        /// <summary>
        /// Обработчик события нажатия на кнопку Sort -> Title
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortByTitle_Click(object sender, RoutedEventArgs e)
        {
            notebook.SortBy(4);
            ShowRecord(thisIndex);
        }

        #endregion

        /// <summary>
        /// Обработчик события нажатия на кнопку ImportByDate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImportByDate_Click(object sender, RoutedEventArgs e)
        {
            var picker = new PickDateForImport();
            picker.Show();
            picker.Closed += Picker_Closed;
        }

        /// <summary>
        /// Обработчик события закрытия формы PickDateForImport
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Picker_Closed(object sender, EventArgs e)
        {
            var fromD = (sender as PickDateForImport).dateFrom;
            var toD = (sender as PickDateForImport).dateTo;

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "XML Files |*.xml";
            var res = sfd.ShowDialog();


            if (res == true)
            {
                var path = sfd.FileName;
                var importRes = notebook.ImportByDate(fromD, toD,path);

                if (importRes != 0) MessageBox.Show("Данны за заданный период отсутствуют.");
            }
        }
    }
}
