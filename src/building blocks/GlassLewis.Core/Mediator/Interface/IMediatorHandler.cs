using FluentValidation.Results;
using GlassLewis.Core.Messages;
using System.Threading.Tasks;

namespace GlassLewis.Core.Mediator.Interface
{
    public interface IMediatorHandler
    {
        Task PublishEvent<T>(T evt) where T : Event;
        Task<ValidationResult> SendCommand<T>(T command) where T : Command;
    }
}
