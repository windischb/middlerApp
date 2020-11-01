namespace middlerApp.IDP.DataAccess.Entities.Models
{
    public interface IConcurrencyAware
    {
        string ConcurrencyStamp { get; set; }
    }
}
