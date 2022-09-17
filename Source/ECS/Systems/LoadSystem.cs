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
    public abstract class LoadSystem : System
    {
        public LoadSystem(World world, params Type[] requiredComponents) : base(world, requiredComponents)
        {

        }

        /// <summary>
        /// Loads all the entities that match the signature of this <see cref="LoadSystem"/> from the <see cref="World"/>
        /// </summary>
        public void LoadEntities(ContentManager content)
        {
            foreach (Entity entity in RegisteredEntities)
            {
                LoadEntity(content, entity, World.GetComponents(entity).FilterCollection(RequiredComponents));
            }
        }

        protected abstract void LoadEntity(ContentManager content, Entity entity, ComponentCollection components);
    }
}