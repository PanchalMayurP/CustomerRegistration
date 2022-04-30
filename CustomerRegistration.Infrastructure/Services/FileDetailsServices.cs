namespace CustomerRegistration.Infrastructure.Services;
    public class FileDetailsServices : IFileDetailsInfrastructure
    {
        CustomerDbContext _customerDbContext;
        private readonly IMapper _mapper;

        public FileDetailsServices(CustomerDbContext customerDbContext, IMapper mapper)
        {
            _customerDbContext = customerDbContext;
            _mapper = mapper;
        }
        public async Task<ApiRequestFileDetails> AddFile(ApiRequestFileDetails apiRequestFileDetails)
        {
            try
            {
                var _isDataExists = await _customerDbContext.FileDetails.Where(x => x.FileName == apiRequestFileDetails.FileName).FirstOrDefaultAsync();

                if (_isDataExists == null)
                {

                    var customerData = _mapper.Map<FileDetails>(apiRequestFileDetails);
                    await _customerDbContext.FileDetails.AddAsync(customerData);
                    await _customerDbContext.SaveChangesAsync();
                    customerData.Id = customerData.Id;
                    apiRequestFileDetails.CustomerId = customerData.Id;
                }
                return apiRequestFileDetails;

            }
            catch (Exception)
            {
                return null;
            }
        }

        public void DeleteFile(int id)
        {
            try
            {
                var _getDataById = _customerDbContext.FileDetails.Where(x => x.Id == id).FirstOrDefault();
                if (_getDataById != null)
                {
                    _customerDbContext.FileDetails.Update(_getDataById);
                    _customerDbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
            }
        }

        public async Task<ApiRequestFileDetails> GetFile(int id)
         => await _customerDbContext.FileDetails.Where(x => x.Id == id)
           .Select(x => new ApiRequestFileDetails
           {
               Id = x.Id,
               CustomerId = x.CustomerId,
               FileName = x.FileName,
               FilePath = x.FilePath,
               Base64 = CommonExtendedUilities.ImageToBase64(x.FilePath + x.FileName)
           }).FirstOrDefaultAsync();


        public async Task<List<ApiRequestFileDetails>> GetFileAll()
        => await _customerDbContext.FileDetails
           .Select(x => new ApiRequestFileDetails
           {
               Id = x.Id,
               CustomerId = x.CustomerId,
               FileName = x.FileName,
               FilePath = x.FilePath,
               Base64 = CommonExtendedUilities.ImageToBase64(x.FilePath + x.FileName)
           }).ToListAsync();

        public async Task<ApiRequestFileDetails> UpdateFile(ApiRequestFileDetails apiRequestFileDetails)
        {
            try
            {
                var _getData = await _customerDbContext.FileDetails.Where(x => x.Id == apiRequestFileDetails.Id).FirstOrDefaultAsync();
                if (_getData == null)
                {
                    apiRequestFileDetails.Id = -2;
                    return apiRequestFileDetails;
                }

                _getData.CustomerId = apiRequestFileDetails.CustomerId != 0 ? apiRequestFileDetails.CustomerId : _getData.CustomerId;
                _getData.FileName = apiRequestFileDetails.FileName != null ? apiRequestFileDetails.FileName : _getData.FileName;
                _getData.FilePath = apiRequestFileDetails.FilePath != null && apiRequestFileDetails.FilePath != String.Empty ? apiRequestFileDetails.FilePath : _getData.FilePath;
                _customerDbContext.FileDetails.Update(_getData);
                await _customerDbContext.SaveChangesAsync();
            }
            catch
            {
                apiRequestFileDetails = null;
            }
            return apiRequestFileDetails;
        }
    }
