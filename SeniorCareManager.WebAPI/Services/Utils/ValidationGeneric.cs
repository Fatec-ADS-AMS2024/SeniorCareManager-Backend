namespace SeniorCareManager.WebAPI.Services.Utils;

public static class ValidationGeneric
{
    public static bool IsNull<T>(this T atributo)
    {
        if (atributo == null)
            return true;

        if (atributo is string stringValue)
            return string.IsNullOrWhiteSpace(stringValue);
        
        return false;
    }
}
