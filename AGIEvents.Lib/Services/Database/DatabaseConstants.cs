namespace AGIEvents.Lib.Services.Database;

public static class DatabaseConstants
{
    private const string DatabaseFileName = "AgiEventsDB";

    // Flags
    // - open the database in read/write mode
    // - create the database if it doesn't exist
    // - enable multi-threaded database access
    public const SQLite.SQLiteOpenFlags Flags =
        SQLite.SQLiteOpenFlags.Create |
        SQLite.SQLiteOpenFlags.ReadWrite |
        SQLite.SQLiteOpenFlags.SharedCache;

    public static string DatabasePath =>
        Path.Combine(FileSystem.AppDataDirectory, DatabaseFileName);
}