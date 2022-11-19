namespace DynamicyRoles.WebApp.Attributes
{
    [AttributeUsage(validOn: AttributeTargets.Method | AttributeTargets.Class)]
    public class AllowAnonymousAttribute : Attribute
    {
    }
}
