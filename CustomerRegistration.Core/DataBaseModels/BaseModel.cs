namespace CustomerRegistration.Core.DataBaseModels;
public class BaseModel
{
    public int? CreatedBy { get; set; }
    public DateTime? CreateDate { get; set; }
    public int? ModifiedBy { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public int? DeletedBy { get; set; }
    public DateTime? DeletedDate { get; set; }
    public bool? IsDeleted { get; set; }
}