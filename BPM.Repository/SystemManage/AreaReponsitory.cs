using BPM.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BPM.Repository.SystemManage
{
   public  class AreaReponsitory : Repository<Power_Area>
    {
        DataContext _db;
        public AreaReponsitory(DataContext db) : base(db)
        {
            _db = db;
        }
    }
}
