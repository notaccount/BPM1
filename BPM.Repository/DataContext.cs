using BPM.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BPM.Repository
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        #region SystemManage

        public DbSet<Power_Area> Power_Area { get; set; }
        public DbSet<Power_Menu> Power_Menu { get; set; }
        public DbSet<Power_MenuChilds> Power_MenuChilds { get; set; }
        public DbSet<Power_OrgRelation> Power_OrgRelation { get; set; }
        public DbSet<Power_Role> Power_Role { get; set; }
        public DbSet<Power_RoleMenu> Power_RoleMenu { get; set; }
        public DbSet<Power_Stations> Power_Stations { get; set; }
        public DbSet<Power_User> Power_User { get; set; }
        public DbSet<Power_UserOrg> Power_UserOrg { get; set; }
        public DbSet<Power_UserOrganization> Power_UserOrganization { get; set; }

        public DbSet<Power_UserRole> Power_UserRole { get; set; }
        #endregion

        #region WorkFlow

        public DbSet<FlowTemplate> FlowTemplate { get; set; }

        public DbSet<FlowAction> FlowAction { get; set; }

        public DbSet<FlowInstance> FlowInstance { get; set; }

        public DbSet<FlowStep> FlowStep { get; set; }

        public DbSet<InstanceDetail> InstanceDetail { get; set; } 
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
