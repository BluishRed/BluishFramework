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
    /// <summary>
    /// A unique ID containing a collection of components
    /// </summary>
    public class Entity
    {

        private Dictionary<Type, Component> _components;

        public int ID { get; private set; }

        public Entity(int id)
        {
            ID = id;
            _components = new Dictionary<Type, Component>();
        }

        /// <summary>
        /// Adds a <see cref="Component"/> to this entity
        /// </summary>
        /// <param name="component">
        /// The component to add
        /// </param>
        internal void AddComponent(Component component)
        {
            _components[component.GetType()] = component;
            OnComponentsChanged();
        }

        internal void AddComponents(params Component[] components)
        {
            foreach (Component component in components)
            {
                AddComponent(component);
            }
        }

        internal void RemoveComponent<T>()
        {
            _components.Remove(typeof(T));
            OnComponentsChanged();
        }

        protected void OnComponentsChanged()
        {
            
        }

        /// <summary>
        /// Returns the component of type <typeparamref name="T"/> if it exists
        /// </summary>
        /// <typeparam name="T">
        /// The type of component to be retrieved
        /// </typeparam>
        /// <exception cref="KeyNotFoundException">
        /// Thrown when a component of type <typeparamref name="T"/> does not exist for this entity
        /// </exception>
        public T GetComponent<T>() where T : Component
        {
            return (T)_components[typeof(T)];
        }

        /// <summary>
        /// Returns <c>true</c> if a component of type <typeparamref name="T"/> exists for this component, <c>false</c> otherwise
        /// </summary>
        /// <typeparam name="T">
        /// The queried component
        /// </typeparam>
        public bool HasComponent<T>() where T : Component
        {
            return _components.ContainsKey(typeof(T));
        }

        /// <summary>
        /// Returns <c>true</c> if a component of type <paramref name="componentType"/> exists for this component, <c>false</c> otherwise
        /// </summary>
        /// <param name="componentType">
        /// The type of component being queried
        /// </param>
        public bool HasComponent(Type componentType)
        {
            return _components.ContainsKey(componentType);
        }

        public static Entity operator +(Entity entity, Component component)
        {
            entity.AddComponent(component);
            return entity;
        }
    }
}