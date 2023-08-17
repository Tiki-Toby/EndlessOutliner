using System;
using Client.Generation.Coins;
using UnityEngine;

namespace Client.ReactiveModels
{
    public class PlayerReactiveModel
    {
        private int _takenCoinCount;
        private Vector2 _position;

        private Action<CoinView> _onTakeCoinView;
        private Action<int> _onTakeCoin;
        private Action _onDeath;

        public Vector2 Position => _position;

        public PlayerReactiveModel()
        {
            _takenCoinCount = 0;
        }
        
        public void SubscribeOnTakeCoinView(Action<CoinView> subscribe)
        {
            _onTakeCoinView += subscribe;
        }
        
        public void SubscribeOnTakeCoin(Action<int> subscribe)
        {
            _onTakeCoin += subscribe;
        }

        public void SubscribeOnDeath(Action subscribe)
        {
            _onDeath += subscribe;
        }

        public void TakeCoin(CoinView takenCoinView)
        {
            _takenCoinCount++;
            _onTakeCoinView?.Invoke(takenCoinView);
            _onTakeCoin?.Invoke(_takenCoinCount);
        }

        public void UpdatePosition(Vector2 position)
        {
            _position = position;
        }

        public void Death()
        {
            _takenCoinCount = 0;
            _onTakeCoin?.Invoke(_takenCoinCount);
            _onDeath?.Invoke();
        }
    }
}