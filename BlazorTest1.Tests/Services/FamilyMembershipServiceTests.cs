using BlazorTest1.Services;
using Xunit;

namespace BlazorTest1.Tests.Services;

public class FamilyMembershipServiceTests
{
    [Fact]
    public void AddMember_WithValidData_AddsMember()
    {
        // Arrange
        var service = new FamilyMembershipService();
        var initialCount = service.Members.Count;
        var newMember = new FamilyMember("Amira", "Diaz", 28);

        // Act
        var result = service.AddMember(newMember);

        // Assert
        Assert.True(result.Success);
        Assert.Null(result.ErrorMessage);
        Assert.Equal(initialCount + 1, service.Members.Count);
        Assert.Contains(service.Members, m => m.FirstName == "Amira" && m.LastName == "Diaz" && m.Age == 28);
    }

    [Fact]
    public void AddMember_WithMissingFirstName_ReturnsError()
    {
        // Arrange
        var service = new FamilyMembershipService();
        var initialCount = service.Members.Count;
        var newMember = new FamilyMember(string.Empty, "Martin", 25);

        // Act
        var result = service.AddMember(newMember);

        // Assert
        Assert.False(result.Success);
        Assert.NotNull(result.ErrorMessage);
        Assert.Equal(initialCount, service.Members.Count);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-2)]
    [InlineData(150)]
    public void AddMember_WithInvalidAge_ReturnsError(int age)
    {
        // Arrange
        var service = new FamilyMembershipService();
        var initialCount = service.Members.Count;
        var newMember = new FamilyMember("Léa", "Dupuis", age);

        // Act
        var result = service.AddMember(newMember);

        // Assert
        Assert.False(result.Success);
        Assert.NotNull(result.ErrorMessage);
        Assert.Equal(initialCount, service.Members.Count);
    }
}
