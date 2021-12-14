using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SumanChoraria_Sprint1.Models
{

    public class Context:DbContext
    {
     
        public Context(DbContextOptions<Context> context):base(context)
        {

        }

 
        public DbSet<UserModel> Users { get; set; }

      
        public DbSet<ProjectModel> Projects { get; set; }

       
        public DbSet<TaskModel> Tasks { get; set; }

        internal object FirstOrDefault(Func<object, bool> p)
        {
            throw new NotImplementedException();
        }
    }
}
