namespace MiniValidation2;

internal sealed record TypeDetails(PropertyDetails[] Properties, bool RequiresAsync)
{
    public static readonly TypeDetails Empty = new([], false);
}
