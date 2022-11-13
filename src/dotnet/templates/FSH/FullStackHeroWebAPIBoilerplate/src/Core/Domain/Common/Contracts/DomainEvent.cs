using FullStackHeroWebAPIBoilerplate.Shared.Events;

namespace FullStackHeroWebAPIBoilerplate.Domain.Common.Contracts;

public abstract class DomainEvent : IEvent
{
    public DateTime TriggeredOn { get; protected set; } = DateTime.UtcNow;
}