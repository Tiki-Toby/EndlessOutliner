using Client.MainPlayer;
using UnityEngine;

namespace Configs.Holders
{
    public interface IGameAssetData
    {
        public MainPlayerView GetMainLineView(int id);
        public Transform GetBlockerView(int id);
    }
}