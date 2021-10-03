using GameData;
using TMPro;
using UnityEngine;

namespace UI.Bars
{
    public class KillCounterBar : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI counter;
        private KillData killData;

        public void Construct(PlayerProgressData progressData)
        {
            killData = progressData.killData;
            killData.AddKillCounterListener(KillCounterHandler);
        }

        private void Start() => KillCounterHandler();

        private void KillCounterHandler() => counter.text = killData.killedZombies.ToString();

        private void OnDestroy()
        {
            killData.RemoveKillCounterListener(KillCounterHandler); 
        }
    }
}