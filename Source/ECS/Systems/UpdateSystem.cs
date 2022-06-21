using System;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace BluishFramework
{
    public abstract class UpdateSystem : System
    {
        public UpdateSystem(World world, params Type[] requiredComponents) : base(world, requiredComponents)
        {
             
        }

        /// <summary>
        /// Updates all the entities that match the signature of this <see cref="UpdateSystem"/> from the <see cref="World"/>
        /// </summary>
        public void UpdateEntities(/*GameTime gameTime*/)
        {
            foreach (int entity in RegisteredEntities)
            {
                UpdateEntity(/*gameTime, */World.GetComponents(entity).GetMatchingComponents(RequiredComponents));
            }
        }

        protected abstract void UpdateEntity(/*GameTime gameTime, */ComponentCollection components);
    }
}