using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Lab8
{
    public partial class SubjectsForm : Form
    {
        public SubjectsForm()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     Обработка нажатия кнопки "Выбрать файл с данными"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chooseFileClick(object sender, EventArgs e)
        {
            if (chooseFile.ShowDialog() == DialogResult.OK)
            {
                labelFile.Text = Controller.CheckChooseFile(chooseFile.FileName);
            }
        }

        /// <summary>
        ///     Заполняет RecordList данными из файла
        /// </summary>
        public void displayRecords()
        {
            if (Controller.xmlFile == null) return;
            recordsList.Items.Clear();
            String record = "";
            foreach (XElement node in Controller.xmlFile.Root.Nodes().ToList())
            {
                record += node.LastAttribute + " ";
                foreach (XElement node2 in node.Nodes())
                {
                    record += node2.Value + " ";
                }
                recordsList.Items.Add(record);
                record = "";
            }

        }

        /// <summary>
        ///     Обработка нажатия "Загрузить данные из файла"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loadDataBtn_Click(object sender, EventArgs e)
        {
            displayRecords();
        }

        /// <summary>
        ///     Обработка нажатия "Добавить запись"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addRecordBtn_Click(object sender, EventArgs e)
        {
            new AddRecordForm(this).Show();
        }

        /// <summary>
        ///     Функция удаления записи
        /// </summary>
        private void deleteRecord()
        {
            if (recordsList.SelectedItem != null)
            {
                String record = recordsList.SelectedItem.ToString();
                String id = record.Substring(record.IndexOf('\"') + 1, 3);
                recordsList.Items.Remove(recordsList.SelectedItem);

                MessageBox.Show(Controller.DeleteRowFromXMLFile(id));
                displayRecords();
            }
            else
            {
                MessageBox.Show("Выберите запись!");
            }
        }

        /// <summary>
        ///     Обработка нажатия "Удалить запись"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteRecordBtn_Click(object sender, EventArgs e)
        {
            deleteRecord();
        }

        /// <summary>
        ///     Обработка нажатия "Поиск записей"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void findRecordsBtn_Click(object sender, EventArgs e)
        {
            new SearchRecordsForm(this).Show();
        }
    }
}