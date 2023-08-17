using Client.MainPlayer;
using Configs.Holders.Concrete;
using UnityEngine;

namespace Configs.Holders
{
    public class GameAssetData : IGameAssetData
    {
        private readonly BlockerObjectsHolder _blockerObjectsHolder;
        private readonly MainLineObjectsHolder _mainLineObjectsHolder;

        public GameAssetData(BlockerObjectsHolder blockerObjectsHolder, 
            MainLineObjectsHolder mainLineObjectsHolder)
        {
            _blockerObjectsHolder = blockerObjectsHolder;
            _mainLineObjectsHolder = mainLineObjectsHolder;
        }

        public MainPlayerView GetMainLineView(int id)
        {
            return _mainLineObjectsHolder.GetObjectWithId(id);
        }

        public Transform GetBlockerView(int id)
        {
            return _blockerObjectsHolder.GetObjectWithId(id);
        }
    }
}