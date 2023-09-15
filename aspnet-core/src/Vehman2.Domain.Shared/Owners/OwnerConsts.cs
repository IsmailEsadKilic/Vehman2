namespace Vehman2.Owners
{
    public static class OwnerConsts
    {
        private const string DefaultSorting = "{0}Name asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Owner." : string.Empty);
        }

    }
}