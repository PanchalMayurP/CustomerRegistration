namespace CustomerRegistration.Application.Interface;
public interface IFileDetails
{
    Task<ApiRequestFileDetails> AddFile(IFormFile ImageName, ApiRequestFileDetails apiRequestFileDetails);
    Task<ApiRequestFileDetails> UpdateFile(IFormFile ImageName, ApiRequestFileDetails apiRequestFileDetails);
    void DeleteFile(int id);
    Task<ApiRequestFileDetails> GetFile(int id);
    Task<List<ApiRequestFileDetails>> GetFileAll();
}
