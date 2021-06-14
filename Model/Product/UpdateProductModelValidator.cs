namespace Architecture.Model
{
    public sealed class UpdateProductModelValidator : ProductModelValidator
    {
        public UpdateProductModelValidator()
        {
            Id(); Name(); Description();
            Price();
        }
    }
}
