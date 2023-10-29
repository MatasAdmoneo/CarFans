using Cf.Contracts.Responses;
using Cf.Domain.Aggregates.Adverts;

namespace Cf.Contracts.Mappers;

public static class MapExtensions
{
    public static Response.Advert ToModel(this Advert model) =>
        new(model.Id);
}

