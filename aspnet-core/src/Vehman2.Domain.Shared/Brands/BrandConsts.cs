namespace Vehman2.Brands
{
    public static class BrandConsts
    {
        private const string DefaultSorting = "{0}Name asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Brand." : string.Empty);
        }

    }
}