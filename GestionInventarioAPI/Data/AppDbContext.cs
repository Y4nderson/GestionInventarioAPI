﻿using Microsoft.EntityFrameworkCore;

namespace GestionInventarioAPI.Data
{
    public class AppDbContext:DbContext
    {


        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            




        }
    }
}
