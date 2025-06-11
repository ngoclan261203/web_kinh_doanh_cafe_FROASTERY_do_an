// Services/VerificationService.cs

public class VerificationService
{
    private readonly Dictionary<string, (string Code, DateTime Expiry)> _codes = new();

    public void SetCode(string email, string code)
    {
        _codes[email] = (code, DateTime.Now.AddMinutes(10));
    }

    public bool VerifyCode(string email, string code)
    {
        if (_codes.TryGetValue(email, out var entry))
        {
            if (entry.Expiry > DateTime.Now && entry.Code == code)
            {
                _codes.Remove(email);
                return true;
            }
        }
        return false;
    }
}
