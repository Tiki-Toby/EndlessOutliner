using UnityEngine;

namespace Client.Generation.Borders
{
    public class BorderManager
    {
        private readonly Transform _borderPrefab;
        private Transform _upBorder, _downBorder;

        public BorderManager(Transform borderPrefab)
        {
            _borderPrefab = borderPrefab;
        }

        public void Init(Vector2 cameraSize)
        {
            Transform parent = new GameObject($"{_borderPrefab.name}s").transform;
            _upBorder = Object.Instantiate(_borderPrefab, Vector2.zero, Quaternion.identity, parent);
            _upBorder.localScale = new Vector3(cameraSize.x, 1, 1);
            _upBorder.position += Vector3.up * (cameraSize.y / 2f - 0.5f);
            
            _downBorder = Object.Instantiate(_borderPrefab, parent);
            _downBorder.localScale = new Vector3(cameraSize.x, 1, 1);
            _downBorder.position -= Vector3.up * (cameraSize.y / 2f - 0.5f);
        }
    }
}