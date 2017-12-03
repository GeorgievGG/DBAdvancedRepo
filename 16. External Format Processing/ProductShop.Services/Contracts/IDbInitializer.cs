using System;

namespace ProductShop.Services.Contracts
{
    public interface IDbInitializer
    {
        void Initialize();
        void Reset();
    }
}
