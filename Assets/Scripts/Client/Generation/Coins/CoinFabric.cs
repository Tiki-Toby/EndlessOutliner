using Client.ReactiveModels;
using Configs.LogicConfigs;
using Tools.Ui.Fabric;
using UnityEngine;

namespace Client.Generation.Coins
{
    public class CoinFabric : IControllerFabric<CoinController, int, CoinView>
    {
        private readonly PlayerReactiveModel _playerReactiveModel;
        private readonly CoinConfig _coinConfig;
        private readonly Transform _parent;

        public CoinFabric(PlayerReactiveModel playerReactiveModel,
            CoinConfig coinConfig)
        {
            _playerReactiveModel = playerReactiveModel;
            _coinConfig = coinConfig;
            _parent = new GameObject($"{coinConfig.CoinViewPrefab.name}s").transform;
            _parent.position = Vector3.zero;
        }
        
        public CoinController CreateController()
        {
            CoinView coinView = Object.Instantiate(_coinConfig.CoinViewPrefab, _parent);
            CoinController coinController = new CoinController(_playerReactiveModel, _coinConfig);
            coinController.SetView(coinView);
            
            return coinController;
        }
    }
}