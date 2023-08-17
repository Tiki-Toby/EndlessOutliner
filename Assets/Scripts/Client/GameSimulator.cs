using Client.CameraSystem;
using Client.Generation;
using Client.Generation.Borders;
using Client.InputSystem;
using Client.Interfaces;
using Client.MainPlayer;
using Client.ReactiveModels;
using Configs.Holders;
using Configs.LogicConfigs;
using Ui;
using UnityEngine;

namespace Client
{
    public class GameSimulator : IOuterLogicUpdate
    {
        private readonly IGameAssetData _gameAssetData;
        private readonly LevelGeneratorController _levelGeneratorController;
        private readonly BorderManager _borderManager;
        
        private readonly IInputManager _inputManager;
        private readonly PlayerReactiveModel _playerReactiveModel;
        private readonly MainPlayerController _mainPlayerController;

        private readonly MainOverlayController _mainOverlayController;
        
        private readonly Vector2 _cameraSize;

        private bool _isGame;

        public GameSimulator(
            CameraView cameraView, 
            Transform borderPrefab,
            MainOverlayView mainOverlayView,
            IGameAssetData gameAssetData,
            BlockerConfig blockerConfig,
            CoinConfig coinConfig,
            CommonLevelConfig commonLevelConfig)
        {
            float screenAspect = (float) Screen.width / (float) Screen.height;
            float camHeight = 2 * cameraView.MainCamera.orthographicSize;
            float camWidth = screenAspect * camHeight;
            _cameraSize = new Vector2(camWidth, camHeight);

            _playerReactiveModel = new PlayerReactiveModel();
            _playerReactiveModel.SubscribeOnDeath(Lose);

            _gameAssetData = gameAssetData;
            _levelGeneratorController = new LevelGeneratorController(gameAssetData, _playerReactiveModel, 
                blockerConfig, coinConfig, commonLevelConfig, _cameraSize);
            _borderManager = new BorderManager(borderPrefab);
            _borderManager.Init(_cameraSize);
            
            _inputManager = new InputManager();
            _mainPlayerController = new MainPlayerController(_gameAssetData.GetMainLineView(0),
                _playerReactiveModel, _inputManager, 
                _cameraSize, 90f);

            _mainOverlayController = new MainOverlayController(_playerReactiveModel, mainOverlayView);
        }
        
        public void Update(float frameLength)
        {
            if(_isGame)
            {
                _levelGeneratorController.Update(frameLength);
                _mainPlayerController.Update(frameLength);
            }
            else
            {
                _isGame = _inputManager.IsJump;
                if (_isGame)
                {
                    _mainOverlayController.SetPreviewPanelActive(false);
                    _mainPlayerController.Start();
                }
            }
        }

        private void Lose()
        {
            _isGame = false;
            _levelGeneratorController.Reset();
            _mainPlayerController.Reset();
            _mainOverlayController.SetPreviewPanelActive(true);
        }
    }
}