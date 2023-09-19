namespace Vehman2.Vehicles
{
    public static class VehicleConsts
    {
        private const string DefaultSorting = "{0}Plate asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Vehicle." : string.Empty);
        }

    }
}