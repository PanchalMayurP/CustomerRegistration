namespace CustomerRegistration.Infrastructure.Services;
public class CustomerService : ICustomerInfrastructure
{
    CustomerDbContext _customerDbContext;
    private readonly IMapper _mapper;

    public CustomerService(CustomerDbContext customerDbContext, IMapper mapper)
    {
        _customerDbContext = customerDbContext;
        _mapper = mapper;
    }
    public async Task<ApiRequestCustomer> AddCustomer(ApiRequestCustomer apiRequestCustomer)
    {
        try
        {
            var _isDataExists = await _customerDbContext.Customer.Where(x => x.FullName == apiRequestCustomer.FullName && x.IsActive == true).FirstOrDefaultAsync();

            if (_isDataExists == null)
            {
                var customerData = _mapper.Map<Customer>(apiRequestCustomer);
                await _customerDbContext.Customer.AddAsync(customerData);
                await _customerDbContext.SaveChangesAsync();
                apiRequestCustomer.Id = customerData.Id;
            }
            return apiRequestCustomer;

        }
        catch (Exception)
        {
            return null;
        }
    }

    public void DeleteCustomer(int id)
    {
        try
        {
            var _getDataById = _customerDbContext.Customer.Where(x => x.Id == id && x.IsActive == true).FirstOrDefault();
            if (_getDataById != null)
            {
                _customerDbContext.Customer.Update(_getDataById);
                _customerDbContext.SaveChanges();
            }
        }
        catch (Exception ex)
        {
        }
    }

    public async Task<ApiRequestCustomer> GetCustomer(int id)
     => await _customerDbContext.Customer.Where(x => x.Id == id && x.IsActive == true)
       .Select(x => new ApiRequestCustomer
       {
           Id = x.Id,
           FullName = x.FullName,
           DateOfBirth = x.DateOfBirth,
           Address = x.Address,
           DateOfRegistration = x.DateOfRegistration,
           IsActive = x.IsActive
       }).FirstOrDefaultAsync();


    public async Task<List<ApiRequestCustomer>> GetCustomerAll()
    => await _customerDbContext.Customer
       .Select(x => new ApiRequestCustomer
       {
           Id = x.Id,
           FullName = x.FullName,
           DateOfBirth = x.DateOfBirth,
           Address = x.Address,
           DateOfRegistration = x.DateOfRegistration,
           IsActive = x.IsActive
       }).ToListAsync();


    public async Task<ApiRequestCustomer> UpdateCustomer(ApiRequestCustomer apiRequestCustomer)
    {
        try
        {
            var _getData = await _customerDbContext.Customer.Where(x => x.Id == apiRequestCustomer.Id && x.IsActive == true).FirstOrDefaultAsync();
            if (_getData == null)
            {
                apiRequestCustomer.Id = -2;
                return apiRequestCustomer;
            }

            _getData.FullName = apiRequestCustomer.FullName != null && apiRequestCustomer.FullName != String.Empty ? apiRequestCustomer.FullName : _getData.FullName;
            _getData.DateOfBirth = apiRequestCustomer.DateOfBirth != null ? apiRequestCustomer.DateOfBirth.Value : _getData.DateOfBirth;
            _getData.Address = apiRequestCustomer.Address != null && apiRequestCustomer.Address != String.Empty ? apiRequestCustomer.Address : _getData.Address;
            _getData.DateOfRegistration = apiRequestCustomer.DateOfRegistration != null ? apiRequestCustomer.DateOfRegistration.Value : _getData.DateOfRegistration;
            _getData.IsActive = apiRequestCustomer.IsActive != null ? apiRequestCustomer.IsActive.Value : _getData.IsActive;
            _customerDbContext.Customer.Update(_getData);
            await _customerDbContext.SaveChangesAsync();
        }
        catch
        {
            apiRequestCustomer = null;
        }
        return apiRequestCustomer;
    }
}
