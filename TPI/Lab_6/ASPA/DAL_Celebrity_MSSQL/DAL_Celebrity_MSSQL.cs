using System;

namespace DAL_Celebrity_MSSQL
{
    // Интерфейс, связывающий обобщенный интерфейс из DAL_Celebrity с конкретными классами
    public interface IRepository : DAL_Celebrity.IRepository<Celebrity, Lifeevent> { }

    public class Celebrity // Знаменитость
    {
        public Celebrity() { this.FullName = string.Empty; this.Nationality = string.Empty; }
        public int Id { get; set; }                      // Id Знаменитости
        public string FullName { get; set; }             // полное имя Знаменитости
        public string Nationality { get; set; }          // гражданство Знаменитости ( 2 символа ISO )
        public string? ReqPhotoPath { get; set; }        // request path Фотографии

        public virtual bool Update(Celebrity celebrity)  // --вспомогательный метод
        {
            if (celebrity == null) return false;
            this.FullName = celebrity.FullName;
            this.Nationality = celebrity.Nationality;
            this.ReqPhotoPath = celebrity.ReqPhotoPath;
            return true;
        }
    }

    public class Lifeevent // Событие в жизни знаменитости
    {
        public Lifeevent() { this.Description = string.Empty; }
        public int Id { get; set; }                      // Id События
        public int CelebrityId { get; set; }             // Id Знаменитости
        public DateTime? Date { get; set; }              // дата События
        public string Description { get; set; }          // описание События
        public string? ReqPhotoPath { get; set; }        // request path Фотографии

        public virtual bool Update(Lifeevent lifeevent)  // -- вспомогательный метод
        {
            if (lifeevent == null) return false;
            this.CelebrityId = lifeevent.CelebrityId;
            this.Date = lifeevent.Date;
            this.Description = lifeevent.Description;
            this.ReqPhotoPath = lifeevent.ReqPhotoPath;
            return true;
        }
    }
}