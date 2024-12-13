using System;

namespace Genealogy.BLL.Exceptions
{
    public class InvalidGenderException(string message) : Exception(message);
}