using FluentValidation;

namespace Architecture.Model
{
    public abstract class ProductModelValidator : AbstractValidator<ProductModel>
    {
        public void Id() => RuleFor(product => product.Id).NotEmpty();

        public void Name() => RuleFor(product => product.Name).NotEmpty();

        public void Description() => RuleFor(product => product.Description).NotEmpty();

        public void UserId() => RuleFor(product => product.UserId).NotEmpty();

        public void Price() => RuleFor(product => product.Price).NotEmpty();

        //public void Email() => RuleFor(user => user.Email).EmailAddress();

        //public void LastName() => RuleFor(user => user.LastName).NotEmpty();

        //public void Auth() => RuleFor(user => user.Auth).SetValidator(new AuthModelValidator());
    }
}
