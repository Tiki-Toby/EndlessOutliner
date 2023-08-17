using Client.Generation.Coins;
using Client.ReactiveModels;
using UnityEngine;

namespace Client.MainPlayer
{
    public class MainPlayerView : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D playerRigidbody;
        [SerializeField] private ParticleSystem playerParticleSystem;

        private PlayerReactiveModel _playerReactiveModel;

        public Rigidbody2D PlayerRigidbody => playerRigidbody;
        public ParticleSystem PlayerParticleSystem => playerParticleSystem;

        public void Init(PlayerReactiveModel playerReactiveModel)
        {
            _playerReactiveModel = playerReactiveModel;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<CoinView>(out CoinView coinView))
            {
                _playerReactiveModel.TakeCoin(coinView);
            }
            else
            {
                _playerReactiveModel.Death();
            }
        }
    }
}