namespace CustomerRegistration.Application.Application;
public class Customer : ICustomer
{
    private readonly ICustomerInfrastructure _customerInfrastructure;
    private readonly IFileDetails _fileDetails;

    public Customer(ICustomerInfrastructure customerInfrastructure, IFileDetails fileDetails)
    {
        _customerInfrastructure = customerInfrastructure;
        _fileDetails = fileDetails;

    }

    public Task<ApiRequestCustomer> AddCustomer(ApiRequestCustomer apiRequestCustomer)
    {
        CustomerValidation customerValidation = new CustomerValidation();
        ValidationResult result = customerValidation.Validate(apiRequestCustomer);
        if (!result.IsValid)
        {
            foreach (ValidationFailure rslt in result.Errors)
            {
                apiRequestCustomer.Error = $"{rslt.PropertyName}  {rslt.ErrorMessage}";
            }
        }

        var res = _customerInfrastructure.AddCustomer(apiRequestCustomer);
        return res;
    }

    public void DeleteCustomer(int id)
    {
        _customerInfrastructure.DeleteCustomer(id);
    }

    public Task<ApiRequestCustomer> GetCustomer(int id)
    {
        return _customerInfrastructure.GetCustomer(id);
    }

    public Task<List<ApiRequestCustomer>> GetCustomerAll()
    {
        return _customerInfrastructure.GetCustomerAll();
    }

    public Task<ApiRequestCustomer> UpdateCustomer(ApiRequestCustomer apiRequestCustomer)
    {
        CustomerValidation customerValidation = new CustomerValidation();
        ValidationResult result = customerValidation.Validate(apiRequestCustomer);
        if (!result.IsValid)
        {
            foreach (ValidationFailure rslt in result.Errors)
            {
                apiRequestCustomer.Error = $"{rslt.PropertyName}  {rslt.ErrorMessage}";
            }
        }
        var res = _customerInfrastructure.UpdateCustomer(apiRequestCustomer);
        return res;
    }
}
