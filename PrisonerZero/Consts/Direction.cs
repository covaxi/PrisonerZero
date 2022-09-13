namespace PrisonerZero.Consts;

internal static class Direction
{
    const string North = "⬆️";
    const string NorthEast = "↗️";
    const string East = "➡️";
    const string SouthEast= "↘️";
    const string South = "⬇️";
    const string SouthWest = "↙️";
    const string West = "⬅️";
    const string NorthWest= "↖️";

    public static string GetArrow(decimal degrees)
    {
        return ((degrees + 720) % 360) switch
        {
            < 22.5m or >= 337.5m => North,
            < 67.5m => NorthEast,
            < 112.5m => East,
            < 157.5m => SouthEast,
            < 202.5m => South,
            < 247.5m => SouthWest,
            < 292.5m => West,
            < 337.5m => NorthWest,
        };
    }
}

