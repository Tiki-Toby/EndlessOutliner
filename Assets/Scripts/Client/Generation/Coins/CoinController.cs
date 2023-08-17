using Client.Interfaces;
using Client.ReactiveModels;
using Configs.LogicConfigs;
using Tools.Ui;
using UnityEngine;

namespace Client.Generation.Coins
{
    public class CoinController : IPoolableElementController<int, CoinView>, IOuterLogicUpdate, IDistanceUpdate
    {
        private readonly PlayerReactiveModel _playerReactiveModel;
        private readonly CoinConfig _coinConfig;

        private CoinView _coinView;

        private float _speed;
        
        public Transform Transform => _coinView.transform;
        public int ID => _coinView.ID;

        public CoinController(PlayerReactiveModel playerReactiveModel,
            CoinConfig coinConfig)
        {
            _playerReactiveModel = playerReactiveModel;
            _coinConfig = coinConfig;
        }
        
        public void SetView(CoinView view)
        {
            _coinView = view;
        }

        public void Open(int key)
        {
            _coinView.ID = key;
        }

        public void Init(Vector3 spawnPosition)
        {
            _coinView.transform.position = spawnPosition;
        }

        public void Update(float frameLength)
        {
            Vector2 deltaPosition = ((Vector2)_coinView.transform.position - _playerReactiveModel.Position);
            float distance = deltaPosition.magnitude;
            if (distance <= _coinConfig.CoinMoveTolerance)
            {
                _speed += _coinConfig.Acceleration * frameLength;
                if(!_coinView.CoinParticleSystem.isPlaying)
                    _coinView.CoinParticleSystem.Play();
            }
            else
            {
                _speed -= _coinConfig.Acceleration * frameLength;
                if(_coinView.CoinParticleSystem.isPlaying)
                    _coinView.CoinParticleSystem.Pause();
            }

            _speed = Mathf.Clamp(_speed, 0f, _coinConfig.BaseSpeed);
            _coinView.transform.position -= (Vector3)deltaPosition.normalized * _speed * frameLength;
        }

        public void UpdateDistance(IReadOnlyLevelStateData levelStateData)
        {
            _coinView.transform.position -= Vector3.right * levelStateData.DeltaMove;
        }

        public void Close()
        {
            _coinView.gameObject.SetActive(false);
        }
        
        public void Dispose()
        {
            Object.Destroy(_coinView.gameObject);
        }
    }
}