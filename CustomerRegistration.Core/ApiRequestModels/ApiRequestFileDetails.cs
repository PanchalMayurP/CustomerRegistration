namespace CustomerRegistration.Core.ApiRequestModels;

public class ApiRequestFileDetails
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public string? FileName { get; set; }
    public string? FilePath { get; set; }
    public string? Base64 { get; set; }
}
