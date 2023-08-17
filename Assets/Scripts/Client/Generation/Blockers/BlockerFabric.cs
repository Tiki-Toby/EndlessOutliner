using Tools.Ui.Fabric;
using UnityEngine;

namespace Client.Generation.Blockers
{
    public class BlockerFabric : IControllerFabric<BlockerController, int, Transform>
    {
        private readonly Transform _parent;
        private readonly Transform _blockerObjectPrefab;

        public BlockerFabric(Transform blockerObjectPrefab)
        {
            _blockerObjectPrefab = blockerObjectPrefab;
            _parent = new GameObject($"{blockerObjectPrefab.name}s").transform;
            _parent.position = Vector3.zero;
        }
        
        public BlockerController CreateController()
        {
            Transform blockerObjectView = Object.Instantiate(_blockerObjectPrefab, _parent);
            return new BlockerController(blockerObjectView);
        }
    }
}