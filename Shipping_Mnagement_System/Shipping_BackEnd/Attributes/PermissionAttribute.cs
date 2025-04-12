namespace Shipping_APIs.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]

    public class PermissionAttribute : Attribute
    {
        public string Permission { get; }

        public PermissionAttribute(string permission)
        {
            Permission = permission;
        }
    }
}
