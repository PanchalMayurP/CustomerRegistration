namespace CustomerRegistration.Infrastructure.Interface;
public interface IFileDetailsInfrastructure
{
    Task<ApiRequestFileDetails> AddFile(ApiRequestFileDetails apiRequestFileDetails);
    Task<ApiRequestFileDetails> UpdateFile(ApiRequestFileDetails apiRequestFileDetails);
    void DeleteFile(int id);
    Task<ApiRequestFileDetails> GetFile(int id);
    Task<List<ApiRequestFileDetails>> GetFileAll();

}
