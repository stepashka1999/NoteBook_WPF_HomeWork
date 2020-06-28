using System;
using System.Collections.Generic;
using System.Xml;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteBook
{
    class Notebook
    {
        List<Record> records { get; set; }  // Список
        
        public int Length { get 
            {
                if (records != null)
                {
                    return records.Count;
                }
                return -1;
            } 
        }              // Свойство длинны списка

        #region Add/Change/Delete Record

        /// <summary>
        /// Удаляет запись из списка
        /// </summary>
        /// <param name="index"></param>
        public void Delete(int index)
        {
            if (records != null)
            {
                var rec = records[index];
                records.Remove(rec);
            }
        }

        /// <summary>
        /// Изменение записи
        /// </summary>
        /// <param name="index">Индекс изменяемой записи</param>
        /// <param name="Title">Заголовок</param>
        /// <param name="Writer">Автор</param>
        /// <param name="Content">Запись</param>
        public void ChangeRecord(int index, string Title, string Writer, string Content)
        {
            records[index].ChangeRecord(Title, Writer, Content);
        }

        /// <summary>
        /// Добавление записи в список
        /// </summary>
        /// <param name="rec">Запись</param>
        public void AddRecord(Record rec)
        {
            records.Add(rec);
        }

        /// <summary>
        /// Добавление записи в список
        /// </summary>
        /// <param name="Title">Заголовок записи</param>
        /// <param name="Writer">Автор записи</param>
        /// <param name="Content">Запись</param>
        public void AddRecord(string Title, string Writer, string Content)
        {
            var rec = new Record(Title, Writer, Content);
            AddRecord(rec);
        }

        #endregion

        #region Open/Save XML-File

        /// <summary>
        /// Открытие xml-документа
        /// </summary>
        /// <param name="path"></param>
        public void Open(string path, bool toThis = false)
        {
            ReadXmlFile(path, toThis); 
        }

        /// <summary>
        /// Сохранение в xml-документ
        /// </summary>
        /// <param name="path"></param>
        public void Save(string path)
        {
            WriteToXml(path);
        }

        /// <summary>
        /// Считывает данные из xml-файла
        /// </summary>
        /// <param name="path"></param>
        private void ReadXmlFile(string path, bool toThis = false)
        {
            if (!toThis) NewNoteBook();
            else if (records == null) NewNoteBook();

            var doc = new XmlDocument();
            doc.Load(path);

            var root = doc.DocumentElement;

            ReadToList(root);
        }
       
        /// <summary>
        /// Записывает считанные данные в список записей
        /// </summary>
        /// <param name="root">Корневой элемент xml-документа</param>
        private void ReadToList(XmlElement root)
        {
            foreach(var item in root.ChildNodes)
            {
                if(item is XmlElement node)
                {
                    var rec = new Record(node.ChildNodes[0].InnerText, node.ChildNodes[1].InnerText, node.ChildNodes[2].InnerText, DateTime.Parse(node.ChildNodes[3].InnerText), DateTime.Parse(node.ChildNodes[4].InnerText));
                    records.Add(rec);
                }

            }
        }

        /// <summary>
        /// Записывает список записей в xml-документ
        /// </summary>
        /// <param name="path">путь к файлу</param>
        private void WriteToXml(string path, IEnumerable<Record> records = null)
        {
            if (records == null) records = this.records;

            var doc = new XmlDocument();
            var xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.AppendChild(xmlDeclaration);

            var root = doc.CreateElement("NoteBook");

            foreach(var rec in records)
            {
                var record = doc.CreateElement("Record");

                AddChildNode(doc, record, "Title", rec.Title);
                AddChildNode(doc, record, "Writer", rec.Writer);
                AddChildNode(doc, record, "Content", rec.Content);
                AddChildNode(doc, record, "DateOfCreate", rec.DateOfCreate.ToString());
                AddChildNode(doc, record, "DateOfChanges", rec.DateOfChanges.ToString());

                root.AppendChild(record);
            }

            doc.AppendChild(root);
            doc.Save(path);

        }

        /// <summary>
        /// Создает и добавляет дочерний элемент к родительскому XmlElement
        /// </summary>
        /// <param name="doc">Документ</param>
        /// <param name="parent">Родительский элемент</param>
        /// <param name="childName">Дочерний элемент</param>
        /// <param name="childContent">Контент дочернего элемента</param>
        private void AddChildNode(XmlDocument doc,XmlElement parent, string childName, string childContent)
        {
            var child = doc.CreateElement(childName);
            child.InnerText = childContent;
            parent.AppendChild(child);
        }

        #endregion

        /// <summary>
        /// Создает новый список записей
        /// </summary>
        public void NewNoteBook()
        {
            records = new List<Record>();
        }

        /// <summary>
        /// Индексатор для Notebook
        /// </summary>
        /// <param name="index"></param>
        /// <returns>Запись по индексу</returns>
        public Record this[int index]
        {
            get
            {
                return records[index];
            }          
        }

        /// <summary>
        /// Sorting by params
        /// </summary>
        /// <param name="i"></param>
        public void SortBy(int i)
        {
            // 11, 12 - by Cr Date
            // 13, 14 - by Ch Date
            // 21, 22 - Content
            // 3 - Writer
            // 4 -Tittle

            switch(i)
            {
                case 11:
                    records.Sort((x, y) => x.DateOfCreate.CompareTo(y.DateOfCreate));
                    break;
                case 12:
                    records.Sort((x, y) => y.DateOfCreate.CompareTo(x.DateOfCreate));
                    break;
                case 13:
                    records.Sort((x, y) => x.DateOfChanges.CompareTo(y.DateOfChanges));
                    break;
                case 14:
                    records.Sort((x, y) => y.DateOfChanges.CompareTo(x.DateOfChanges));
                    break;
                case 21:
                    records.Sort((x, y) => x.Content.Length > y.Content.Length? 1: -1);
                    break;
                case 22:
                    records.Sort((x, y) => x.Content.Length > y.Content.Length ? -1 : 1);
                    break;
                case 3:
                    records.Sort((x, y) => x.Writer.CompareTo(y.Writer));
                    break;
                case 4:
                    records.Sort((x, y) => x.Title.CompareTo(y.Title));
                    break;
            }          
        }

        /// <summary>
        /// Импортирует в xml-файл данные из списка за определенный период 
        /// </summary>
        /// <param name="fromD"></param>
        /// <param name="toD"></param>
        /// <param name="path"></param>
        public int ImportByDate(DateTime fromD, DateTime toD, string path)
        {
            var importList = from item in records
                             where item.DateOfCreate > fromD && item.DateOfCreate < toD
                             select item;

            if (importList != null)
            {
                WriteToXml(path, importList);
                return 0;
            }
            
            return 1;
        }

    }
}
