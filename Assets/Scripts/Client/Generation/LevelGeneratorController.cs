using Client.Generation.Blockers;
using Client.Generation.Coins;
using Client.Interfaces;
using Client.ReactiveModels;
using Configs.Holders;
using Configs.LogicConfigs;
using UnityEngine;

namespace Client.Generation
{
    public class LevelGeneratorController : IOuterLogicUpdate
    {
        private readonly CommonLevelConfig _commonLevelConfig;
        private readonly LevelStateData _levelStateData;
        private readonly BlockerManager _blockerManager;
        private readonly CoinManager _coinManager;
        private float _distance;

        public IReadOnlyLevelStateData LevelStateData => _levelStateData;
        
        public LevelGeneratorController(
            IGameAssetData gameAssetData,
            PlayerReactiveModel playerReactiveModel,
            BlockerConfig blockerConfig,
            CoinConfig coinConfig,
            CommonLevelConfig commonLevelConfig,
            Vector2 viewPortSize)
        {
            _commonLevelConfig = commonLevelConfig;
            _levelStateData = new LevelStateData();

            Vector2 spawnAreaSize = new Vector2(viewPortSize.x * commonLevelConfig.SpawnAreaFactor.x / 2f,
                viewPortSize.y * commonLevelConfig.SpawnAreaFactor.y / 2f);
            _blockerManager = new BlockerManager(gameAssetData, blockerConfig, spawnAreaSize);
            _coinManager = new CoinManager(coinConfig, playerReactiveModel, spawnAreaSize);
        }
        
        public void Update(float frameLength)
        {
            float speed = _commonLevelConfig.BaseSpeed + _distance * _commonLevelConfig.DistanceToSpeedImpact;
            float deltaMoveDistance = speed * frameLength;
            _levelStateData.UpdateData(deltaMoveDistance);
            
            _blockerManager.Update(frameLength);
            _blockerManager.UpdateDistance(_levelStateData);
            
            _coinManager.Update(frameLength);
            _coinManager.UpdateDistance(_levelStateData);
        }

        public void Reset()
        {
            _levelStateData.Reset();
            _blockerManager.Reset();
            _coinManager.Reset();
        }
    }
}