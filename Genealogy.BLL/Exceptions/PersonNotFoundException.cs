using System;

namespace Genealogy.BLL.Exceptions
{
    public class PersonNotFoundException(string message) : Exception(message);
    
}