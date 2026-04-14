using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DAL_Celebrity_MSSQL
{
    public class Repository : IRepository
    {
        Context context;

        public Repository() { this.context = new Context(); }
        public Repository(string connectionstring) { this.context = new Context(connectionstring); }

        public static IRepository Create() { return new Repository(); }
        public static IRepository Create(string connectionstring) { return new Repository(connectionstring); }

        // --- Методы для Celebrities ---
        public List<Celebrity> GetAllCelebrities() { return this.context.Celebrities.ToList<Celebrity>(); }

        public Celebrity? GetCelebrityById(int id)
        {
            return this.context.Celebrities.FirstOrDefault(c => c.Id == id);
        }

        public bool AddCelebrity(Celebrity celebrity)
        {
            if (celebrity == null) return false;
            this.context.Celebrities.Add(celebrity);
            this.context.SaveChanges();
            return true;
        }

        public bool DelCelebrity(int id)
        {
            var celebrity = this.context.Celebrities.FirstOrDefault(c => c.Id == id);
            if (celebrity == null) return false;

            this.context.Celebrities.Remove(celebrity);
            this.context.SaveChanges();
            return true;
        }

        public bool UpdCelebrity(int id, Celebrity celebrity)
        {
            var existing = this.context.Celebrities.FirstOrDefault(c => c.Id == id);
            if (existing == null || celebrity == null) return false;

            existing.Update(celebrity); // Используем вспомогательный метод из класса
            this.context.SaveChanges();
            return true;
        }

        public int GetCelebrityIdByName(string name)
        {
            var celebrity = this.context.Celebrities.FirstOrDefault(c => c.FullName.Contains(name));
            return celebrity != null ? celebrity.Id : 0;
        }


        // --- Методы для Lifeevents ---
        public List<Lifeevent> GetAllLifeevents() { return this.context.Lifeevents.ToList<Lifeevent>(); }

        public Lifeevent? GetLifeevetById(int id)
        {
            return this.context.Lifeevents.FirstOrDefault(l => l.Id == id);
        }

        public bool AddLifeevent(Lifeevent lifeevent)
        {
            if (lifeevent == null) return false;
            this.context.Lifeevents.Add(lifeevent);
            this.context.SaveChanges();
            return true;
        }

        public bool DelLifeevent(int id)
        {
            var lifeevent = this.context.Lifeevents.FirstOrDefault(l => l.Id == id);
            if (lifeevent == null) return false;

            this.context.Lifeevents.Remove(lifeevent);
            this.context.SaveChanges();
            return true;
        }

        public bool UpdLifeevent(int id, Lifeevent lifeevent)
        {
            var existing = this.context.Lifeevents.FirstOrDefault(l => l.Id == id);
            if (existing == null || lifeevent == null) return false;

            existing.Update(lifeevent); // Используем вспомогательный метод из класса
            this.context.SaveChanges();
            return true;
        }

        public List<Lifeevent> GetLifeeventsByCelebrityId(int celebrityId)
        {
            return this.context.Lifeevents.Where(l => l.CelebrityId == celebrityId).ToList();
        }

        public Celebrity? GetCelebrityByLifeeventId(int lifeeventId)
        {
            var lifeEvent = this.context.Lifeevents.FirstOrDefault(l => l.Id == lifeeventId);
            if (lifeEvent != null)
            {
                return this.context.Celebrities.FirstOrDefault(c => c.Id == lifeEvent.CelebrityId);
            }
            return null;
        }

        public void Dispose()
        {
            this.context?.Dispose();
        }
    }
}