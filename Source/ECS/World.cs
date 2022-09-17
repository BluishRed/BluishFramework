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
    /// <summary>
    /// A container for entities and the <see cref="System"/>'s that operate on them
    /// </summary>
    public class World
    {
        private Dictionary<Entity, ComponentCollection> _entities;
        private List<Entity> _entitesToRemove;
        private Stack<Entity> _entitiesToReuse;
        private Dictionary<Type, UpdateSystem> _updateSystems;
        private Dictionary<Type, DrawSystem> _drawSystems;
        private Dictionary<Type, LoadSystem> _loadSystems;
        private int _currentID;

        public World()
        {
            _drawSystems = new Dictionary<Type, DrawSystem>();
            _updateSystems = new Dictionary<Type, UpdateSystem>();
            _loadSystems = new Dictionary<Type, LoadSystem>();
            _entities = new Dictionary<Entity, ComponentCollection>();
            _entitesToRemove = new List<Entity>();
            _entitiesToReuse = new Stack<Entity>();
            _currentID = 0;
        }
         
        /// <summary>
        /// Adds an entity to this <see cref="World"/>
        /// </summary>
        /// <param name="components">
        /// Components that the entity has
        /// </param>
        public void AddEntity(params Component[] components)
        {
            ComponentCollection componentCollection = new ComponentCollection();

            foreach(Component component in components)
            {
                componentCollection.AddComponent(component);
            }

            if (_entitiesToReuse.Count > 0)
            {
                _entities.Add(_entitiesToReuse.Pop(), componentCollection);
            }
            else
            {
                _entities.Add(_currentID++, componentCollection);
            }
        }

        /// <summary>
        /// Removes <paramref name="entity"/> from this <see cref="World"/>
        /// </summary>
        /// <param name="entity">
        /// Entity to remove
        /// </param>
        public void RemoveEntity(Entity entity)
        {
            _entitesToRemove.Add(entity);
        }

        /// <summary>
        /// Adds <paramref name="component"/> to <paramref name="entity"/>
        /// </summary>
        public void AddComponent(Entity entity, Component component)
        {
            _entities[entity].AddComponent(component);
            UpdateEntityRegistrationForAllSystems(entity);
        }


        /// <summary>
        /// Returns <paramref name="entity"/>'s <see cref="Component"/>'s as a <see cref="ComponentCollection"/>
        /// </summary>
        public ComponentCollection GetComponents(Entity entity)
        {
            _entities.TryGetValue(entity, out ComponentCollection componentCollection);
            return componentCollection;
        }
        
        /// <summary>
        /// Adds <paramref name="system"/> to this world
        /// </summary>
        public void AddSystem(System system)
        {
            if (system as DrawSystem is not null)
            {
                _drawSystems.TryAdd(system.GetType(), system as DrawSystem);
            }
            else if (system as UpdateSystem is not null)
            {
                _updateSystems.TryAdd(system.GetType(), system as UpdateSystem);
            }
            else if (system as LoadSystem is not null)
            {
                _loadSystems.TryAdd(system.GetType(), system as LoadSystem);
            }

            foreach (Entity entity in _entities.Keys)
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

        /// <summary>
        /// Runs every <see cref="UpdateSystem"/> in this <see cref="World"/>
        /// </summary>
        public virtual void Update(GameTime gameTime)
        {
            foreach (UpdateSystem updateSystem in _updateSystems.Values)
            {
                updateSystem.UpdateEntities(gameTime);
            }

            foreach (Entity entity in _entitesToRemove)
            {
                DestroyEntity(entity);
            }

            _entitesToRemove.Clear();
        }

        /// <summary>
        /// Runs every <see cref="DrawSystem"/> in this <see cref="World"/>
        /// </summary>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            foreach (DrawSystem drawSystem in _drawSystems.Values)
            {
                drawSystem.DrawEntities(spriteBatch);
            }
        }

        /// <summary>
        /// Runs every <see cref="LoadSystem"/> in this <see cref="World"/>
        /// </summary>
        public virtual void LoadContent(ContentManager content)
        {
            foreach (LoadSystem loadSystem in _loadSystems.Values)
            {
                loadSystem.LoadEntities(content);
            }
        }
        
        private void DestroyEntity(Entity entity)
        {
            _entities.Remove(entity);
            foreach (UpdateSystem updateSystem in _updateSystems.Values)
            {
                updateSystem.UnregisterEntity(entity);
            }
            foreach (DrawSystem drawSystem in _drawSystems.Values)
            {
                drawSystem.UnregisterEntity(entity);
            }
            foreach (LoadSystem loadSystem in _loadSystems.Values)
            {
                loadSystem.UnregisterEntity(entity);
            }
            _entitiesToReuse.Push(entity);
        }

        private void UpdateEntityRegistrationForAllSystems(Entity entity)
        {
            foreach (UpdateSystem updateSystem in _updateSystems.Values)
            {
                updateSystem.UpdateEntityRegistration(entity);
            }
            foreach (DrawSystem drawSystem in _drawSystems.Values)
            {
                drawSystem.UpdateEntityRegistration(entity);
            }
            foreach (LoadSystem loadSystem in _loadSystems.Values)
            {
                loadSystem.UpdateEntityRegistration(entity);
            }
        }
    }
}