using System.Threading.Tasks;
using Convey.CQRS.Events;

namespace Pacco.Services.Vehicles.Application.Messaging
{
    public interface IMessageBroker
    {
        Task PublishAsync<T>(T @event) where T : class, IEvent;
    }
}