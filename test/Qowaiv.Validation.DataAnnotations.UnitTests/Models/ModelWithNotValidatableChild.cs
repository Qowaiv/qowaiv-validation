using System.IO;

namespace Qowaiv.Validation.DataAnnotations.UnitTests.Models;

public class ModelWithNotValidatableChild
{
    [Mandatory]
    public MemoryStream Stream { get; set; }
}
