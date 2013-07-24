using System.Data;
using System.Linq;
using IssueTracker.API.Security;
using ServiceStack.OrmLite;

namespace IssueTracker.API.Entities.Seeding
{
    internal class UserSeeder
    {
        public static void Seed(IDbConnection db)
        {
            if (db.Select<UserEntity>().Any()) return;

            db.InsertParam(new UserEntity
            {
                Username = "admin",
                IsAdmin = true,
                IsMod = true,
                Password = PasswordHashingService.HashPassword("password")
            });
        }
    }
}