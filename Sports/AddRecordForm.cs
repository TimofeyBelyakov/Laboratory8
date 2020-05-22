using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Lab8
{
    public partial class AddRecordForm : Form
    {
        SubjectsForm mainForm;

        public AddRecordForm()
        {
            InitializeComponent();
        }

        public AddRecordForm(SubjectsForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        /// <summary>
        ///     Обработка нажатия "Добавить запись"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addBtn_Click(object sender, EventArgs e)
        {
            string msg = Controller.AddRowToXMLFile(
                    Surname.Text,
                    Code.Text,
                    TypeEx.Text,
                    Coach.Text,
                    Date.Text,
                    Time.Text,
                    Long.Text,
                    PricePerMin.Text
                );
            MessageBox.Show(msg);
            if(msg == "Запись была успешно внесена!") addRecordToDisplay();
        }

        /// <summary>
        ///     Выводим новую запись на дисплей
        /// </summary>
        private void addRecordToDisplay()
        {
            XElement node = (XElement)Controller.xmlFile.Root.LastNode;
            String record = node.LastAttribute + " ";
            foreach (XElement node2 in node.Nodes())
            {
                record += node2.Value + " ";
            }
            mainForm.recordsList.Items.Add(record);
        }

        /// <summary>
        ///     Обработка закрытия формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}