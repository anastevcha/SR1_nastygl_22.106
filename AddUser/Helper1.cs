using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddUser.Model;
using HashPasswords;
using System.Data.Entity;

namespace AddUser
{
    public class Helper1

    {

        public static atelieEntities s_atelieEntities;
        public static atelieEntities GetContext()
        {
            if (s_atelieEntities == null)
            {
                s_atelieEntities = new atelieEntities();
            }
            return s_atelieEntities;

        }
        public void CreateUsers(Avtorizacia avtorizacia)
        {
            s_atelieEntities.Avtorizacia.Add(avtorizacia);
            s_atelieEntities.SaveChanges();
        }
        public void CreateSotrudnik(Sotrudniki sotrudnik)
        {
            s_atelieEntities.Sotrudniki.Add(sotrudnik);
            s_atelieEntities.SaveChanges();
        }
        public void UpdateUsers(Avtorizacia avtorizacia)
        {
            s_atelieEntities.Entry(avtorizacia).State = EntityState.Modified;
            s_atelieEntities.SaveChanges();
        }
        public void UpdateSotrudnik(Sotrudniki sotrudniki)
        {
            s_atelieEntities.Entry(sotrudniki).State = EntityState.Modified;
            s_atelieEntities.SaveChanges();
        }
        public void RemoveUsers(int idAvtorizacia)
        {
            var avtorizacia = s_atelieEntities.Avtorizacia.Find(idAvtorizacia);
            s_atelieEntities.Avtorizacia.Remove(avtorizacia);
            s_atelieEntities.SaveChanges();
        }
        public void RemoveSotrudnik(int idSotrudnik)
        {
            var sotrudniki = s_atelieEntities.Sotrudniki.Find(idSotrudnik);
            s_atelieEntities.Sotrudniki.Remove(sotrudniki);
            s_atelieEntities.SaveChanges();
        }
        
    }
}

