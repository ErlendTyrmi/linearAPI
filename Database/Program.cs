using Database.Entities;
using Database.LinearDatabase;
using linearAPI.Entities;
using System.Text.Json;


LinearRepo<LinearUser> repo = new LinearRepo<LinearUser>();

repo.Create(new LinearUser("12345", "Adam Adminsen", "aadm@tvx.dk", true));
repo.Create(new LinearUser("54321", "Eva deBureau", "evad@bureau.net", false));

Console.WriteLine("Individual getters:");
printUser(repo.Read("12345"));
printUser(repo.Read("54321"));
Console.WriteLine("*********************");


var userList = repo.ReadAll();

if (userList != null) {
    foreach (var _user in userList)
    {
        printUser(_user);
    }
}

void printUser(LinearUser user){
    if (user == null) return;

    Console.WriteLine($"ID:{user.Id}");
    Console.WriteLine($"Name: {user.Name}");
    Console.WriteLine($"Mail: {user.Email}");
    Console.WriteLine($"Admin: {user.IsAdmin}");
    Console.WriteLine();
}

