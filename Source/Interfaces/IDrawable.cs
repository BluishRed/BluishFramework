using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BluishFramework
{
    public interface IDrawable
    {
        bool ShouldDraw { get; set; }

        void Draw(SpriteBatch spriteBatch);
    }
}