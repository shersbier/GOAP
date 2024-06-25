﻿using CrashKonijn.Goap.Core.Interfaces;
using UnityEngine;

namespace CrashKonijn.Goap.Classes
{
    public abstract class CapabilityFactoryBase
    {
        public abstract ICapabilityConfig Create();
    }
    
    public abstract class MonoCapabilityFactoryBase : MonoBehaviour
    {
        public abstract ICapabilityConfig Create();
    }
    
    public abstract class ScriptableCapabilityFactoryBase : ScriptableObject
    {
        public abstract ICapabilityConfig Create();
    }
}