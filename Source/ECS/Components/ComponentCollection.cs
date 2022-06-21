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
    /// A collection of <see cref="Component"/>'s, where each <see cref="Type"/> of <see cref="Component"/> can only be stored once
    /// </summary>
    public class ComponentCollection
    {
        private Dictionary<Type, Component> _components;

        public ComponentCollection()
        {
            _components = new Dictionary<Type, Component>();
        }
        
        /// <summary>
        /// Adds <paramref name="component"/> to this <see cref="ComponentCollection"/>
        /// </summary>
        /// <param name="component">
        /// Component to add
        /// </param>
        public void AddComponent(Component component)
        {
            _components.TryAdd(component.GetType(), component);
        }

        /// <summary>
        /// Removes the <see cref="Component"/> of type <typeparamref name="T"/> from this <see cref="ComponentCollection"/>
        /// </summary>
        public void RemoveComponent<T>() where T : Component
        {
            _components.Remove(typeof(T));
        }

        /// <summary>
        /// Removes the <see cref="Component"/> of type <paramref name="component"/> from this <see cref="ComponentCollection"/>
        /// </summary>
        /// <param name="component"></param>
        public void RemoveComponent(Type component)
        {
            _components.Remove(component);
        }

        /// <summary>
        /// Returns the <see cref="Component"/> of type <typeparamref name="T"/> from this <see cref="ComponentCollection"/>
        /// </summary>
        public T GetComponent<T>() where T : Component
        {
            return _components[typeof(T)] as T;
        }

        /// <summary>
        /// Returns a new <see cref="ComponentCollection"/> with only the components from this <see cref="ComponentCollection"/> that match the <see cref="Type"/>'s of <paramref name="componentTypes"/>
        /// </summary>
        /// <param name="componentTypes">
        /// A list containing the <see cref="Type"/>'s of <see cref="Component"/>'s to put in the returned <see cref="ComponentCollection"/>
        /// </param>
        public ComponentCollection GetMatchingComponents(Type[] componentTypes)
        {
            ComponentCollection components = new ComponentCollection();

            foreach (Type componentType in componentTypes)
            {
                _components.TryGetValue(componentType, out Component component);
                components.AddComponent(component);
            }

            return components;
        }

        /// <summary>
        /// Returns <c>true</c> if this <see cref="ComponentCollection"/> contains a <see cref="Component"/> of type <typeparamref name="T"/>, <c>false</c> otherwise
        /// </summary>
        public bool HasComponent<T>() where T : Component
        {
            return _components.ContainsKey(typeof(T));
        }

        /// <summary>
        /// Returns <c>true</c> if this <see cref="ComponentCollection"/> contains a <see cref="Component"/> of type <paramref name="component"/>, <c>false</c> otherwise
        /// </summary>
        public bool HasComponent(Type component)
        {
            return _components.ContainsKey(component);
        }

        /// <summary>
        /// Returns <c>true</c> if this <see cref="ComponentCollection"/> contains all <see cref="Component"/>'s of of the respective types from <paramref name="components"/>, <c>false</c> otherwise
        /// </summary>
        public bool HasComponents(params Type[] components)
        {
            foreach (Type component in components)
            {
                if (!HasComponent(component))
                {
                    return false;
                }
            }

            return true;
        }
    }
}