using ForumProject.Data.Concrete;
using ForumProject.Entity;
using Microsoft.EntityFrameworkCore;

namespace BloggApp.Data.Concrete.EfCore
{
    public static class SeedData{

        public static void TestVerileriniDoldur(IApplicationBuilder app){
            var context = app.ApplicationServices.CreateScope().ServiceProvider.GetService<ApplicationDbContext>();

            if(context != null){
                if(context.Database.GetPendingMigrations().Any()){
                    context.Database.Migrate();
                }
                if(!context.Forums.Any()){
                    context.Forums.AddRange(
                        new Forum {Title = "Python", Description = "A popular dynamic, strongly typed programming language", CreateTime = DateTime.Now,ImageUrl = "~/images/forum/py.jpg"},
                        new Forum {Title = "C#", Description = "An object-oriented programming language for building applications on the .NET framwork", CreateTime = DateTime.Now,ImageUrl = "~/images/forum/c#.jpg"},
                        new Forum {Title = "Haskell", Description = "A popular functional programming language",ImageUrl = "~/images/forum/haskell.jpg"},
                        new Forum {Title = "Javascript", Description = "Multi-paradigm language based on the ECMAScript specification", CreateTime = DateTime.Now,ImageUrl = "~/images/forum/js.jpg"},
                        new Forum {Title = "Go", Description = "Open source statically typed programming language", CreateTime = DateTime.Now,ImageUrl = "~/images/forum/go.jpg"}
                    );
                    context.SaveChanges();
                }
            }
        }
    }
}