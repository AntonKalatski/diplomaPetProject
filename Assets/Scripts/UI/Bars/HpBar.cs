using UnityEngine;
using UnityEngine.UI;

namespace UI.Bars
{
    public class HpBar : MonoBehaviour
    {
        [SerializeField] private Image image;
        public void SetValue(float current, float max) => image.fillAmount = current / max;
    }
}