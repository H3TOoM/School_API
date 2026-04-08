namespace school_api.Validation
{
    public abstract class AbstractValidator<T> : IValidator<T>
    {
        public async Task<ValidationResult> ValidateAsync(T entity)
        {
            var errors = new List<string>();
            await ValidateInternal(entity, errors);
            
            return errors.Count == 0 
                ? ValidationResult.Success() 
                : ValidationResult.Failure(errors.ToArray());
        }

        protected abstract Task ValidateInternal(T entity, List<string> errors);
    }
}
