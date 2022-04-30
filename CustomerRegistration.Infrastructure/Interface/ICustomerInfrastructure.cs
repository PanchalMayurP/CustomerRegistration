namespace CustomerRegistration.Infrastructure.Interface;
public interface ICustomerInfrastructure
{
    Task<ApiRequestCustomer> AddCustomer(ApiRequestCustomer apiRequestCustomer);
    Task<ApiRequestCustomer> UpdateCustomer(ApiRequestCustomer apiRequestCustomer);
    void DeleteCustomer(int id);
    Task<ApiRequestCustomer> GetCustomer(int id);
    Task<List<ApiRequestCustomer>> GetCustomerAll();

}
