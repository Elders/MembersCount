using CommunityToolkit.HighPerformance;
using Elders.Cronus;

namespace UniCom.Newton.Shared;

public static class UrnExtensions
{
    public static T Extract<T>(this Urn urn, string nssDescriptor, Func<string, T> factory, bool throwOnError = true, T defaultValue = default)
    {
        try
        {
            var tokens = urn.Value
                .AsSpan()
                .Slice(3)
                .Tokenize(Urn.PARTS_DELIMITER);

            while (true)
            {
                if (tokens.MoveNext() == false)
                    break;

                if (tokens.Current.IndexOf(nssDescriptor) >= 0)
                {
                    if (tokens.MoveNext())
                    {
                        return factory(tokens.Current.ToString());
                    }
                }
            }

            if (throwOnError)
                throw new NullReferenceException();
            else
                return defaultValue;
        }
        catch (Exception)
        {
            if (throwOnError)
                throw new NullReferenceException();
            else
                return defaultValue;
        }
    }
}
