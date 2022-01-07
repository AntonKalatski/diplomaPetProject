using System;
using System.Collections.Generic;
using System.Linq;
using GameData;
using Services.GameProgress;
using UnityEngine.Purchasing;

namespace Services.IAp
{
    public class InAppService : IInAppService
    {
        private readonly InAppProvider _inAppProvider;
        private readonly IGameProgressService _progressService;

        public bool IsInitialized => _inAppProvider.IsInitialized;
        public event Action Initialized;

        public InAppService(InAppProvider inAppProvider, IGameProgressService progressService)
        {
            _inAppProvider = inAppProvider;
            _progressService = progressService;
        }

        public void Initialize()
        {
            _inAppProvider.Initilalize(this);
            _inAppProvider.Initialized += () => Initialized?.Invoke();
        }

        public void StartPurchase(string productId) => _inAppProvider.StartPurchase(productId);

        public List<ProductDescription> Products() => ProductDescriptions().ToList();

        public PurchaseProcessingResult ProcessPurchase(Product purchasedProduct)
        {
            ProductConfig productConfig = _inAppProvider.Configs[purchasedProduct.definition.id];
            switch (productConfig.ItemType)
            {
                case ItemType.None:
                    break;
                case ItemType.Skulls:
                    _progressService.PlayerProgressData.killData.AddSkullCount(productConfig.Quantity);
                    _progressService.PlayerProgressData.purchaseData.AddPurchase(purchasedProduct.definition.id);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return PurchaseProcessingResult.Complete;
        }

        private IEnumerable<ProductDescription> ProductDescriptions()
        {
            PurchaseData purchaseData = _progressService.PlayerProgressData.purchaseData;

            foreach (string productId in _inAppProvider.Products.Keys)
            {
                ProductConfig config = _inAppProvider.Configs[productId];
                Product product = _inAppProvider.Products[productId];

                PurchasedInApp purchasedInApp =
                    purchaseData.PurchasedInApps.Find(inApp => inApp.PurchasedProductId.Equals(productId));
                if (IsProductFullyPurchased(purchasedInApp, config))
                    continue;

                yield return new ProductDescription()
                {
                    productId = productId,
                    productConfig = config,
                    product = product,
                    availablePurchasesLeft = !ReferenceEquals(purchasedInApp, null)
                        ? config.MaxPurchaseCount - purchasedInApp.Count
                        : config.MaxPurchaseCount
                };
            }
        }

        private bool IsProductFullyPurchased(PurchasedInApp purchasedInApp, ProductConfig config) =>
            !ReferenceEquals(purchasedInApp, null) && purchasedInApp.Count >= config.MaxPurchaseCount;
    }
}