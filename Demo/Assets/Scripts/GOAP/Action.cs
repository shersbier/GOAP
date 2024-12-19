using System.Collections.Generic;
using UnityEngine;

namespace TestGOAP
{
    public abstract class Action : MonoBehaviour
    {
        public Dictionary<string, bool> Preconditions { get; private set; }
        public Dictionary<string, bool> Effects { get; private set; }

        public float Cost { get; set; } = 1f;

        public Action()
        {
            Preconditions = new Dictionary<string, bool>();
            Effects = new Dictionary<string, bool>();
        }

        public void AddPrecondition(string key, bool value)
        {
            Preconditions[key] = value;
        }

        public void AddEffect(string key, bool value)
        {
            Effects[key] = value;
        }

        public abstract bool PerformAction();
    }
    
}
