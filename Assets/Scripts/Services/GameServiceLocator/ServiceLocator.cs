namespace Services.GameServiceLocator
{
    public class ServiceLocator
    {
        private static ServiceLocator instance;
        public static ServiceLocator Container => instance ??= new ServiceLocator();

        public void RegisterService<TService>(TService implementation) where TService : IService =>
            Implementation<TService>.ServiceInstance = implementation;

        public TService LocateService<TService>() where TService : IService => Implementation<TService>.ServiceInstance;

        private static class Implementation<TService> where TService : IService
        {
            public static TService ServiceInstance;
        }
    }
}