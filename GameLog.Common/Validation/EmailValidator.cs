namespace GameLog.Common.Validation;

public static class EmailValidator
{
    // this is an oversimplified example email validation
    public static bool IsValid(string email)
    {
        if (string.IsNullOrEmpty(email))
            return false;

        return email.Contains('@') && email.Contains('.');
    }
}