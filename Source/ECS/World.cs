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
        private List<int> _entitesToRemove;
        private Dictionary<Type, System> _systems;
        private int _currentID;

        public World()
        {
            _systems = new Dictionary<Type, System>();
            _entities = new Dictionary<int, ComponentCollection>();
            _entitesToRemove = new List<int>();
            _currentID = 0;
        }

        public void AddEntity(params Component[] components)
        {
            ComponentCollection componentCollection = new ComponentCollection();
            
            foreach(Component component in components)
            {
                componentCollection.AddComponent(component);
            }

            _entities.Add(_currentID++, componentCollection);
        }

        public void RemoveEntity(int entity)
        {
            _entitesToRemove.Add(entity);
        }

        public void AddComponent(int entity, Component component)
        {
            _entities[entity].AddComponent(component);
            UpdateEntityRegistration(entity);
        }

        public ComponentCollection GetComponents(int entity)
        {
            // TODO: Ensure entity exists
            return _entities[entity];
        }

        public void AddSystem<T>() where T : System
        {
            _systems[typeof(T)] = (T)Activator.CreateInstance(typeof(T), this);
        }

        public T GetSystem<T>() where T : System
        {
            return (T)_systems[typeof(T)];
        }

        public void RemoveSystem<T>() where T : System
        {
            _systems.Remove(typeof(T));
        }

        public void Update(GameTime gameTime)
        {
            foreach (System system in _systems.Values)
            {
                system.UpdateEntities(gameTime);
            }

            foreach (int entity in _entitesToRemove)
            {
                DestroyEntity(entity);
            }
        }

        private void DestroyEntity(int entity)
        {
            _entitesToRemove.Remove(entity);
            _entities.Remove(entity);
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