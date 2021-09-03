using Effects;
using UnityEngine;

namespace Player
{
    public class PlayerVfx : MonoBehaviour
    {
        [SerializeField] private PlayerBodyProvider playerBodyProvider;
        [SerializeField] private BloodPuddleEffect bloodPuddle;
        [SerializeField] private ParticleSystem blood;
        private void Awake() => blood.transform.rotation = Quaternion.LookRotation(-transform.forward);

        public void Damage() => blood.Play();

        public void Death() => Instantiate(bloodPuddle, transform).ShowBloodPuddle(playerBodyProvider.Hips);
    }
}