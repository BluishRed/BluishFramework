using System;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace BluishFramework
{
    public class World
    {

        private Dictionary<int, Entity> _entities;
        private List<int> _entitiesToDelete;
        private Dictionary<Type, System> _systems;
        private int _currentID;

        public World()
        {
            _entities = new Dictionary<int, Entity>();
            _entitiesToDelete = new List<int>();
            _systems = new Dictionary<Type, System>();
        }

        public Entity AddEntity()
        {
            Entity entity = new Entity(_currentID);
            _entities[++_currentID] = entity;
            return entity;
        }

        public void DeleteEntity(int id)
        {
            _entitiesToDelete.Add(id);
        }

        public Entity GetEntity(int id)
        {
            return _entities[id];
        }

        public bool DoesEntityExist(int id)
        {
            return _entities.ContainsKey(id);
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

        private void UpdateEntityRegistration(Entity entity)
        {
            foreach (System system in _systems.Values)
            {
                system.UpdateEntityRegistration(entity);
            }
        }
    }
}