using System.Collections.Generic;
using Providers.Assets;
using Services.GameProgress;

using UnityEngine;
using UnityEngine.AddressableAssets;

namespace UI.Screens.Shop
{
    public class ShopItemsContainer : MonoBehaviour
    {
        [SerializeField] private GameObject[] shopUnavailableObjects;
        [SerializeField] private Transform parent;

        private IAssetProvider _asset;
        //private IInAppService _inAppService;
        private IGameProgressService _progressService;
        private List<GameObject> _shopItems = new List<GameObject>();
        private const string ShopItemPath = "ShopItem";

        public void Construct(//
            IGameProgressService progressService,
            IAssetProvider asset)
        {
            _asset = asset;
           // _inAppService = inAppService;
            _progressService = progressService;
        }

        public void Initialize() => RefreshAvailableItems();

        public void Subscribe()
        {
           // _inAppService.Initialized += RefreshAvailableItems;
            _progressService.PlayerProgressData.purchaseData.OnInAppPurchasedInfoChanged += RefreshAvailableItems;
        }

        public void CleanUp()
        {
            //_inAppService.Initialized -= RefreshAvailableItems;
            _progressService.PlayerProgressData.purchaseData.OnInAppPurchasedInfoChanged -= RefreshAvailableItems;
        }

        private async void RefreshAvailableItems()
        {
            UpdateUnavailableObjects();

            // if (!_inAppService.IsInitialized)
            //     return;

            foreach (var item in _shopItems)
                Addressables.ReleaseInstance(item.gameObject);//todo move to 

            // foreach (var product in _inAppService.Products())
            // {
            //     var shopItemObject = await _asset.Instantiate(ShopItemPath, parent);
            //     if (!shopItemObject.TryGetComponent<ShopItem>(out var shopItem)) continue;
            //     shopItem.Construct(product,_inAppService,_asset);
            //     shopItem.Initialize();
            //     _shopItems.Add(shopItem.gameObject);
            // }
        }

        private void UpdateUnavailableObjects()
        {
            // foreach (var shopObject in shopUnavailableObjects)
            //     shopObject.SetActive(!_inAppService.IsInitialized);
        }
    }
}