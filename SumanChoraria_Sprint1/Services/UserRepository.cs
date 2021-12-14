using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SumanChoraria_Sprint1.Models.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class UserRepository : BaseRepository<UserModel>,IUserRepository
    {
        private readonly Context _context;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public UserRepository(Context context) : base(context)
        {
            _context = context;

            if (_context.Users.Count() == 0)
            {
                _context.Users.Add(new UserModel
                {
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john.doe@test.com",
                    Password = "Password1"
                });

                _context.SaveChanges();

                _context.Users.Add(new UserModel
                {
                    FirstName = "John",
                    LastName = "Skeet",
                    Email = "john.skeet@test.com",
                    Password = "Password1"
                });

                _context.SaveChanges();

                _context.Users.Add(new UserModel
                {
                    FirstName = "Mark",
                    LastName = "Seeman",
                    Email = "mark.seeman@test.com",
                    Password = "Password1"
                });

                _context.SaveChanges();

                _context.Users.Add(new UserModel
                {
                    FirstName = "Bob",
                    LastName = "Martin",
                    Email = "bob.martin@test.com",
                    Password = "Password1"
                });

                _context.SaveChanges();
            }
        }
    }
}
