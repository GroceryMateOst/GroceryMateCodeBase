namespace grocery_mate_backend.Data.DataModels.Authentication;

public class TokenBlacklistEntry
{
    public Guid TokenBlacklistEntryId { get; set; }
    public string CanceledToken { get; set; }
    public DateTime CancellationDate { get; set; }

    public TokenBlacklistEntry(string canceledToken, DateTime cancellationDate)
    {
        CanceledToken = canceledToken;
        CancellationDate = cancellationDate;
    }

    public TokenBlacklistEntry()
    {
        CanceledToken = string.Empty;
        CancellationDate = DateTime.MinValue;
    }
}