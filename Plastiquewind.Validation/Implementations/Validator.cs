using System.Collections.Generic;
using System.Linq;
using Plastiquewind.Base.Abstractions;
using Plastiquewind.Base.Abstractions.Errors;
using Plastiquewind.Base.Implementations;
using Plastiquewind.Validation.Abstractions;
using PommaLabs.Thrower;

namespace Plastiquewind.Validation.Implementations
{
    public class Validator<T> : IValidator<T>
    {
        private readonly IValidationRulesFactory<T> rulesFactory;

        public Validator(IValidationRulesFactory<T> rulesFactory)
        {
            Raise.ArgumentNullException.IfIsNull(rulesFactory, nameof(rulesFactory));

            this.rulesFactory = rulesFactory;
        }

        public virtual IProcessingResult<bool> Validate(T entity)
        {
            IEnumerable<IValidationRule<T>> validationRules = rulesFactory.Create();
            var errorsList = new List<IError>(validationRules.Count());

            foreach (var rule in validationRules)
            {
                IProcessingResult<bool> fieldValidationResult = rule.Check(entity);

                if (!fieldValidationResult.Result)
                {
                    errorsList.AddRange(fieldValidationResult.Errors);
                }
            }

            if (errorsList.Any())
            {
                return new ProcessingResult<bool>(false, errorsList);
            }
            else
            {
                return new ProcessingResult<bool>(true);
            }
        }
    }
}
