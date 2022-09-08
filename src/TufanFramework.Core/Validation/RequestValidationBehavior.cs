using FluentValidation;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TufanFramework.Core.Validation
{
    //public class RequestValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    //{
    //    private readonly IEnumerable<IValidator<TRequest>> _validators;

    //    public RequestValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    //    {
    //        _validators = validators;
    //    }

    //    public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    //    {
    //        var errors = _validators
    //            .Select(v => v.Validate(request))
    //            .SelectMany(result => result.Errors)
    //            .Where(error => error != null)
    //            .ToList();

    //        if (errors.Any())
    //            throw new ValidationException("Invalid command, reason: ", errors);
            
    //        return next();
    //    }
    //}
}