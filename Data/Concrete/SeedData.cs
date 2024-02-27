using System.Security.Claims;
using ForumProject.Data.Concrete;
using ForumProject.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BloggApp.Data.Concrete.EfCore
{
    public static class SeedData
    {

        public static async void TestVerileriniDoldur(IApplicationBuilder app)
        {
            var random = new Random();
            var context = app.ApplicationServices.CreateScope().ServiceProvider.GetService<ApplicationDbContext>();
            var _userManager = app.ApplicationServices.CreateScope().ServiceProvider.GetService<UserManager<User>>();

            if (context != null)
            {
                if (context.Database.GetPendingMigrations().Any())
                {
                    context.Database.Migrate();
                }
                if (!context.Forums.Any())
                {
                    context.Forums.AddRange(
                        new Forum { Title = "Python", Description = "A popular dynamic, strongly typed programming language", CreateTime = DateTime.Now, ImageUrl = "/images/forum/python.jpg" },
                        new Forum { Title = "C#", Description = "An object-oriented programming language for building applications on the .NET framwork", CreateTime = DateTime.Now, ImageUrl = "/images/forum/c#.jpg" },
                        new Forum { Title = "Haskell", Description = "A popular functional programming language", ImageUrl = "/images/forum/haskell.jpg" },
                        new Forum { Title = "Javascript", Description = "Multi-paradigm language based on the ECMAScript specification", CreateTime = DateTime.Now, ImageUrl = "/images/forum/js.jpg" },
                        new Forum { Title = "Unity", Description = "Open source statically typed programming language", CreateTime = DateTime.Now, ImageUrl = "/images/forum/go.jpg" },
                        new Forum { Title = "Java", Description = "An object-oriented programming language for building applications on the Java ecosystem", CreateTime = DateTime.Now, ImageUrl = "/images/forum/go.jpg" },
                        new Forum { Title = "Mathemathics", Description = "The world of Mathemathics", CreateTime = DateTime.Now, ImageUrl = "/images/forum/go.jpg" },
                        new Forum { Title = "Pyhsics", Description = "The world of Pyhsics", CreateTime = DateTime.Now, ImageUrl = "/images/forum/go.jpg" },
                        new Forum { Title = "Biology", Description = "The world of Biology", CreateTime = DateTime.Now, ImageUrl = "/images/forum/go.jpg" },
                        new Forum { Title = "Chemistry", Description = "The world of Chemistry", CreateTime = DateTime.Now, ImageUrl = "/images/forum/go.jpg" },
                        new Forum { Title = "Fitness and Exercise", Description = "Share fitness tips, find mental health support, discuss nutrition, and explore holistic approaches to well-being.", CreateTime = DateTime.Now, ImageUrl = "/images/forum/go.jpg" },
                        new Forum { Title = " Tech News", Description = "Explore the latest in gadgets, discuss programming challenges, and get support for troubleshooting tech issues.", CreateTime = DateTime.Now, ImageUrl = "/images/forum/go.jpg" },
                        new Forum { Title = "Mental Health", Description = "Open source statically typed programming language", CreateTime = DateTime.Now, ImageUrl = "/images/forum/go.jpg" },
                        new Forum { Title = "Photography and Videography", Description = "Connect with enthusiasts in photography, gaming, DIY projects, and various hobbies for advice and inspiration.", CreateTime = DateTime.Now, ImageUrl = "/images/forum/go.jpg" },
                        new Forum { Title = "Gaming and eSports", Description = "Connect with enthusiasts in photography, gaming, DIY projects, and various hobbies for advice and inspiration.", CreateTime = DateTime.Now, ImageUrl = "/images/forum/go.jpg" }
                    );
                    context.SaveChanges();
                }
                if (!context.Users.Any())
                {
                    await _userManager.CreateAsync(new User
                    {
                        CreateDate = DateTime.Now,
                        Email = "info@ahmet.com",
                        IsActive = true,
                        UserName = "ForumAdmin",
                        ProfileImageUrl = "/images/user/1.jpg",
                        Rating = random.Next(1, 5),
                    }, "Deneme123!");
                    await _userManager.CreateAsync(new User
                    {
                        CreateDate = DateTime.Now,
                        Email = "info@ahmet.com",
                        IsActive = true,
                        UserName = "ahmet",
                        ProfileImageUrl = "/images/user/2.jpg",
                        Rating = random.Next(1, 5)
                    }, "Deneme123!");
                    await _userManager.CreateAsync(new User
                    {
                        CreateDate = DateTime.Now,
                        Email = "info@mozgur.com",
                        IsActive = true,
                        UserName = "mozgur",
                        ProfileImageUrl = "/images/user/3.jpg",
                        Rating = random.Next(1, 5)
                    }, "Deneme123!");
                    await _userManager.CreateAsync(new User
                    {
                        CreateDate = DateTime.Now,
                        Email = "info@elif.com",
                        IsActive = true,
                        UserName = "elif",
                        ProfileImageUrl = "/images/user/4.jpg",
                        Rating = random.Next(1, 5)
                    }, "Deneme123!");
                    await _userManager.CreateAsync(new User
                    {
                        CreateDate = DateTime.Now,
                        Email = "info@hasan.com",
                        IsActive = true,
                        UserName = "hasan",
                        ProfileImageUrl = "/images/user/5.jpg",
                        Rating = random.Next(1, 5)
                    }, "Deneme123!");
                    await _userManager.CreateAsync(new User
                    {
                        CreateDate = DateTime.Now,
                        Email = "info@cemre.com",
                        IsActive = true,
                        UserName = "cemre",
                        ProfileImageUrl = "/images/user/6.jpg",
                        Rating = random.Next(1, 5)
                    }, "Deneme123!");
                    await _userManager.CreateAsync(new User
                    {
                        CreateDate = DateTime.Now,
                        Email = "info@abdullah.com",
                        IsActive = true,
                        UserName = "abdullah",
                        ProfileImageUrl = "/images/user/5.jpg",
                        Rating = random.Next(1, 5)
                    }, "Deneme123!");
                    await _userManager.CreateAsync(new User
                    {
                        CreateDate = DateTime.Now,
                        Email = "info@omer.com",
                        IsActive = true,
                        UserName = "omer",
                        ProfileImageUrl = "/images/user/3.jpg",
                        Rating = random.Next(1, 5)
                    }, "Deneme123!");
                    var admin = await _userManager.FindByNameAsync("ForumAdmin");
                    await _userManager.AddClaimAsync(admin, new Claim(ClaimTypes.Role, "Admin"));
                    context.SaveChanges();
                }
                if (!context.Posts.Any())
                {
                    context.Posts.AddRange(
                        new Post
                        {
                            Content = "First Python Post content",
                            CreateTime = DateTime.Now,
                            Title = "First Python post",
                            User = context.Users.Where(user => user.UserName == "ahmet").First(),
                            Forum = context.Forums.Where(f => f.Id == 1).First()
                        },
                        new Post
                        {
                            Content = "Content",
                            CreateTime = DateTime.Now,
                            Title = "Python Ml",
                            User = context.Users.Where(user => user.UserName == "omer").First(),
                            Forum = context.Forums.Where(f => f.Id == 1).First()
                        },
                        new Post
                        {
                            Content = "Snake game using python",
                            CreateTime = DateTime.Now,
                            Title = "Snake Game",
                            User = context.Users.Where(user => user.UserName == "elif").First(),
                            Forum = context.Forums.Where(f => f.Id == 1).First()
                        },
                        new Post
                        {
                            Content = "Pandas is a library used in data manipulation",
                            CreateTime = DateTime.Now,
                            Title = "How to use pandas",
                            User = context.Users.Where(user => user.UserName == "mozgur").First(),
                            Forum = context.Forums.Where(f => f.Id == 1).First()
                        },
                        new Post
                        {
                            Content = "I cant seem to call matplotlib",
                            CreateTime = DateTime.Now,
                            Title = "How to fix this bug???",
                            User = context.Users.Where(user => user.UserName == "ForumAdmin").First(),
                            Forum = context.Forums.Where(f => f.Id == 1).First()
                        },
                        new Post
                        {
                            Content = "Hello everyone I can't seem to use ViewBag whatever I try can you help me",
                            CreateTime = DateTime.Now,
                            Title = "How can I use ViewBag",
                            User = context.Users.Where(user => user.UserName == "mozgur").First(),
                            Forum = context.Forums.Where(f => f.Id == 2).First()
                        },
                        new Post
                        {
                            Content = "Hey There. Can you rate my application.",
                            CreateTime = DateTime.Now,
                            Title = "I made a forum application",
                            User = context.Users.Where(user => user.UserName == "ForumAdmin").First(),
                            Forum = context.Forums.Where(f => f.Id == 2).First()
                        }
                        
                    );
                    context.SaveChanges();
                }
                if (!context.PostReplies.Any())
                    {
                        context.PostReplies.AddRange(
                            new PostReply
                            {
                                Post = context.Posts.Where(post => post.Id == 1).First(),
                                User = context.Users.Where(user => user.UserName == "mozgur").First(),
                                CreateTime = DateTime.Now,
                                Content = " Tebrikler calisiyor."
                            },
                            new PostReply
                            {
                                Post = context.Posts.Where(post => post.Id == 3).First(),
                                User = context.Users.Where(user => user.UserName == "omer").First(),
                                CreateTime = DateTime.Now,
                                Content = "Hangi libraryleri kullandin.Ben de bir ara tetris yapmayi denemistim."
                            },
                            new PostReply
                            {
                                Post = context.Posts.Where(post => post.Id == 5).First(),
                                User = context.Users.Where(user => user.UserName == "ForumAdmin").First(),
                                CreateTime = DateTime.Now,
                                Content = " pip install matplotlib sonra import matplotlib as plt"
                            }
                        );
                        context.SaveChanges();
                    }
            }
        }
    }
}