namespace Vehman2.CarModels
{
    public static class CarModelConsts
    {
        private const string DefaultSorting = "{0}Name asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "CarModel." : string.Empty);
        }

    }
}