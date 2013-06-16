using IssueTracker.Data;
using ServiceStack.FluentValidation;
using ServiceStack.ServiceInterface;

namespace IssueTracker.API.Validators.Priorities
{
    public class PriorityValidator : AbstractValidator<Priority>
    {
        public PriorityValidator()
        {
            RuleSet(ApplyTo.Post | ApplyTo.Delete, () => RuleFor(r => r.Id).NotNull());
            RuleSet(ApplyTo.Put | ApplyTo.Post, () => RuleFor(r => r.Name).NotEmpty());
        }
    }
}