using System;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace BluishFramework
{
    /// <summary>
    /// Static class containing all methods necessary to manipulate <see cref="State"/>'s
    /// </summary>
    public static class StateManager
    {
        private static Type _initalState;
        private static Stack<State> _stateStack = new Stack<State>();

        /// <summary>
        /// The state that is currently being processed
        /// </summary>
        public static State CurrentState
        {
            get { return _stateStack.Peek(); }
            private set 
            {
                _stateStack.Push(value);
            }
        }

        public static void Initialise()
        {
            ChangeToInitialState(); 
        }

        public static void SetInitialState(Type state)
        {
            if (state.IsSubclassOf(typeof(State)))
                _initalState = state;
        }

        public static void ChangeState<T>() where T : State, new()
        {
            CurrentState.UnloadContent();
            CurrentState = new T();
            CurrentState.Initialise();
            CurrentState.LoadContent();
        }

        private static void ChangeState(State state)
        {
            CurrentState.UnloadContent();
            CurrentState = state; 
            CurrentState.Initialise();
            CurrentState.LoadContent();
        }

        private static void ChangeToInitialState()
        {
            CurrentState = (State)Activator.CreateInstance(_initalState);
            CurrentState.Initialise();
            CurrentState.LoadContent();
        }
    }
}