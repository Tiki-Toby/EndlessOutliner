using System;
using System.Collections.Generic;
using UnityEngine;

namespace Configs.Holders
{
    public abstract class BaseObjectHolder<TValue> : ScriptableObject
    {
        [SerializeField] private List<TValue> objectReferencesById;
        
        public List<TValue> ObjectReferencesById => objectReferencesById;

        public TValue GetObjectWithId(int id)
        {
            if (objectReferencesById.Count <= id)
            {
                throw new Exception($"Not available value for key {id}");
            } 
        
            return objectReferencesById[id];
        }
    }
}