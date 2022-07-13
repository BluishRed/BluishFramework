using System;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace BluishFramework
{
    public abstract class State : World
    {
        protected ContentManager Content { get; private set; }

        public State()
        {
            Content = ContentProvider.GetContentManager();
        }

        public virtual void LoadContent()
        {
            LoadContent(Content);
        }

        public abstract void AddEntities();

        public abstract void AddSystems();

        internal void Initialise()
        {
            AddEntities();
            AddSystems();
        }

        public void UnloadContent()
        {
            Content.Unload();
        }
    }
}