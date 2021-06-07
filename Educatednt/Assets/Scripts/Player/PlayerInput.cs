using System;
using UnityEngine;


namespace Player {
    public static class PlayerInput {
        public enum Axis
        {
            HORIZONTAL,
            VERTICAL,
            MIN_HORIZONTAL,
            MIN_VERTICAL,
        }

        public enum Key
        {
            SPACE,
            ENTER,
            F,
        }

        public enum Mouse
        {
            LEFT,
            RIGHT
        }



        public static string GetAxis( Axis input ) {
            string axis;
            switch ( input ) {
                case Axis.HORIZONTAL:
                    axis = "Horizontal";
                    break;
                case Axis.VERTICAL:
                    axis = "Vertical";
                    break;
                case Axis.MIN_HORIZONTAL:
                    axis = "Minus_Horizontal";
                    break;
                case Axis.MIN_VERTICAL:
                    axis = "Minus_Vertical";
                    break;
                default:
                    axis = "";
                    break;
            }
            if ( axis == "" ) {
                Debug.LogWarning( $"{input.ToString()} is not a passable axis." );
            }
            return axis;
        }


        public static KeyCode GetKey( Key input ) {
            KeyCode key;
            switch ( input ) {
                case Key.SPACE:
                    key = KeyCode.Space;
                    break;
                case Key.ENTER:
                    key = KeyCode.Return;
                    break;
                case Key.F:
                    key = KeyCode.F;
                    break;
                default:
                    key = KeyCode.Escape;
                    break;
            }
            if ( key == KeyCode.Escape ) {
                Debug.LogWarning( $"{input.ToString()} is not a passable key, defaulting to Escape." );
            }
            return key;
        }


        public static int GetMouseButton( Mouse input ) {
            int mouse;
            switch ( input ) {
                case Mouse.LEFT:
                    mouse = 0;
                    break;
                case Mouse.RIGHT:
                    mouse = 1;
                    break;
                default:
                    mouse = -1;
                    break;
            }
            if ( mouse == -1 ) {
                Debug.LogWarning( $"{input.ToString()} is not a passable mouse button." );
            }
            return mouse;
        }
    }
}
