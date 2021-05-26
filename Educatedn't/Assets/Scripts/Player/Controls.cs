using System;
using UnityEngine;


namespace Player {
    [CreateAssetMenu( fileName = "Controls", menuName = "ScriptableObjects/Controls", order = 1 )]
    public class Controls : ScriptableObject {
        public PlayerInput.Axis Forward => _forward;
        public PlayerInput.Axis Side => _sideways;
        public PlayerInput.Key Jump => _jumpButton;
        public PlayerInput.Mouse Attack => _attackButton;


        [SerializeField]
        private PlayerInput.Axis _forward = PlayerInput.Axis.HORIZONTAL;

        [SerializeField]
        private PlayerInput.Axis _sideways = PlayerInput.Axis.VERTICAL;

        [SerializeField]
        private PlayerInput.Key _jumpButton = PlayerInput.Key.SPACE;

        [SerializeField]
        private PlayerInput.Mouse _attackButton = PlayerInput.Mouse.LEFT;
    }
}
