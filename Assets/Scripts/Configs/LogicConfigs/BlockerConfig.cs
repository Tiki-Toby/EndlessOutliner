using UnityEngine;

namespace Configs.LogicConfigs
{
    [CreateAssetMenu(fileName = "BlockerConfig", menuName = "Configs/Generation/BlockerConfig")]
    public class BlockerConfig : ScriptableObject
    {
        [SerializeField] private int blockersInLineCount;
        [SerializeField] private float maxOffset;
        [SerializeField] private float baseSpeed;
        [SerializeField] private float distanceToSpeedImpact;

        public int BlockersInLineCount => blockersInLineCount;
        public float MaxOffset => maxOffset;
        public float BaseSpeed => baseSpeed;
        public float DistanceToSpeedImpact => distanceToSpeedImpact;
    }
}