using System;
using System.Collections.Generic;
using System.Linq;
using Extensions;
using UnityEngine;
using UnityEngine.Purchasing;

namespace Services.IAp
{
    public class InAppProvider : IStoreListener
    {
        private const string inAppConfigsPath = "IAP/products";

        public Dictionary<string, ProductConfig> Configs { get; private set; }
        public Dictionary<string, Product> Products { get; private set; }

        private IExtensionProvider _extensions;
        private IStoreController _controller;
        private IInAppService _inAppService;

        public bool IsInitialized => _controller != null && _extensions != null;
        public event Action Initialized;

        public void Initilalize(IInAppService inAppService)
        {
            _inAppService = inAppService;

            Configs = new Dictionary<string, ProductConfig>();
            Products = new Dictionary<string, Product>();

            Load();

            ConfigurationBuilder builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

            foreach (var config in Configs.Values)
                builder.AddProduct(config.Id, config.ProductType);

            UnityPurchasing.Initialize(this, builder: builder);
        }

        public void StartPurchase(string productId) => _controller.InitiatePurchase(productId);

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            _controller = controller;
            _extensions = extensions;
            Initialized?.Invoke();

            foreach (var product in controller.products.all)
                Products.Add(product.definition.id, product);
            Debug.Log("Unity Purchasing Initialization success");
        }

        public void OnInitializeFailed(InitializationFailureReason error) =>
            Debug.Log("Unity Purchasing Initialization failed");

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
        {
            Debug.Log($"Unity Purchasing ProcessPurchase success {purchaseEvent.purchasedProduct.definition.id}");

            return _inAppService.ProcessPurchase(purchaseEvent.purchasedProduct);
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason) =>
            Debug.LogError(
                $"Product {product.definition.id}, purchase failed, Purchase failure reason {failureReason}, transaction id {product.transactionID}");

        private void Load()
        {
            Configs = Resources
                .Load<TextAsset>(inAppConfigsPath)
                .text
                .ToDeserialized<ProductConfigWrapper>()
                .Configs
                .ToDictionary(key => key.Id, Value => Value);
        }
    }
}