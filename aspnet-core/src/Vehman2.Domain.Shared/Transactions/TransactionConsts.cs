namespace Vehman2.Transactions
{
    public static class TransactionConsts
    {
        private const string DefaultSorting = "{0}Price asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Transaction." : string.Empty);
        }

        public const double PriceMinLength = 0;
        public const double PriceMaxLength = 999999;
        public const double LitersMinLength = 0;
        public const double LitersMaxLength = 999999;
    }
}