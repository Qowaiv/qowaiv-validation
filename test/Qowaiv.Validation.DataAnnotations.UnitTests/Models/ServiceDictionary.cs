namespace Qowaiv.Validation.DataAnnotations.UnitTests.Models;

internal sealed class ServiceDictionary : Dictionary<Type, object>, IServiceProvider
{
    public object GetService(Type serviceType) => this[serviceType];
}
