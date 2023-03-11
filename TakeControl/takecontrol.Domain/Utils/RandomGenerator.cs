namespace takecontrol.Domain.Utils;

public static class RandomGenerator
{
    public static int RandomNumber(int min, int max)
    {
        var random = new Random();
        return random.Next(min, max);
    }

    public static string RandomString(int size, bool lowerCase = false)
    {
        var random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, size)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}