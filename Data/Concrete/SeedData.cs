using ForumProject.Data.Concrete;
using ForumProject.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BloggApp.Data.Concrete.EfCore
{
    public static class SeedData{

        public static async void TestVerileriniDoldur(IApplicationBuilder app){
            var context = app.ApplicationServices.CreateScope().ServiceProvider.GetService<ApplicationDbContext>();
            var _userManager = app.ApplicationServices.CreateScope().ServiceProvider.GetService<UserManager<User>>();
            var random = new Random();
            if(context != null){
                if(context.Database.GetPendingMigrations().Any()){
                    context.Database.Migrate();
                }
                if(!context.Forums.Any()){
                    context.Forums.AddRange(
                        new Forum {Title = "Python", Description = "A popular dynamic, strongly typed programming language", CreateTime = DateTime.Now,ImageUrl = "/images/forum/python.jpg"},
                        new Forum {Title = "C#", Description = "An object-oriented programming language for building applications on the .NET framwork", CreateTime = DateTime.Now,ImageUrl = "/images/forum/c#.jpg"},
                        new Forum {Title = "Haskell", Description = "A popular functional programming language",ImageUrl = "/images/forum/haskell.jpg"},
                        new Forum {Title = "Javascript", Description = "Multi-paradigm language based on the ECMAScript specification", CreateTime = DateTime.Now,ImageUrl = "/images/forum/js.jpg"},
                        new Forum {Title = "Go", Description = "Open source statically typed programming language", CreateTime = DateTime.Now,ImageUrl = "/images/forum/go.jpg"}
                    );
                    context.SaveChanges();
                }
                if(!context.Users.Any()){
                    await _userManager.CreateAsync(new User
                    {
                        CreateDate = DateTime.Now,
                        Email = "info@ahmet.com",
                        IsActive = true,
                        UserName = "ahmet",
                        ProfileImageUrl = "/images/user/python.jpg",
                        Rating = random.Next(1,5)                    
                    }, "Deneme123!");
                    await _userManager.CreateAsync(new User
                    {
                        CreateDate = DateTime.Now,
                        Email = "info@mozgur.com",
                        IsActive = true,
                        UserName = "deneme",
                        ProfileImageUrl = "/images/user/python.jpg",
                        Rating = random.Next(1,5)     
                    }, "Deneme123!");
                    await _userManager.CreateAsync(new User
                    {
                        CreateDate = DateTime.Now,
                        Email = "info@elif.com",
                        IsActive = true,
                        UserName = "elif",
                        ProfileImageUrl = "/images/user/python.jpg",
                        Rating = random.Next(1,5)     
                    }, "Deneme123!");
                    context.SaveChanges();
                }
                if(!context.Posts.Any()){
                    context.Posts.AddRange(
                        new Post {
                            Content = "First Python Post content",
                            CreateTime = DateTime.Now,
                            Title = "First Python post",
                            User = context.Users.Where(user => user.UserName == "deneme").First(),
                            Forum = context.Forums.Where(f => f.Id == 1).First()
                        },
                        new Post {
                            Content = "Content",
                            CreateTime = DateTime.Now,
                            Title = "Python Ml",
                            User = context.Users.Where(user => user.UserName == "deneme").First(),
                            Forum = context.Forums.Where(f => f.Id == 1).First()
                        },
                        new Post {
                            Content = "Snake game using python",
                            CreateTime = DateTime.Now,
                            Title = "Snake Game",
                            User = context.Users.Where(user => user.UserName == "deneme").First(),
                            Forum = context.Forums.Where(f => f.Id == 1).First()
                        },
                        new Post {
                            Content = "Pandas is a library used in data manipulation",
                            CreateTime = DateTime.Now,
                            Title = "How to use pandas",
                            User = context.Users.Where(user => user.UserName == "deneme").First(),
                            Forum = context.Forums.Where(f => f.Id == 1).First()
                        },
                        new Post {
                            Content = "I cant seem to call matplotlib",
                            CreateTime = DateTime.Now,
                            Title = "How to fix this bug???",
                            User = context.Users.Where(user => user.UserName == "deneme").First(),
                            Forum = context.Forums.Where(f => f.Id == 1).First()
                        }
                    );
                    context.SaveChanges();
                }
            }
        }
    }
}