using TMPro;
using UnityEngine;

namespace Profiling
{
    public class FpsCounter : MonoBehaviour
    {
        [SerializeField] private TMP_Text counterText;

        private void Update()
        {
            var fps = 1 / Time.unscaledDeltaTime;
            counterText.text = fps.ToString("F0");
        }
    }
}
