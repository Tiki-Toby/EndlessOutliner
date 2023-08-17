using Client.Generation.Coins;
using UnityEngine;

namespace Configs.LogicConfigs
{
    [CreateAssetMenu(fileName = "CoinConfig", menuName = "Configs/Generation/CoinConfig")]
    public class CoinConfig : ScriptableObject
    {
        [SerializeField] private CoinView coinViewPrefab;
        [SerializeField] private float coinMoveTolerance;
        [SerializeField] private float baseSpeed;
        [SerializeField] private float acceleration;

        public CoinView CoinViewPrefab => coinViewPrefab;
        public float CoinMoveTolerance => coinMoveTolerance;
        public float BaseSpeed => baseSpeed;
        public float Acceleration => acceleration;
    }
}