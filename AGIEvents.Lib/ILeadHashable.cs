namespace AGIEvents.Lib;

public interface ILeadHashable
{
    string FirstName { get; }
    string LastName { get; }
    string Company { get; }

    public static int GetHash(ILeadHashable obj) =>
        obj.FirstName.GetHashCode() ^ obj.LastName.GetHashCode() ^ obj.Company.GetHashCode();
}