using System.Collections.ObjectModel;

namespace BlazorTest1.Services;

public record FamilyMember(string FirstName, string LastName, int Age);

public record AddMemberResult(bool Success, string? ErrorMessage = null)
{
    public static AddMemberResult Ok() => new(true, null);
    public static AddMemberResult Fail(string message) => new(false, message);
}

public class FamilyMembershipService
{
    private readonly List<FamilyMember> _members =
    [
        new("Mohamad", "Haji", 37),
        new("Shérine", "Haji", 9)
    ];

    public IReadOnlyList<FamilyMember> Members => new ReadOnlyCollection<FamilyMember>(_members);

    public AddMemberResult AddMember(FamilyMember member)
    {
        var validation = ValidateMember(member);
        if (!validation.Success)
        {
            return validation;
        }

        _members.Add(member);
        return AddMemberResult.Ok();
    }

    public bool RemoveMember(FamilyMember member) => _members.Remove(member);

    private static AddMemberResult ValidateMember(FamilyMember member)
    {
        if (string.IsNullOrWhiteSpace(member.FirstName))
        {
            return AddMemberResult.Fail("Le prénom est requis.");
        }

        if (string.IsNullOrWhiteSpace(member.LastName))
        {
            return AddMemberResult.Fail("Le nom de famille est requis.");
        }

        if (member.Age <= 0)
        {
            return AddMemberResult.Fail("L'âge doit être supérieur à 0.");
        }

        if (member.Age > 120)
        {
            return AddMemberResult.Fail("L'âge indiqué est trop élevé.");
        }

        return AddMemberResult.Ok();
    }
}
