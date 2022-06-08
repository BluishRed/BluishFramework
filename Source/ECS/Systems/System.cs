using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace BluishFramework
{
    public abstract class System : IUpdateable
    {
        public bool ShouldUpdate { get; set; } = true;

        private HashSet<int> _registeredEntities;
        private readonly List<Type> _requiredComponents;

        public System()
        {
            _registeredEntities = new HashSet<int>();
            _requiredComponents = new List<Type>();
        }

        public void Update(GameTime gameTime)
        {

        }

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

        private bool Matches(Entity entity)
        {
            foreach (Type component in _requiredComponents)
            {
                if (entity.HasComponent(component))
                    return false;
            }
            return true;
        }
    }
}