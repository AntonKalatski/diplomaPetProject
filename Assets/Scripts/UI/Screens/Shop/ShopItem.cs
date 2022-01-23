using Providers.Assets;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Screens.Shop
{
    public class ShopItem : MonoBehaviour
    {
        [SerializeField] private Button buyItemButton;
        [SerializeField] private TMP_Text priceText;
        [SerializeField] private TMP_Text quantityText;
        [SerializeField] private TMP_Text availableItemsLeft;
        [SerializeField] private Image icon;
       // private ProductDescription _productDescription;
        //private IInAppService _inAppService;
        private IAssetProvider _assetsProvider;

        public void Construct(
           // ProductDescription productDescription,
            //IInAppService inAppService,
            IAssetProvider assetProvider)
        {
            //_inAppService = inAppService;
           // _productDescription = productDescription;
            _assetsProvider = assetProvider;
        }

        public async void Initialize()
        {
            //buyItemButton.onClick.AddListener(OnBuyItemClickHandler);
            //priceText.text = _productDescription.productConfig.Price;
           // quantityText.text = _productDescription.productConfig.Quantity.ToString();
           // availableItemsLeft.text = _productDescription.availablePurchasesLeft.ToString();
           //icon.sprite = await _assetsProvider.Load<Sprite>(_productDescription.productConfig.Icon);
        }

       // private void OnBuyItemClickHandler() => _inAppService.StartPurchase(_productDescription.productId);
    }
}