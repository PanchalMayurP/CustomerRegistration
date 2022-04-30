namespace CustomerRegistration.Core.Validation;
public class CustomerValidation : AbstractValidator<ApiRequestCustomer>
{
    public CustomerValidation()
    {
        RuleFor(x => x.FullName).NotEmpty().WithMessage("Customer Name is required");
        RuleFor(x => x.Address).NotEmpty().WithMessage("Customer Address is required");
    }

    private bool FullNameLength(string fullName)
    {
        if (fullName.Length < 50)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private bool AddressLength(string address)
    {
        if (address.Length < 500)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}

