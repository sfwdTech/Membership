namespace Membership.Abstractions.Entities;
public class UserEntity
{
    public string FirstName { get; }
    public string LastName { get; }
    public string Email { get; }
    public string FullName => $"{FirstName} {LastName}";

    public UserEntity(string firstName, string lastName, string email)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }
}
