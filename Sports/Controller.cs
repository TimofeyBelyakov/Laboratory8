using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Windows.Forms;

namespace Lab8
{
    public static class Controller
    {
        public static long START_ID = 100;
        public static long lastId = -1;
        public static XDocument xmlFile;
        public static String xmlPathName;

        /// <summary>
        ///     Проверка выбранного файла
        /// </summary>
        /// <param name="FileName"></param>
        /// <returns></returns>
        public static string CheckChooseFile(String FileName)
        {
            string strMessage = "";
            if (isXml(FileName))
            {
                xmlFile = XDocument.Load(FileName);
                xmlPathName = FileName;
                try
                {
                    lastId = xmlFile.Root.Nodes().Count() == 0 ? START_ID :
                             long.Parse(((XElement)xmlFile.Root.LastNode).LastAttribute.Value);
                }
                catch { }
                strMessage = "Выбранный файл: " + FileName;
            }
            else
            {
                strMessage = "Неверный формат файла";
            }

            return strMessage;
        }

        /// <summary>
        ///     Проверка на формат .xml
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static bool isXml(String path)
        {
            string ext = path.Substring(path.LastIndexOf('.') + 1);
            return ext == "xml";
        }

        /// <summary>
        ///     Добавление записи в файл
        /// </summary>
        public static string AddRowToXMLFile(string surname, string code, string type, 
                                             string coach, string date, string time, 
                                             string longT, string price)
        {
            string strMessage = "";
            long id = lastId == -1 ? START_ID : lastId + 1;

            if (surname.Length == 0 ||
                code.Length == 0 ||
                type.Length == 0 ||
                coach.Length == 0 ||
                date.Length == 0 ||
                time.Length == 0 ||
                longT.Length == 0 ||
                price.Length == 0)
            {
                return "Заполните все поля!";
            }

            try
            {
                xmlFile.Root.Add(new XElement("Exercise", new XAttribute("ID", id),
                            new XElement("Surname", surname),
                            new XElement("Code", code),
                            new XElement("Type", type),
                            new XElement("Coach", coach),
                            new XElement("Date", date),
                            new XElement("Time", time),
                            new XElement("Long", longT),
                            new XElement("PricePerMin", price)
                ));
                xmlFile.Save(xmlPathName);
                lastId = id;

                strMessage = "Запись была успешно внесена!";
            }
            catch
            {
                strMessage = "Выберите файл!";
            }

            return strMessage;
        }

        /// <summary>
        ///     LINQ-запросы на поиск нужных записей
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<XNode> searchRecords(bool surnameClientChecked, bool surnameCoachChecked,
                                                        bool typeChecked, bool dateChecked,
                                                        bool longChecked,
                                                        string surnameClientTextBox, string surnameCoachTextBox,
                                                        string typeTextBox, string datePicker,
                                                        string longComboBox, string longTextBox)
        {
            IEnumerable<XNode> result = null;

            try
            {
                if (surnameClientChecked)
                {
                    IEnumerable<XNode> temp = xmlFile.Root.Nodes();
                    result = from node in temp
                             where ((XElement)((XElement)node).Nodes().Where((n) =>
                             ((XElement)n).Name == "Surname").Single()).Value == surnameClientTextBox
                             select node;
                }

                if (surnameCoachChecked)
                {
                    IEnumerable<XNode> temp = result == null ? xmlFile.Root.Nodes() : result;
                    result = from node in temp
                             where ((XElement)((XElement)node).Nodes().Where((n) =>
                             ((XElement)n).Name == "Coach").Single()).Value == surnameCoachTextBox
                             select node;
                }

                if (typeChecked)
                {
                    IEnumerable<XNode> temp = result == null ? xmlFile.Root.Nodes() : result;
                    result = from node in temp
                             where ((XElement)((XElement)node).Nodes().Where((n) =>
                             ((XElement)n).Name == "Type").Single()).Value == typeTextBox
                             select node;
                }

                if (dateChecked)
                {
                    IEnumerable<XNode> temp = result == null ? xmlFile.Root.Nodes() : result;
                    result = from node in temp
                             where ((XElement)((XElement)node).Nodes().Where((n) =>
                             ((XElement)n).Name == "Date").Single()).Value == datePicker
                             select node;
                }

                if (longChecked)
                {
                    IEnumerable<XNode> temp = result == null ? xmlFile.Root.Nodes() : result;
                    if (longComboBox == ">")
                    {
                        result = from node in temp
                                 where Convert.ToInt32(((XElement)((XElement)node).Nodes().Where((n) =>
                                 ((XElement)n).Name == "Long").Single()).Value) > Convert.ToInt32(longTextBox)
                                 select node;
                    }
                    else
                    {
                        result = from node in temp
                                 where Convert.ToInt32(((XElement)((XElement)node).Nodes().Where((n) =>
                                 ((XElement)n).Name == "Long").Single()).Value) < Convert.ToInt32(longTextBox)
                                 select node;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Выберите файл!");
            }

            return result;
        }

        /// <summary>
        ///     Удаление записи из файла
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string DeleteRowFromXMLFile(String id)
        {
            string strMessage = "Запись успешно удалена";
            XNode elemToDelete = null;
            try
            {
                elemToDelete = Controller.xmlFile.Root.Nodes().Where((node) =>
                ((XElement)node).LastAttribute.Value == id).Single();
                elemToDelete.Remove();
            }
            catch
            {
                strMessage = "Данной записи нет";
            }
            
            Controller.xmlFile.Save(Controller.xmlPathName);

            return strMessage;
        }

        /// <summary>
        ///     Очистка переменных
        /// </summary>
        public static void ClearXmlFileArg()
        {
            xmlFile = null;
            xmlPathName = "";
        }

        /// <summary>
        ///     Поиск записей
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static string SearchResult(
            bool a1, bool a2, bool a3, bool a4, bool a5,
            string b1, string b2, string b3, string b4, string b5, string b6)
        {
            string strMessage = "Записи найдены!";

            IEnumerable<XNode> result = searchRecords(a1, a2, a3, a4, a5, b1, b2, b3, b4, b5, b6);
            if (result.Count() == 0)
                strMessage = "Не найдены записи, соответствующие данным критериям.";
            return strMessage;
        }

        public static int CountRows(bool a1, bool a2, bool a3, bool a4, bool a5,
            string b1, string b2, string b3, string b4, string b5, string b6)
        {
            IEnumerable<XNode> result = searchRecords(a1, a2, a3, a4, a5, b1, b2, b3, b4, b5, b6);
            return result.Count();
        }
    }
}
