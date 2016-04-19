namespace YFit.Web.EF
{
    using System.Collections.Generic;
    using System.Data.Entity;

    public class EFInitializer : DropCreateDatabaseIfModelChanges<EfContext>
    {
        protected override void Seed(EfContext db)
        {
            var users = new List<User>()
            {
                new User()
                {
                    FirstName = "Sergey",
                    LastName = "Nestertsov",
                    Sex = UserSex.Male,
                    Role = UserRole.Admin,
                    Password = "1234",
                    Email = "n@gmail.com"
                },
            };

            users.ForEach(u => db.Set<User>().Add(u));
            db.SaveChanges();


            
            //db.SaveChanges();
        }
    }
}