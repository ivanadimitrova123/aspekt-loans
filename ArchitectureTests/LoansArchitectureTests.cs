namespace Aspekt.ArchitectureTests;

using Common;

public sealed class LoansArchitectureTests
{
    private const string Event = "Event";

    [Theory]
    [InlineData(Modules.Contacts)]
    internal void Loans_should_not_have_dependency_on_module(string moduleName)
    {
        // Arrange
        var loansModule = Solution.Types
            .That()
            .ResideInNamespace(Modules.Loans);

        var forbiddenModule = Solution.Types
            .That()
            .ResideInNamespace(moduleName);
        var forbiddenModuleTypes = forbiddenModule.GetModuleTypes();

        // Act
        var rules = loansModule
            .Should()
            .NotHaveDependencyOnAny(forbiddenModuleTypes);
        var validationResult = rules!.GetResult();

        // Assert
        validationResult.FailingTypes.Should().BeNull();
    }

    [Fact]
    internal void LoansShouldCommunicateWithApplicationsViaEvents()
    {
        // Arrange
        var loansModule = Solution.Types
            .That()
            .ResideInNamespace(Modules.Loans);

        var shouldModule = Solution.Types
            .That()
            .ResideInNamespace(Modules.Applications)
            .And()
            .DoNotHaveNameEndingWith(Event);
        var forbiddenModuleTypes = shouldModule.GetModuleTypes();

        // Act
        var rules = loansModule
            .Should()
            .NotHaveDependencyOnAny(forbiddenModuleTypes);
        var validationResult = rules!.GetResult();

        // Assert
        validationResult.FailingTypes.Should().BeNull();
    }
}
