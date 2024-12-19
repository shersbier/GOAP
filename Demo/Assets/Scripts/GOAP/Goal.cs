using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGOAP
{
    public class Goal
    {
        public string Name { get; set; }
        public Dictionary<string, bool> TargetState { get; private set; }
        public float Priority { get; set; }

        public Goal(string name, float priority)
        {
            Name = name;
            Priority = priority;
            TargetState = new Dictionary<string, bool>();
        }

        public void AddTargetState(string key, bool value)
        {
            TargetState[key] = value;
        }
    }
    
}

