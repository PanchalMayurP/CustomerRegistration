namespace CustomerRegistration.Core.DataBaseModels;
public class FileDetails : BaseModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public string? FileName { get; set; }
    public string? FilePath { get; set; }
}
