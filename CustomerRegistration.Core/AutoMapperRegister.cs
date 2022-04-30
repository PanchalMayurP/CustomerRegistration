namespace CustomerRegistration.Core;
public class AutoMapperRegister : Profile
{
    public AutoMapperRegister()
    {
        //admin
        CreateMap<Customer, ApiRequestCustomer>().ReverseMap();
        CreateMap<FileDetails, ApiRequestFileDetails>().ReverseMap();

    }

}
