namespace Gaspadorius.Auth;


public static class Roles
{
    public const string Admin = "Admin";
    public const string RegisteredUser = "RegisteredUser";
    public static List<string> AllRoles{ get; set; } = new List<string>(){"Admin", RegisteredUser};
}

public enum RoleEnum
{
    RegisteredUser = 1,
    Admin = 2
}

// public static class Roles
// {
//     public const string Admin = "Admin";
//     public const string RegisteredUser = "User";

//     public static IReadOnlyCollection<string> All = new[] { Admin, RegisteredUser };

// }