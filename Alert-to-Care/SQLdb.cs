using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Alert_to_Care
{
    public class ICUContext : DbContext
    {
        public int id { get; set; }
        public int NumberOfBeds { get; set; }

        public Bed[] Beds { get; set; }

        public char Layout { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=ICU.db");
    }

    public class Bed
    {
        public string id { get; set; }
        public bool isOccupied { get; set; }
    }

    public class UserInput
    {
        public int NumberOfBeds { get; set; }
        public char Layout { get; set; }
    }
}