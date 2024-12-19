using System.Collections.Generic;


namespace TestGOAP
{
    public class WorldState 
    {
        public Dictionary<string, bool> StateDic { get; private set; }

        public WorldState() 
        {
            this.StateDic = new Dictionary<string, bool>();
        }

        public void SetState(string key, bool value) 
        {
            this.StateDic[key] = value;
        }

        public bool HasState(string key) 
        {
            return this.StateDic.ContainsKey(key) ;
        }

        public WorldState Clone() 
        {
            var clone = new WorldState();
            foreach (var kvp in this.StateDic)
            {
                clone.SetState(kvp.Key, kvp.Value);
            }
            return clone;
        }
    }
    
}

