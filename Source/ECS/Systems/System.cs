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
    public abstract class System : EntityRegister
    {
        public System(World world, params Type[] requiredComponents) : base(world, requiredComponents)
        {
             
        }

        /// <summary>
        /// Updates all the entities that match the signature of this <see cref="System"/> from the <see cref="World"/>
        /// </summary>
        public void UpdateEntities(GameTime gameTime)
        {
            foreach (int entity in RegisteredEntities)
            {
                UpdateEntity(gameTime, World.GetComponents(entity).GetComponents(RequiredComponents));
            }
        }

        protected abstract void UpdateEntity(GameTime gameTime, List<Component> components);
    }
}