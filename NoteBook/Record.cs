using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteBook
{
    class Record
    {        
        public string Title { get; private set; }    // Загловок
        public string Writer { get; private set; }   // Автор записи
        public string Content { get; private set; }  // Конетнт записи
        public DateTime DateOfCreate { get; private set; }   // Дата создания
        public DateTime DateOfChanges { get; private set; }  // Дата изменения

        #region Constructors

        /// <summary>
        /// Конструктор Record
        /// </summary>
        /// <param name="Title"></param>
        /// <param name="Writer"></param>
        /// <param name="Content"></param>
        public Record(string Title, string Writer, string Content)
        {
            this.Title = Title;
            this.Writer = Writer;
            this.Content = Content;
            DateOfCreate = DateTime.Now;
            DateOfChanges = DateOfCreate;
        }
        
        /// <summary>
        /// Конструктор Record
        /// </summary>
        /// <param name="Title"></param>
        /// <param name="Writer"></param>
        /// <param name="Content"></param>
        /// <param name="DateOfCreate"></param>
        public Record(string Title, string Writer, string Content, DateTime DateOfCreate): this(Title, Writer, Content)
        {
            this.DateOfCreate = DateOfCreate;
            this.DateOfChanges = DateOfCreate;
        }

        /// <summary>
        /// Конструктор Record
        /// </summary>
        /// <param name="Title"></param>
        /// <param name="Writer"></param>
        /// <param name="Content"></param>
        /// <param name="DateOfCreate"></param>
        /// <param name="DateOfChanges"></param>
        public Record(string Title, string Writer, string Content, DateTime DateOfCreate, DateTime DateOfChanges) : this(Title, Writer, Content, DateOfCreate)
        {
            this.DateOfChanges = DateOfChanges;
        }

        #endregion

        /// <summary>
        /// Изменение записи
        /// </summary>
        /// <param name="Title"></param>
        /// <param name="Writer"></param>
        /// <param name="Content"></param>
        public void ChangeRecord(string Title, string Writer, string Content)
        {
            this.Title = Title;
            this.Writer = Writer;
            this.Content = Content;
            DateOfChanges = DateTime.Now;
        }
    }
}
