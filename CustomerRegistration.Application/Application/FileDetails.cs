namespace CustomerRegistration.Application.Application;
public class FileDetails : IFileDetails
{
    private readonly IFileDetailsInfrastructure _fileInfrastructure;

    public FileDetails(IFileDetailsInfrastructure fileInfrastructure)
    {
        _fileInfrastructure = fileInfrastructure;

    }
    public async Task<ApiRequestFileDetails> AddFile(IFormFile ImageName, ApiRequestFileDetails apiRequestFileDetails)
    {
        string path = Path.Combine(Directory.GetCurrentDirectory(), Constrains.FOLDER_PATH);
        if (!Directory.Exists(path))
        {
            DirectoryInfo di = Directory.CreateDirectory(path);
        }
        try
        {
            if (ImageName != null)
            {
                string Extension = Path.GetExtension(ImageName.FileName);
                string WExtension = Path.GetFileNameWithoutExtension(ImageName.FileName);
                string fileName = WExtension.AppendTimeStamp("") + Extension;
                apiRequestFileDetails.FileName = fileName;
                apiRequestFileDetails.FilePath = path;

            }
            var res = await _fileInfrastructure.AddFile(apiRequestFileDetails);

            if (ImageName != null && ImageName.Length > 0)
            {
                CommonExtendedUilities.FileUpload(ImageName, path, apiRequestFileDetails.FileName);
            }
            return res;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async void DeleteFile(int id)
    {
        var getData = await _fileInfrastructure.GetFile(id);
        if (getData != null)
        {
            File.Delete(getData.FilePath);
            _fileInfrastructure.DeleteFile(id);
        }
    }

    public async Task<ApiRequestFileDetails> GetFile(int id)
   => await _fileInfrastructure.GetFile(id);

    public async Task<List<ApiRequestFileDetails>> GetFileAll()
    => await _fileInfrastructure.GetFileAll();
    public async Task<ApiRequestFileDetails> UpdateFile(IFormFile ImageName, ApiRequestFileDetails apiRequestFileDetails)
    {
        string path = Path.Combine(Directory.GetCurrentDirectory(), Constrains.FOLDER_PATH);

        var getData = await _fileInfrastructure.GetFile(apiRequestFileDetails.Id);
        if (getData != null)
        {
            File.Delete(getData.FilePath + getData.FileName);
        }

        if (ImageName != null)
        {
            string Extension = Path.GetExtension(ImageName.FileName);
            string WExtension = Path.GetFileNameWithoutExtension(ImageName.FileName);
            string fileName = WExtension.AppendTimeStamp("") + Extension;
            apiRequestFileDetails.FileName = fileName;
            apiRequestFileDetails.FilePath = path;

        }

        var res = await _fileInfrastructure.UpdateFile(apiRequestFileDetails);

        if (ImageName != null && ImageName.Length > 0)
        {

            CommonExtendedUilities.FileUpload(ImageName, path, apiRequestFileDetails.FileName);
        }

        return res;
    }
}

