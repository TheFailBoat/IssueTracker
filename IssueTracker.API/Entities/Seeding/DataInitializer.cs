using IssueTracker.API.Entities;
using ServiceStack.OrmLite;

namespace IssueTracker.API.Seeding
{
    internal class DataInitializer
    {
        public static void CreateTables(IDbConnectionFactory dbFactory)
        {
            dbFactory.Run(db =>
            {
                db.CreateTable<CustomerEntity>(overwrite: false);

                db.CreateTable<UserEntity>(overwrite: false);
                db.CreateTable<AuthTokenEntity>(overwrite: false);

                db.CreateTable<CategoryEntity>(overwrite: false);
                db.CreateTable<StatusEntity>(overwrite: false);
                db.CreateTable<PriorityEntity>(overwrite: false);
                db.CreateTable<IssueEntity>(overwrite: false);

                db.CreateTable<CommentEntity>(overwrite: false);
                db.CreateTable<CommentChangeEntity>(overwrite: false);
            });
        }

        public static void SeedTables(IDbConnectionFactory dbFactory)
        {
            dbFactory.Run(db =>
            {
                CategorySeeder.Seed(db);
                PrioritySeeder.Seed(db);
                StatusSeeder.Seed(db);
            });
        }
    }
}