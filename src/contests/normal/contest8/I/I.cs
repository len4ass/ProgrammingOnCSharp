using System.Collections.Generic;
using System.Linq;

internal static class Methods
{
    public static int FindBestBiathlonistValue(List<Sportsman> sportsmen)
    {
        return sportsmen.Max(sportsman =>
        {
            return sportsman is IShooter shooter and ISkiRunner skirunner
                ? (int)(0.4 * new[] {shooter.Shoot(), skirunner.Run()}.Max() + 0.6 * new[] {shooter.Shoot(), skirunner.Run()}.Min())
                : 0;
        });
    }

    public static int FindBestShooterValue(List<Sportsman> sportsmen)
    {
        return sportsmen.Max(sportsman => (sportsman as IShooter)?.Shoot() ?? 0);
    }

    public static int FindBestRunnerValue(List<Sportsman> sportsmen)
    {
        return sportsmen.Max(sportsman => (sportsman as ISkiRunner)?.Run() ?? 0);
    }
}