using Qowaiv.Financial;
using Qowaiv.TestTools.Globalization;
using Qowaiv.Validation.DataAnnotations;

namespace Data_Annotations.Attributes.SetOf_values_specs;

public class Supports
{
    [Test]
    public void primitive_params_of_same_type_as_TValue()
        => new AllowedAttribute<int>(17, 42).Values
        .Should().BeEquivalentTo([17, 42]);

    [Test]
    public void pimitive_params_that_can_be_converted()
    {
        using (TestCultures.en_US.Scoped())
        {
            new AllowedAttribute<Amount>(42.12, "17.30").Values
                .Should().BeEquivalentTo([42.12.Amount(), 17.30.Amount()]);
        }
    }
}
