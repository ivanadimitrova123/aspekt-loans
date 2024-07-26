namespace Aspekt.ArchitectureTests;

using Common;

public sealed class ApplicationsArchitectureTests
{
    private const string Event = "Event";

    [Theory]
    [InlineData(Modules.Contacts)]
    [InlineData(Modules.Loans)]
    internal void Applications_should_not_have_dependency_on_module(string moduleName)
    {
        // Arrange
        var offersModule = Solution.Types
            .That()
            .ResideInNamespace(Modules.Applications);

        var forbiddenModule = Solution.Types
            .That()
            .ResideInNamespace(moduleName);
        var forbiddenModuleTypes = forbiddenModule.GetModuleTypes();

        // Act
        var rules = offersModule
            .Should()
            .NotHaveDependencyOnAny(forbiddenModuleTypes);
        var validationResult = rules!.GetResult();

        // Assert
        validationResult.FailingTypes.Should().BeNull();
    }

    [Fact]
    public void ApplicationsShouldCommunicateWithLoansViaEvents()
    {
        // Arrange
        var applicationsModule = Solution.Types
            .That()
            .ResideInNamespace(Modules.Applications);
        var shouldModule = Solution.Types
            .That()
            .ResideInNamespace(Modules.Loans)
            .And()
            .DoNotHaveNameEndingWith(Event);
        var forbiddenModuleTypes = shouldModule.GetModuleTypes();

        // Act
        var rules = applicationsModule
            .Should()
            .NotHaveDependencyOnAny(forbiddenModuleTypes);
        var validationResult = rules!.GetResult();

        // Assert
        validationResult.FailingTypes.Should().BeNull();
    }
}
