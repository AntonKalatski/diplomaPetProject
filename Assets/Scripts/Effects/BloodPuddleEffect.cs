using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Effects
{
    public class BloodPuddleEffect : MonoBehaviour
    {
        [SerializeField] private float prefferedScale;
        private void Awake() => transform.localScale = Vector3.zero;
        private Action onPuddleEffectEnd;
        public void AddOnPuddleEffectListener(Action listener) => onPuddleEffectEnd += listener;

        public void RemovePuddleEffectListener(Action listener) => onPuddleEffectEnd -= listener;
        public void ShowBloodPuddle(Transform hips) => StartCoroutine(BloodPudleRoutine(hips));

        private IEnumerator BloodPudleRoutine(Transform hips)
        {
            Vector3 pos = new Vector3(hips.position.x, 0, hips.position.z);
            float scaleFactor = 0;
            float rndRotation = Random.Range(0, 180f);
            transform.rotation = Quaternion.Euler(0, rndRotation, 0);
            transform.position = pos;
            while (scaleFactor < prefferedScale)
            {
                scaleFactor += Time.deltaTime;
                transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
                yield return null;
            }
            onPuddleEffectEnd?.Invoke();
        }
    }
}