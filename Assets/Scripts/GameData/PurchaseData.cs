using System;
using System.Collections.Generic;

namespace GameData
{
    [Serializable]
    public class PurchaseData
    {
        public List<PurchasedInApp> PurchasedInApps = new List<PurchasedInApp>();

        public event Action OnInAppPurchasedInfoChanged;

        public void AddPurchase(string productId)
        {
            PurchasedInApp purchasedInApp = PurchasedInApps.Find(item => item.PurchasedProductId.Equals(productId));
            if (ReferenceEquals(purchasedInApp, null))
                purchasedInApp.Count++;
            else
                PurchasedInApps.Add(new PurchasedInApp {PurchasedProductId = productId, Count = 1});

            OnInAppPurchasedInfoChanged?.Invoke();
        }
    }
}