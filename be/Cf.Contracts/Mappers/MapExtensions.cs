using Cf.Contracts.Responses;
using Cf.Domain.Aggregates.Adverts;
using Cf.Domain.Aggregates.Jobs;

namespace Cf.Contracts.Mappers;

public static class MapExtensions
{
    public static Response.AdvertId ToModel(this Advert model) =>
        new(model.Id);

    public static Response.JobId ToModel(this Job model) => 
        new(model.Id);
}

