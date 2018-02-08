using BPM.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BPM.Repository.PowerManage
{
    public class PowerUserRepository : Repository<Power_User> 
    {
        DataContext _db;
        public PowerUserRepository(DataContext db) :base (db)
        {
            _db = db;
        }
    }
}
