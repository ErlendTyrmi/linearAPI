using Database.LinearDatabase;
using linearAPI.Entities;
using System.Text.Json;


LinearUserRepo repo = new LinearUserRepo();

repo.create(new LinearUser("12345", "Adam Evasen", "adev@tvx.dk", true));
repo.create(new LinearUser("54321", "Eva Adamsen", "adev@bureau.net", false));

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
    Console.WriteLine($"Admin: {user.Admin}");
    Console.WriteLine();
}

