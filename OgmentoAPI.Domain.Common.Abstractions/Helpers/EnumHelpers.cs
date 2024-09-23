namespace OgmentoAPI.Domain.Common.Abstractions.Helpers
{
    public static class EnumHelper
    {
        public static string GetEnumName<T>(int value) where T : Enum
        {
            return Enum.GetName(typeof(T), value);
        }
    }
}
