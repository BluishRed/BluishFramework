using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BluishFramework
{
    public interface IUpdateable
    {
        bool ShouldUpdate { get; set; }

        void Update(GameTime gameTime);
    }
}