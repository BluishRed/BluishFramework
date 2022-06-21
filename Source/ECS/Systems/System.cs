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
    public class System
    {
        protected Type[] RequiredComponents { get; private set; }
        protected World World { get; private set; }
        private protected HashSet<int> RegisteredEntities { get; private set; }

        public System(World world, params Type[] requiredComponents)
        {
            RegisteredEntities = new HashSet<int>();
            RequiredComponents = requiredComponents;
            World = world;
        }

        public void DeleteEntity(int id)
        {
            if (RegisteredEntities.Contains(id))
            {
                RegisteredEntities.Remove(id);
            }
        }

        /// <summary>
        /// Evaluates <paramref name="entity"/>'s components and adds it to this <see cref="UpdateSystem"/> if it meets the signature,
        /// Or removes it from this <see cref="UpdateSystem"/> if the entity was registered but no longer matches the signature
        /// </summary>
        public void UpdateEntityRegistration(int entity)
        {
            bool matches = Matches(entity);

            if (RegisteredEntities.Contains(entity))
            {
                if (!matches)
                    RegisteredEntities.Remove(entity);
            }
            else
            {
                if (matches)
                    RegisteredEntities.Add(entity);
            }
        }

        /// <returns>
        /// <c>true</c> if the entity matches this <see cref="UpdateSystem"/>'s signature, <c>false</c> otherwise
        /// </returns>
        private bool Matches(int entity)
        {
            return World.GetComponents(entity).HasComponents(RequiredComponents);
        }
    }
}