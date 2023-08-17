using UnityEngine;

namespace Configs.LogicConfigs
{
    [CreateAssetMenu(fileName = "CommonLevelConfig", menuName = "Configs/Generation/CommonLevelConfig")]
    public class CommonLevelConfig : ScriptableObject
    {
        [SerializeField] private float baseSpeed;
        [SerializeField] private float distanceToSpeedImpact;
        [SerializeField] private Vector2 spawnAreaFactor;

        public float BaseSpeed => baseSpeed;
        public float DistanceToSpeedImpact => distanceToSpeedImpact;
        public Vector2 SpawnAreaFactor => spawnAreaFactor;
    }
}