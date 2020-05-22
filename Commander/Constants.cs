using System;

namespace Commander
{
    public static class Constants
    {
        public static readonly bool IN_DOCKER =
            bool.TryParse(Environment.GetEnvironmentVariable("IN_DOCKER"), out var result) && result;
    }
}
