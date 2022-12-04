namespace takecontrol.Domain.Shared;

public abstract class BaseDomainModel<T> where T : ValueObject
{
    public T Id { get; set; }
    public DateTime? CreatedDate { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastModifiedDate { get; set; }
    public string? LastModifiedBy { get; set; }
}
