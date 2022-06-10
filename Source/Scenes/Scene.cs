using System;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace BluishFramework
{
    public class Scene
    {

        private Dictionary<int, Entity> _entities;
        private Dictionary<Type, System> _systems;
        private List<int> _entitiesToDelete;
        private int _currentID;

        public Scene()
        {
            _entities = new Dictionary<int, Entity>();
            _systems = new Dictionary<Type, System>();
            _entitiesToDelete = new List<int>();
        }

        public Entity CreateEntity()
        {
            Entity entity = new Entity(_currentID);
            _entities[_currentID] = entity;
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
    }
}