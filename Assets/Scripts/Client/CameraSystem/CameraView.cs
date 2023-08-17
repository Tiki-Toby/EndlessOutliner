using UnityEngine;

namespace Client.CameraSystem
{
    public class CameraView : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;

        public Camera MainCamera => mainCamera;
    }
}