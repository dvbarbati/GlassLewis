using FluentValidation.Results;
using GlassLewis.Core.Data.Interfaces;
using System.Threading.Tasks;

namespace GlassLewis.Core.Messages
{
    public abstract class CommandHandler
    {
        protected ValidationResult ValidationResult;

        protected CommandHandler()
        {
            ValidationResult = new ValidationResult();
        }

        protected void AddError(string message)
        {
            ValidationResult.Errors.Add(new ValidationFailure(string.Empty, message));
        }

        protected async Task<ValidationResult> PersistData(IUnitOfWork uow)
        {
            if (!await uow.Commit())
                AddError("There was an error persisting the data.");

            return ValidationResult;
        }
    }
}
