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
    public class ComponentCollection
    {
        private Dictionary<Type, Component> _components;

        public ComponentCollection()
        {
            _components = new Dictionary<Type, Component>();
        }

        public void AddComponent(Component component)
        {
            _components.TryAdd(component.GetType(), component);
        }

        public void RemoveComponent<T>() where T : Component
        {
            _components.Remove(typeof(T));
        }

        public void RemoveComponent(Type component)
        {
            _components.Remove(component);
        }

        public T GetComponent<T>() where T : Component
        {
            // TODO: Ensure component exists
            return _components[typeof(T)] as T;
        }

        public List<Component> GetComponents(Type[] componentTypes)
        {
            List<Component> components = new List<Component>();

            foreach (Type componentType in componentTypes)
            {
                _components.TryGetValue(componentType, out Component component);
                components.Add(component);
            }

            return components;
        }

        public bool HasComponent<T>() where T : Component
        {
            return _components.ContainsKey(typeof(T));
        }

        public bool HasComponent(Type component)
        {
            return _components.ContainsKey(component);
        }

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