public static class UserService
{
    private static Dictionary<string, string> users = new()
    {
        ["user@example.com"] = "oldpassword"
    };

    public static string? FindByEmail(string email)
    {
        return users.ContainsKey(email) ? email : null;
    }

    public static bool UpdatePassword(string email, string newPassword)
    {
        if (users.ContainsKey(email))
        {
            users[email] = newPassword;
            return true;
        }
        return false;
    }
}
