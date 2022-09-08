using System;

namespace TufanFramework.Core.Authentication
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class RequiredPermissionAttribute : Attribute
    {
        public Permission RequiredPermission { get; private set; }
        public RequiredPermissionAttribute(string permissionCode)
        {

            if (string.IsNullOrWhiteSpace(permissionCode))
                return;

            RequiredPermission = new Permission(permissionCode);
        }
    }
}