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
        private Dictionary<Type, UpdateSystem> _updateSystems;
        private Dictionary<Type, DrawSystem> _drawSystems;
        private int _currentID;

        public World()
        {
            _drawSystems = new Dictionary<Type, DrawSystem>();
            _updateSystems = new Dictionary<Type, UpdateSystem>();
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
            UpdateEntityRegistrationForAllSystems(entity);
        }

        public ComponentCollection GetComponents(int entity)
        {
            _entities.TryGetValue(entity, out ComponentCollection componentCollection);
            return componentCollection;
        }

        public void AddSystem<T>() where T : System
        {
            T system = (T)Activator.CreateInstance(typeof(T), this);

            //_drawSystems.TryAdd(system.GetType(), system as DrawSystem);
            _updateSystems.TryAdd(system.GetType(), system as UpdateSystem);

            foreach (int entity in _entities.Keys)
            {
                system.UpdateEntityRegistration(entity);
            }
        }

        //public T GetSystem<T>() where T : System
        //{
        //    return (T)_systems[typeof(T)];
        //}

        //public void RemoveSystem<T>() where T : System
        //{
        //    _systems.Remove(typeof(T));
        //}

        public void Update(/*GameTime gameTime*/)
        {
            foreach (UpdateSystem updateSystem in _updateSystems.Values)
            {
                updateSystem.UpdateEntities(/*gameTime*/);
            }

            foreach (int entity in _entitesToRemove)
            {
                DestroyEntity(entity);
            }

            _entitesToRemove.Clear();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (DrawSystem drawSystem in _drawSystems.Values)
            {
                drawSystem.DrawEntities(spriteBatch);
            }
        }

        private void DestroyEntity(int entity)
        {
            _entities.Remove(entity);
            foreach (UpdateSystem updateSystem in _updateSystems.Values)
            {
                updateSystem.RemoveEntity(entity);
            }
            foreach (DrawSystem drawSystem in _drawSystems.Values)
            {
                drawSystem.RemoveEntity(entity);
            }
        }

        private void UpdateEntityRegistrationForAllSystems(int entity)
        {
            foreach (UpdateSystem updateSystem in _updateSystems.Values)
            {
                updateSystem.UpdateEntityRegistration(entity);
            }
            foreach (DrawSystem drawSystem in _drawSystems.Values)
            {
                drawSystem.UpdateEntityRegistration(entity);
            }
        }
    }
}