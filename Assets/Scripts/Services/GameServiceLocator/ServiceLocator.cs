using System;

namespace Services.GameServiceLocator
{
    public class ServiceLocator
    {
        private static ServiceLocator instance;
        public static ServiceLocator Container => instance ??= new ServiceLocator();

        public void RegisterService<TService>(TService implementation) where TService : IService
        {
            if (ReferenceEquals(implementation, null))
                throw new ArgumentNullException(nameof(implementation) + "is null or empty");

            Implementation<TService>.ServiceInstance = implementation;
        }

        public TService LocateService<TService>() where TService : IService
        {
            return Implementation<TService>.ServiceInstance;
        }

        private static class Implementation<TService> where TService : IService
        {
            public static TService ServiceInstance;
        }
    }
}