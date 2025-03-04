namespace stock_market.Helplers;

public static class DateTimeHelpers
{
   
        private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static DateTime FromUnixTime(long unixTime)
        {
            var a  = Epoch.AddSeconds(unixTime).ToLocalTime();
            return Epoch.AddSeconds(unixTime).ToLocalTime();
        }
}