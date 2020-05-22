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
    public partial class SearchRecordsForm : Form
    {
        SubjectsForm mainForm;

        public SearchRecordsForm()
        {
            InitializeComponent();
        }

        public SearchRecordsForm(SubjectsForm form)
        {
            InitializeComponent();
            mainForm = form;
        }

        /// <summary>
        ///     Проверки CheckBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SurnameClientChangeCheck(object sender, EventArgs e)
        {
            SurnameClientTextBox.Enabled = SurnameClient.Checked;
        }

        private void SurnameCoachChangeCheck(object sender, EventArgs e)
        {
            SurnameCoachTextBox.Enabled = SurnameCoach.Checked;
        }

        private void TypeChecked(object sender, EventArgs e)
        {
            TypeTextBox.Enabled = TypeCheck.Checked;
        }

        private void DateChecked(object sender, EventArgs e)
        {
            DatePicker.Enabled = DateCheck.Checked;
        }

        private void LongChecked(object sender, EventArgs e)
        {
            LongComboBox.Enabled = LongCheck.Checked;
            LongTextBox.Enabled = LongCheck.Checked;
        }

        /// <summary>
        ///     Обработка закрытия данной формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        ///     Вызов формы с результатами поиска
        /// </summary>
        /// <param name="result"></param>
        private void displayResult(IEnumerable<XNode> result)
        {
            if (result != null)
            {
                SearchResultForm srf = new SearchResultForm();
                srf.Show();
                srf.DisplayResult(result);
            }
        }

        /// <summary>
        ///     Обработка нажатия "Найти записи"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void searchButtonClick(object sender, EventArgs e)
        {
            IEnumerable<XNode> result = Controller.searchRecords(
                SurnameClient.Checked,
                SurnameCoach.Checked,
                TypeCheck.Checked,
                DateCheck.Checked,
                LongCheck.Checked,
                SurnameClientTextBox.Text,
                SurnameCoachTextBox.Text,
                TypeTextBox.Text,
                DatePicker.Text,
                LongComboBox.Text,
                LongTextBox.Text
                );
            displayResult(result);
        }
    }
}