namespace Bredinin.TestProject.Service.Core.Exceptions
{
    public class ServiceOwnErrorAttribute : Attribute
    {
        public string Message { get; }

        public ServiceOwnErrorAttribute(string message)
        {
            Message = message;
        }
    }
}
