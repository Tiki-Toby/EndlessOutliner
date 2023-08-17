using UnityEngine;

namespace Client.Generation.Coins
{
    public class CoinView : MonoBehaviour
    {
        [SerializeField] private ParticleSystem coinParticleSystem;
        public int ID { get; set; }

        public ParticleSystem CoinParticleSystem => coinParticleSystem;
    }
}