namespace Vehman2.Fuels
{
    public static class FuelConsts
    {
        private const string DefaultSorting = "{0}Name asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Fuel." : string.Empty);
        }

    }
}