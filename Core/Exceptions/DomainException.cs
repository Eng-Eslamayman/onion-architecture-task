﻿// Core/Exceptions/DomainException.cs
using System;

namespace Core.Exceptions
{
    public abstract class DomainException : Exception
    {
        protected DomainException(string message) : base(message)
        {
        }
    }
}
