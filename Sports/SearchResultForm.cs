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
    public partial class SearchResultForm : Form
    {
        public SearchResultForm()
        {
            InitializeComponent();
        }

        public string DisplayResult(IEnumerable<XNode> result)
        {
            string strMessage = "Записи найдены!";

            if (result.Count() == 0)
            {
                strMessage = "Не найдены записи, соответствующие данным критериям.";
                resultDisplayBox.Items.Add(strMessage);
            }
            else
            {
                String record = "";
                foreach (XElement node in result)
                {
                    record += node.LastAttribute + " ";
                    foreach (XElement node2 in node.Nodes())
                    {
                        record += node2.Value + " ";
                    }
                    resultDisplayBox.Items.Add(record);
                    record = "";
                }
            }
            
            return strMessage;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}