namespace CustomerRegistration.Core.DataBaseModels;

public class Customer : BaseModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string? FullName { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? Address { get; set; }
    public DateTime? DateOfRegistration { get; set; }
    public bool? IsActive { get; set; }
}


