namespace Qowaiv.Validation.Abstractions.UnitTests
{
    internal static class Arrange
    {
        public static readonly ValidationMessage Error1 = ValidationMessage.Error("Error 1");
        public static readonly ValidationMessage Error2 = ValidationMessage.Error("Error 2");
        public static readonly ValidationMessage Warning1 = ValidationMessage.Warn("Warning 1");
        public static readonly ValidationMessage Warning2 = ValidationMessage.Warn("Warning 2");
        public static readonly ValidationMessage Info1 = ValidationMessage.Info("Info 1");
        public static readonly ValidationMessage Info2 = ValidationMessage.Info("Info 2");
        public static readonly ValidationMessage[] TestMessages =
        {
            Error1, Error2, Warning1, Warning2, Info1, Info2
        };
    }
}
