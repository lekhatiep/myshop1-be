using Api.Models;

namespace Api.Share
{
    //If a Interface has two or more class implement
    //Delegate indentify name of class implement 
    public class Delegate
    {
        public delegate IStudentRepository ServiceResolver(ServiceType serviceType);
    }
}
