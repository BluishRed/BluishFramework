using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BluishFramework
{
    public static class Input
    {
        private static Dictionary<Keys, KeyPressState> _pressedKeys;
        private static Keys[] _pressedKeysLastFrame;

        static Input()
        {
            _pressedKeys = new Dictionary<Keys, KeyPressState>();
        }

        public static void Update()
        {
            _pressedKeys.Clear();
            Keys[] keys = Keyboard.GetState().GetPressedKeys();
            foreach (Keys key in keys)
            {
                _pressedKeys.Add(key, Array.IndexOf(_pressedKeysLastFrame, key) == -1 ? KeyPressState.JustPressed : KeyPressState.Held);
            }
            _pressedKeysLastFrame = keys;
        }

        public static bool IsKeyPressed(Keys key)
        {
            return _pressedKeys.TryGetValue(key, out _);
        }

        public static bool IsKeyJustPressed(Keys key)
        {
            if (_pressedKeys.TryGetValue(key, out KeyPressState currentState))
            {
                return currentState == KeyPressState.JustPressed;
            }
            return false;
        }

        public static bool IsKeyInState(Keys key, KeyPressState keyPressState)
        {
            if (_pressedKeys.TryGetValue(key, out KeyPressState currentState))
            {
                if (keyPressState == KeyPressState.Held)
                {
                    return true;
                }
                else
                {
                    return keyPressState == currentState;
                }     
            }
            return false;
        }

        public static bool IsKeyInState((Keys, KeyPressState) inputCondition)
        {
            return IsKeyInState(inputCondition.Item1, inputCondition.Item2);
        }
    }
}