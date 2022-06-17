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
    public class World
    {
        private Dictionary<int, ComponentCollection> _entities;
        private Dictionary<Type, System> _systems;
        private int _currentID;

        public World()
        {
            _systems = new Dictionary<Type, System>();
            _entities = new Dictionary<int, ComponentCollection>();
        }

        public void AddEntity(params Component[] components)
        {
            ComponentCollection componentCollection = new ComponentCollection();
            
            foreach(Component component in components)
            {
                componentCollection.AddComponent(component);
            }

            _entities.Add(_currentID, componentCollection);

            UpdateEntityRegistration(_currentID++);
        }

        public void DeleteEntity(int entity)
        {
            
        }

        public void AddSystem<T>() where T : System
        {
            _systems[typeof(T)] = (T)Activator.CreateInstance(typeof(T), this);
        }

        public T GetSystem<T>() where T : System
        {
            return (T)_systems[typeof(T)];
        }

        public void Update(GameTime gameTime)
        {
            foreach (System system in _systems.Values)
            {
                system.UpdateEntities(gameTime);
            }
        }

        private void UpdateEntityRegistration(int entity)
        {
            foreach (System system in _systems.Values)
            {
                system.UpdateEntityRegistration(entity);
            }
        }
    }
}