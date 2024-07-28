namespace Aspekt.ArchitectureTests;

using Common;

public sealed class ContactsArchitectureTests
{
    [Theory]
    [InlineData(Modules.Applications)]
    [InlineData(Modules.Loans)]
    internal void Contacts_should_not_have_dependency_on_module(string moduleName)
    {
        // Arrange
        var contactsModule = Solution.Types
            .That()
            .ResideInNamespace(Modules.Contacts);

        var forbiddenModule = Solution.Types
            .That()
            .ResideInNamespace(moduleName);
        var forbiddenModuleTypes = forbiddenModule.GetModuleTypes();

        // Act
        var rules = contactsModule
            .Should()
            .NotHaveDependencyOnAny(forbiddenModuleTypes);
        var validationResult = rules!.GetResult();

        // Assert
        validationResult.FailingTypes.Should().BeNull();
    }
}
