using System;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Text;
using Azure.Identity;
using ChessEntity;
using ChessWeb;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ChessContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

Console.WriteLine("Runned");

//Container cnt1 = new Container();
//cnt1.MainContainer.Append(new NameMovePair("Adam", "a1a1"));
//cnt1.MainContainer.Append(new NameMovePair("Adam1", "a1a1"));

//Container.AddToContainer(new NameMovePair("Adam", "h7h5"));
//Container.AddToContainer(new NameMovePair("Adam2", "h7h5"));

//var contextOptions = new DbContextOptionsBuilder<ChessContext>()
//    .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
//    .Options;

//using var context = new ChessContext(contextOptions);
//User testUser01 = new User { UserName = "test01", Password = "123", Salt = "abc12" };
//context.Users.Add(testUser01);
//context.SaveChanges();

//PasswordHasherTest.GetHashTest();
//string[] saltHashPair = new string[2];


//Console.WriteLine(PasswordHasherMD5.GetHashMd5("domina"));
//Console.WriteLine(PasswordHasherMD5.GetHashMd5("domina"));
//Console.WriteLine(PasswordHasherMD5.GetHashMd5("domina1"));

string[] saltHashPair = PasswordHasher.CreateHash("chash01");
Console.WriteLine("Genered salt: " + saltHashPair[0]);
Console.WriteLine("Hashed pass: " + saltHashPair[1]);

//Genered salt: QvYa9rR77jjSnP5P + LFAZg ==
//Hashed pass: MXIGYsNQN0IgMyzJiY9BbVOgpolR7GqPnYlg8FVXSnk =

string[] saltHashPair2 = PasswordHasher.CheckHash("chash01", saltHashPair[0]);
Console.WriteLine("Genered salt: " + saltHashPair2[0]);
Console.WriteLine("Hashed pass: " + saltHashPair2[1]);
Console.WriteLine("MXIGYsNQN0IgMyzJiY9BbVOgpolR7GqPnYlg8FVXSnk");


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();


app.MapControllers();


app.Run();



