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

        ContentManager _content;

        public State()
        {
            _content = ContentProvider.GetContentManager();
        }

        public abstract void AddEntities();

        public abstract void AddSystems();

        internal void Initialise()
        {
            AddEntities();
            AddSystems();
        }

        public void LoadContent()
        {
            LoadContent(_content);
        }

        public void UnloadContent()
        {
            _content.Unload();
        }
    }
}