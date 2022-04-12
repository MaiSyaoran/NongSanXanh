using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NongSanXanh.Models;
using PagedList;

namespace Model.DAO
{
    public class UserDao
    {
        NongSanXanhDbContext db = null;
        public UserDao()
        {
            db = new NongSanXanhDbContext();
        }


        public long InsertForFacebook(Muser entity)
        {
            var user = db.users.SingleOrDefault(x => x.username == entity.username);
            if (user == null)
            {
                db.users.Add(entity);
                db.SaveChanges();
                return entity.ID;
            }
            else
            {
                return user.ID;
            }

        }

    
    }
}
