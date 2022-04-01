using System;

namespace Services
{
    public interface IInputProvider
    {
        Action<string> OnButtonClickProvide { get; set; }
    }
}