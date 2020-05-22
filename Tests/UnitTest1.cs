using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml.Linq;
using Lab8;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Assert.AreEqual(
                    "Выбранный файл: C:\\Users\\Admin\\Desktop\\MyData.xml", 
                    Controller.CheckChooseFile("C:\\Users\\Admin\\Desktop\\MyData.xml")
            );
        }

        [TestMethod]
        public void TestMethod2()
        {
            Assert.AreEqual(
                    "Неверный формат файла",
                    Controller.CheckChooseFile("C:\\Users\\Admin\\Desktop\\MyData.txt")
            );
        }

        [TestMethod]
        public void TestMethod3()
        {
            Assert.AreEqual(
                    "Заполните все поля!",
                    Controller.AddRowToXMLFile(
                        "Jhames",
                        "645",
                        "Baseball",
                        "Plant",
                        "21 мая 2020 г.",
                        "18:00",
                        "",
                        "25"
            ));
        }

        [TestMethod]
        public void TestMethod4()
        {
            Controller.ClearXmlFileArg();
            Assert.AreEqual(
                    "Выберите файл!",
                    Controller.AddRowToXMLFile(
                        "Jhames",
                        "645",
                        "Baseball",
                        "Plant",
                        "21 мая 2020 г.",
                        "18:00",
                        "45",
                        "25"
            ));
        }

        [TestMethod]
        public void TestMethod5()
        {
            Controller.CheckChooseFile("C:\\Users\\Admin\\Desktop\\MyData.xml");
            Assert.AreEqual(
                    "Запись была успешно внесена!",
                    Controller.AddRowToXMLFile(
                        "Jhames",
                        "645",
                        "Baseball",
                        "Plant",
                        "21 мая 2020 г.",
                        "18:00",
                        "45",
                        "25"
            ));
        }

        [TestMethod]
        public void TestMethod6()
        {
            Controller.CheckChooseFile("C:\\Users\\Admin\\Desktop\\MyData1.xml");
            Controller.AddRowToXMLFile(
                        "Jhames",
                        "645",
                        "Baseball",
                        "Plant",
                        "21 мая 2020 г.",
                        "18:00",
                        "45",
                        "25"
            );
            Assert.AreEqual(
                        "Запись успешно удалена",
                        Controller.DeleteRowFromXMLFile(Controller.lastId.ToString())
            );
        }

        [TestMethod]
        public void TestMethod7()
        {
            Controller.CheckChooseFile("C:\\Users\\Admin\\Desktop\\MyData1.xml");
            Assert.AreEqual(
                        "Данной записи нет",
                        Controller.DeleteRowFromXMLFile("1")
            );
        }

        [TestMethod]
        public void TestMethod8()
        {
            Controller.CheckChooseFile("C:\\Users\\Admin\\Desktop\\MyData1.xml");
            Assert.AreEqual(
                        "Не найдены записи, соответствующие данным критериям.",
                        Controller.SearchResult(
                            true, false, false, false, false,
                            "Belyakov", "", "", "", "", ""
                        )
            );
        }

        [TestMethod]
        public void TestMethod9()
        {
            Controller.CheckChooseFile("C:\\Users\\Admin\\Desktop\\MyData1.xml");
            Assert.AreEqual(
                        "Записи найдены!",
                        Controller.SearchResult(
                            true, false, false, false, false,
                            "Bell", "", "", "", "", ""
                        )
            );
        }

        [TestMethod]
        public void TestMethod10()
        {
            Controller.CheckChooseFile("C:\\Users\\Admin\\Desktop\\MyData2.xml");
            Assert.AreEqual(
                        3,
                        Controller.CountRows(
                            true, false, false, false, false,
                            "Pitt", "", "", "", "", ""
                        )
            );
            Assert.AreEqual(
                        2,
                        Controller.CountRows(
                            true, false, false, false, false,
                            "Bell", "", "", "", "", ""
                        )
            );
            Assert.AreEqual(
                        4,
                        Controller.CountRows(
                            true, false, false, false, false,
                            "Jhames", "", "", "", "", ""
                        )
            );
            Assert.AreEqual(
                        1,
                        Controller.CountRows(
                            true, false, true, false, false,
                            "Pitt", "", "Baseball", "", "", ""
                        )
            );
            Assert.AreEqual(
                        0,
                        Controller.CountRows(
                            true, false, true, false, false,
                            "Pit44t", "", "Baseball", "", "", ""
                        )
            );
        }
    }
}
