using Client.InputSystem;
using Client.Interfaces;
using Client.ReactiveModels;
using UnityEngine;

namespace Client.MainPlayer
{
    public class MainPlayerController : IOuterLogicUpdate
    {
        private readonly MainPlayerView _view;
        private readonly PlayerReactiveModel _playerReactiveModel;
        private readonly IInputManager _inputManager;
        private readonly float _acceleration;

        private float _speed;

        public MainPlayerController(MainPlayerView prefab,
            PlayerReactiveModel playerReactiveModel,
            IInputManager inputManager, 
            Vector2 cameraSize, float acceleration)
        {
            _view = Object.Instantiate(prefab);
            _view.transform.position = Vector3.left * cameraSize.x / 3f;
            _view.Init(playerReactiveModel);
            _inputManager = inputManager;

            _playerReactiveModel = playerReactiveModel;
            
            _speed = 0f;
            _acceleration = acceleration;
        }

        public void Start()
        {
            _view.PlayerParticleSystem.Play();
        }

        public void Update(float frameLength)
        {
            if(_inputManager.IsJump)
            {
                _speed += _acceleration * frameLength;
            }
            else
            {
                _speed -= _acceleration * frameLength;
            }

            Vector2 newPosition = _view.transform.position + Vector3.up * _speed * frameLength;
            _view.PlayerRigidbody.MovePosition(newPosition);
            _playerReactiveModel.UpdatePosition(newPosition);
        }

        public void Reset()
        {
            _speed = 0;
            Vector3 position = _view.transform.position;
            _view.PlayerRigidbody.MovePosition(new Vector3(position.x, 0f, position.z));
            _view.PlayerParticleSystem.Stop();
        }
    }
}