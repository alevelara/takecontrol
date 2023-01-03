using System.Text.RegularExpressions;

namespace takecontrol.Domain.Utils;

public static class ValitatorsUtil
{

    public static bool HasAnyLowerCase(string pw)
    {
        var lowercase = new Regex("[a-z]+");
        bool result = false;
        if (!string.IsNullOrEmpty(pw))
        {
            result = lowercase.IsMatch(pw);
        }

        return result;
    }

    public static bool HasAnyUpperCase(string pw)
    {
        var uppercase = new Regex("[A-Z]+");
        bool result = false;
        if (!string.IsNullOrEmpty(pw))
        {
            result = uppercase.IsMatch(pw);
        }
        return result;
    }

    public static bool HasAnyDigit(string pw)
    {
        var digit = new Regex("(\\d)+");
        bool result = false;
        if (!string.IsNullOrEmpty(pw))
        {
            result = digit.IsMatch(pw);
        }
        return result;
    }

    public static bool HasAnySymbol(string pw)
    {
        var symbol = new Regex("(\\W)+");
        bool result = false;
        if (!string.IsNullOrEmpty(pw))
        {
            result = symbol.IsMatch(pw);
        }
        return result;
    }
}
