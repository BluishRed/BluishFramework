using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace BluishFramework
{
    public abstract class DrawSystem : System
    {
        public DrawSystem(World world, params Type[] requiredComponents) : base(world, requiredComponents)
        {

        }

        /// <summary>
        /// Draws all the entities that match the signature of this <see cref="DrawSystem"/> from the <see cref="World"/>
        /// </summary>
        public void DrawEntities(SpriteBatch spriteBatch)
        {
            foreach (Entity entity in RegisteredEntities)
            {
                DrawEntity(spriteBatch, entity, World.GetComponents(entity));
            }
        }

        protected abstract void DrawEntity(SpriteBatch spriteBatch, Entity entity, ComponentCollection components);
    }
}