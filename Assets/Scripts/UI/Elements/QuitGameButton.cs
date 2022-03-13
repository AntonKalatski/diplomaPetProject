using UnityEngine;
using UnityEngine.UI;

namespace UI.Elements
{
    public class QuitGameButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        
        private void Awake() => button.onClick.AddListener(QuitGameHandler);

        private void QuitGameHandler() => Application.Quit();
    }
}