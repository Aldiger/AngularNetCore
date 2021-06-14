namespace Architecture.Model
{
    public sealed class AddProductModelValidator : ProductModelValidator
    {
        public AddProductModelValidator()
        {
            Name(); Description(); UserId();
        }
    }
}
