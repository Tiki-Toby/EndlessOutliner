using UnityEngine;

namespace Client.InputSystem
{
    public class InputManager : IInputManager
    {
        public bool IsJump => Input.GetKey(KeyCode.Space);
    }
}