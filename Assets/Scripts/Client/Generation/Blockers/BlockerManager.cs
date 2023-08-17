using System.Collections.Generic;
using Client.Interfaces;
using Configs.Holders;
using Configs.LogicConfigs;
using Tools.Ui;
using UnityEngine;

namespace Client.Generation.Blockers
{
    public class BlockerManager : IOuterLogicUpdate, IDistanceUpdate
    {
        private readonly IGameAssetData _gameAssetData;
        private readonly BlockerConfig _blockerConfig;
        private readonly ObjectControllerPool<BlockerController, Transform, int> _blockerPool;
        private readonly List<int> _removableBlockers;
        private readonly Vector2 _spawnAreaSize;

        private int _ids;
        private int _stage;

        public BlockerManager(
            IGameAssetData gameAssetData,
            BlockerConfig blockerConfig,
            Vector2 spawnAreaSize)
        {
            _gameAssetData = gameAssetData;
            _blockerConfig = blockerConfig;

            BlockerFabric blockerFabric = new BlockerFabric(_gameAssetData.GetBlockerView(0));
            _blockerPool = new ObjectControllerPool<BlockerController, Transform, int>(blockerFabric);
            _removableBlockers = new List<int>(3);
            _spawnAreaSize = spawnAreaSize;
            
            _stage = 0;
            _ids = 0;
            CreateBlockerLine(0f);
        }

        public void Update(float frameLength)
        {
            foreach (var blockerControllersKeyPair in _blockerPool)
            {
                blockerControllersKeyPair.Value.Update(frameLength);
            }
        }

        public void UpdateDistance(IReadOnlyLevelStateData levelStateData)
        {
            if (levelStateData.Distance / _spawnAreaSize.x > _stage)
            {
                CreateBlockerLine(levelStateData.Distance);
            }
            
            float removingDistance = -_spawnAreaSize.x * 1.2f;
            foreach (var blockerControllersKeyPair in _blockerPool)
            {
                blockerControllersKeyPair.Value.UpdateDistance(levelStateData);
                if (blockerControllersKeyPair.Value.Transform.position.x <= removingDistance)
                    _removableBlockers.Add(blockerControllersKeyPair.Key);
            }
            
            foreach (var removableBlockerKey in _removableBlockers)
            {
                _blockerPool.ReleaseSlot(removableBlockerKey);
            }
            
            if(_removableBlockers.Count > 0)
                _removableBlockers.Clear();
        }

        private void CreateBlockerLine(float distance)
        {
            float speed = _blockerConfig.BaseSpeed + _blockerConfig.DistanceToSpeedImpact * distance;
            float x = _spawnAreaSize.x * 1.2f;
            for (int i = 0; i < _blockerConfig.BlockersInLineCount; i++)
            {
                BlockerController blockerController = _blockerPool.GetFreeElementController(_ids++);
                float y = Random.Range(-_spawnAreaSize.y, _spawnAreaSize.y);
                Vector2 spawnPosition = new Vector2(x, y);
                blockerController.Init(spawnPosition, _blockerConfig.MaxOffset, speed);
            }
            
            _stage++;
        }

        public void Reset()
        {
            _blockerPool.CloseAll();
            _stage = 0;
            _ids = 0;
        }
    }
}