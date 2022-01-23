// using System;
// using System.Collections.Generic;
// using Services.GameServiceLocator;
// using UnityEngine.Purchasing;
//
// namespace Services.IAp
// {
//     public interface IInAppService : IService
//     {
//         PurchaseProcessingResult ProcessPurchase(Product purchasedProduct);
//         bool IsInitialized { get; }
//         event Action Initialized;
//         void Initialize();
//         void StartPurchase(string productId);
//         List<ProductDescription> Products();
//     }
// }