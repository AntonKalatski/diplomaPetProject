using Factories;
using Providers.Assets;
using Services.GameServiceLocator;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public class TestAddressables : MonoBehaviour
{
    [SerializeField] private Button button;

    [SerializeField] private AssetReference reference;


    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(ReleaseReference);
    }

    private async void ReleaseReference()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}