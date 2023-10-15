namespace Gaspadorius.Models;

using Gaspadorius.Auth;
using Models.Dto;

public static class EntityExntentions
{
    public static PropertyDto AsDto(this Property property)
    {
        return new PropertyDto(
            property.Id,
            property.Title,
            property.Address,
            property.Size,
            property.UserId,
            property.FkCity
        );
    }
}