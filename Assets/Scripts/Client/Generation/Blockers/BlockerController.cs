using Client.Interfaces;
using Tools.Ui;
using UnityEngine;

namespace Client.Generation.Blockers
{
    public class BlockerController : IPoolableElementController<int, Transform>, IOuterLogicUpdate, IDistanceUpdate
    {
        private int _id;
        private float _maxOffset;
        private float _offset;
        private float _speed;
        private int _direction;
        
        private Transform _blockerObjectTransform;

        public Transform Transform => _blockerObjectTransform;
        public int ID => _id;
        
        public BlockerController(Transform blockerObjectTransform)
        {
            _blockerObjectTransform = blockerObjectTransform;
        }
        
        public void SetView(Transform view)
        {
            _blockerObjectTransform = view;
        }

        public void Open(int key)
        {
            _id = key;
        }

        public void Init(Vector2 position, float maxOffset, float speed)
        {
            _direction = 1;
            _maxOffset = maxOffset * 2f;
            float additionalOffset = Random.Range(0, maxOffset);
            _offset = maxOffset + additionalOffset;
            _speed = speed * Random.Range(0.85f, 1.15f);
            _blockerObjectTransform.position = position + Vector2.up * additionalOffset;
        }
        
        public void Update(float frameLength)
        {
            float deltaMove = frameLength * _speed;
            _blockerObjectTransform.position += Vector3.up * _direction * deltaMove;
            _offset += deltaMove;
            if(_offset >= _maxOffset)
            {
                _direction *= -1;
                _offset = 0f;
            }
        }

        public void UpdateDistance(IReadOnlyLevelStateData levelStateData)
        {
            _blockerObjectTransform.position -= Vector3.right * levelStateData.DeltaMove;
        }

        public void Close()
        {
            _blockerObjectTransform.gameObject.SetActive(false);
        }
        
        public void Dispose()
        {
            Object.Destroy(_blockerObjectTransform.gameObject);
        }
    }
}