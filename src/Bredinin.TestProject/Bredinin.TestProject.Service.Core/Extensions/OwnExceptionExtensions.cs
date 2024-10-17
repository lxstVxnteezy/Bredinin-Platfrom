using Bredinin.TestProject.Service.Common;
using Bredinin.TestProject.Service.Core.Exceptions;

namespace Bredinin.TestProject.Service.Core.Extensions
{
    public static class OwnExceptionExtensions
    {
        public static OwnException ToException<TEnum>(this TEnum value, string? userMessage = null)
            where TEnum : Enum
        {
            var ownErrorAttribute = value.GetAttribute<ServiceOwnErrorAttribute>();

            return new OwnException(code: (int)(object)value, ownErrorAttribute.Message, userMessage);
        }
    }
}
