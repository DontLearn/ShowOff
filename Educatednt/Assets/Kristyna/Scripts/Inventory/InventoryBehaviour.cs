using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    public class InventoryBehaviour : PersistentDataBehaviour
    {
        [SerializeField]
        protected Dictionary<string, int> data = new Dictionary<string, int>()
        {
            {"rice", 0 },
            {"tomatoe", 0 },
            {"mushroom", 0 },
            {"burger", 0 }
        };

        protected bool isLoaded = false;

        public override void Load(PersistentData persistentData)
        {
            Dictionary<string, int> newDic = new Dictionary<string, int>();

            foreach (KeyValuePair<string, int> pair in data)
            {
                if (int.TryParse(persistentData.GetStringData(pair.Key), out int data))
                {
                    newDic.Add(pair.Key, pair.Value);
                }
                else
                {
                    Debug.LogError($"Could not parse {pair.Key} to an int");
                }
            }

            data = newDic;
            isLoaded = true;
        }
        public override void Save(PersistentData persistentData)
        {
            foreach (KeyValuePair<string, int> pair in data)
            {
                persistentData.SetIntData(pair.Key, pair.Value);
            }
        }
    }
}