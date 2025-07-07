using Novus.API.Models;

namespace Novus.API.Service;

public interface IProducerService : IHostedService
{
    Task Send(List<Novusdata> novusdatalist);
    Task SetTopic(string topic);
}
