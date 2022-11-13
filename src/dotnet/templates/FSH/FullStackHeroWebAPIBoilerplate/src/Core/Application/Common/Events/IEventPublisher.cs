using FullStackHeroWebAPIBoilerplate.Shared.Events;

namespace FullStackHeroWebAPIBoilerplate.Application.Common.Events;

public interface IEventPublisher : ITransientService
{
    Task PublishAsync(IEvent @event);
}