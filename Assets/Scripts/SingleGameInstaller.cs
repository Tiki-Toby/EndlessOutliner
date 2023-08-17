using Client;
using Client.CameraSystem;
using Configs.Holders;
using Configs.Holders.Concrete;
using Configs.LogicConfigs;
using Ui;
using UnityEngine;

public class SingleGameInstaller : MonoBehaviour
{
    [Header("Views")] 
    [SerializeField] private CameraView cameraView;
    [SerializeField] private Transform borderPrefab;
    [SerializeField] private MainOverlayView mainOverlayView;
    
    [Header("Holders")]
    [SerializeField] private MainLineObjectsHolder mainLineObjectsHolder;
    [SerializeField] private BlockerObjectsHolder blockerObjectsHolder;

    [Header("Configs")]
    [SerializeField] private BlockerConfig blockerConfig;
    [SerializeField] private CoinConfig coinConfig;
    [SerializeField] private CommonLevelConfig commonLevelConfig;
    
    private GameSimulator _gameSimulator;

    private void Start()
    {
        IGameAssetData gameAssetData = new GameAssetData(blockerObjectsHolder, mainLineObjectsHolder);
        _gameSimulator = new GameSimulator(cameraView, borderPrefab, mainOverlayView,
            gameAssetData, blockerConfig, coinConfig, commonLevelConfig);
    }

    private void Update()
    {
        _gameSimulator.Update(Time.deltaTime);
    }
}
