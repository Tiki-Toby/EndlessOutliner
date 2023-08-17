using System.Collections.Generic;
using Client.Interfaces;
using Client.ReactiveModels;
using Configs.LogicConfigs;
using Tools.Ui;
using UnityEngine;

namespace Client.Generation.Coins
{
    public class CoinManager : IOuterLogicUpdate, IDistanceUpdate
    {
        private readonly ObjectControllerPool<CoinController, CoinView, int> _coinPool;
        private readonly List<int> _removableCoins;
        private readonly Vector2 _spawnAreaSize;

        private int _ids;
        private int _stage;

        public CoinManager(
            CoinConfig coinConfig,
            PlayerReactiveModel playerReactiveModel,
            Vector2 spawnAreaSize)
        {
            CoinFabric blockerFabric = new CoinFabric(playerReactiveModel, coinConfig);
            _coinPool = new ObjectControllerPool<CoinController, CoinView, int>(blockerFabric);
            _removableCoins = new List<int>(3);
            _spawnAreaSize = spawnAreaSize;
            
            _stage = 0;
            _ids = 0;
            CreateBlockerLine(0f);

            playerReactiveModel.SubscribeOnTakeCoinView(RemoveTakenCoin);
        }

        public void Update(float frameLength)
        {
            foreach (var coinControllersKeyPair in _coinPool)
            {
                coinControllersKeyPair.Value.Update(frameLength);
            }
        }

        public void UpdateDistance(IReadOnlyLevelStateData levelStateData)
        {
            if (levelStateData.Distance / _spawnAreaSize.x > _stage)
            {
                CreateBlockerLine(levelStateData.Distance);
            }
            
            float removingDistance = -_spawnAreaSize.x * 1.2f;
            foreach (var coinControllersKeyPair in _coinPool)
            {
                coinControllersKeyPair.Value.UpdateDistance(levelStateData);
                if (coinControllersKeyPair.Value.Transform.position.x <= removingDistance)
                    _removableCoins.Add(coinControllersKeyPair.Key);
            }
            
            foreach (var removableBlockerKey in _removableCoins)
            {
                _coinPool.ReleaseSlot(removableBlockerKey);
            }
            
            if(_removableCoins.Count > 0)
                _removableCoins.Clear();
        }

        private void CreateBlockerLine(float distance)
        {
            CoinController coinController = _coinPool.GetFreeElementController(_ids++);
            float y = Random.Range(-_spawnAreaSize.y, _spawnAreaSize.y);
            Vector3 spawnPosition = new Vector3(_spawnAreaSize.x * 1.7f, y);
            coinController.Init(spawnPosition);

            _stage++;
        }

        public void Reset()
        {
            _coinPool.CloseAll();
            _stage = 0;
            _ids = 0;
        }

        private void RemoveTakenCoin(CoinView coinView)
        {
            _coinPool.ReleaseSlot(coinView.ID);
        }
    }
}