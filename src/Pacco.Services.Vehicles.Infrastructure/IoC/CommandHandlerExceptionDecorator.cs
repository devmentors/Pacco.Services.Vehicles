using System.Threading.Tasks;
using Convey.CQRS.Commands;
using Convey.Types;
using Pacco.Services.Vehicles.Core.Exceptions;

namespace Pacco.Services.Vehicles.Infrastructure.IoC
{
    public class CommandHandlerExceptionDecorator<TCommand> : ICommandHandler<TCommand> where TCommand : class, ICommand
    {
        private ICommandHandler<TCommand> _handler { get; }
        
        public CommandHandlerExceptionDecorator(ICommandHandler<TCommand> handler)
            => _handler = handler;
        
        public async Task HandleAsync(TCommand command)
        {
            try
            {
                await _handler.HandleAsync(command);
            }
            catch (ExceptionBase ex)
            {
                throw new ConveyException(ex, ex.Code, ex.Message);
            }
        }
    }
}