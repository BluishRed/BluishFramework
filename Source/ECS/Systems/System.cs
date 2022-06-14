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
    public abstract class System
    {

        private World _world;
        /// <summary>
        /// Entities that match the signature of this <see cref="System"/> from the <see cref="World"/>
        /// </summary>
        private HashSet<int> _registeredEntities;
        private readonly List<Type> _requiredComponents;

        public System(World world)
        {
            _registeredEntities = new HashSet<int>();
            _requiredComponents = new List<Type>();
            _world = world;
        }

        /// <summary>
        /// Updates all the entities that match the signature of this <see cref="System"/> from the <see cref="World"/>
        /// </summary>
        public void UpdateEntities(GameTime gameTime)
        {
            foreach (int id in _registeredEntities)
            {
                UpdateEntity(_world.GetEntity(id), gameTime);
            }
        }

        protected abstract void UpdateEntity(Entity entity, GameTime gameTime);

        public virtual void DeleteEntity(int id)
        {
            if (_registeredEntities.Contains(id))
            {
                _registeredEntities.Remove(id);
            }
        }

        /// <summary>
        /// Evaluates <paramref name="entity"/>'s components and adds it to this <see cref="System"/> if it meets the signature,
        /// Or removes it from this <see cref="System"/> if the entity was registered but no longer matches the signature
        /// </summary>
        public void UpdateEntityRegistration(Entity entity)
        {
            bool matches = Matches(entity);

            if (_registeredEntities.Contains(entity.ID))
            {
                if (!matches)
                    _registeredEntities.Remove(entity.ID);
            }
            else
            {
                if (matches)
                    _registeredEntities.Add(entity.ID);
            }
        }

        protected void AddRequiredComponent<T>() where T : Component
        {
            _requiredComponents.Add(typeof(T));
        }

        /// <returns>
        /// <c>true</c> if the entity matches this <see cref="System"/>'s signature, <c>false</c> otherwise
        /// </returns>
        private bool Matches(Entity entity)
        {
            foreach (Type component in _requiredComponents)
            {
                if (!entity.HasComponent(component))
                    return false;
            }
            return true;
        }
    }
}