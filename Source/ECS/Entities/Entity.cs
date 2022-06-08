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
    public class Entity
    {
        private Dictionary<Type, Component> _components;

        public int ID { get; private set; }

        public Entity(int id)
        {
            ID = id;
            _components = new Dictionary<Type, Component>();
        }

        internal void AddComponent(Component component)
        {
            _components[component.GetType()] = component;
        }

        public T GetComponent<T>() where T : Component
        {
            return (T)_components[typeof(T)];
        }

        public bool HasComponent<T>() where T : Component
        {
            return _components.ContainsKey(typeof(T));
        }

        public bool HasComponent(Type componentType)
        {
            return _components.ContainsKey(componentType);
        }
    }
}