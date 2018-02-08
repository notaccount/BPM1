using BPM.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BPM.Repository.PowerManage
{
    public class PowerMenuRepository : Repository<Power_Menu>
    {
        DataContext _db;
        public PowerMenuRepository(DataContext db) :base (db)
        {
            _db = db;
        }
    }
}
