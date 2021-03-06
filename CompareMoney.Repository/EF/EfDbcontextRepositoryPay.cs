﻿using CompareMoney.Core.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace CompareMoney.Repository.Base
{
    public class EfDbcontextRepositoryPay : DbContext
    {
        public EfDbcontextRepositoryPay()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();
            var sqlType = config["PaySql:sqlType"];
            var sqlstr = config["PaySql:str"];
            if (sqlType == "1")
            {
                optionsBuilder.UseSqlServer(sqlstr, b => b.UseRowNumberForPaging());

                return;
            }
            else if (sqlType == "2")
            {
                optionsBuilder.UseOracle(sqlstr);

                return;
            }
            else if (sqlType == "3")
            {
                optionsBuilder.UseMySQL(sqlstr);

                return;
            }

        }

        public DbSet<FXStmtLine> FXStmtLine { get; set; }  //pay 的数据
        public DbSet<PayTable> PayTable { get; set; }      //pay 的数据
        public DbSet<User> User { get; set; }               //pay 的数据
    }
}
