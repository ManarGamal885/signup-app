namespace Domain.ValueObjects;

public class Email
{
    public Guid Id { get; private set; }
    public string Value { get; private set; }

    private Email(string value)
    {
        Value = value;
    }

    public static Email Create(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentException("Email cannot be empty");
        }

        if (!IsValidEmail(email))
        {
            throw new ArgumentException("Invalid email format");
        }

        return new Email(email);
    }
    
    //Validate the Email format.
    private static bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }

}