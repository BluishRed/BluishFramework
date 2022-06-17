using System;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace BluishFramework
{
    public static class StateManager
    {
        private static Stack<State> _stateStack = new Stack<State>();
        private static State _initalState;

        public static void Initialise()
        {
            ChangeState<SplashScreen>();
        }

        public static void SetInitialState<T>() where T : State, new()
        {
            _initalState = new T();
        }

        public static void ChangeToInitialState()
        {
            if (_initalState != null)
                ChangeState(_initalState);
        }

        /// <summary>
        /// The state that is currently being processed
        /// </summary>
        public static State CurrentState
        {
            get { return _stateStack.Peek(); }
            private set { _stateStack.Push(value); }
        }

        public static void ChangeState<T>() where T : State, new()
        {
            CurrentState = new T();
        }

        private static void ChangeState(State state)
        {
            CurrentState = state;
        }
    }
}