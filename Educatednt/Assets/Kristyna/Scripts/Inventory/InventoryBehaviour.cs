using System.Collections.Generic;
using UnityEngine;


namespace Data {
    public class InventoryBehaviour : PersistentDataBehaviour {
        public enum Data {
            RICE,
            TOMATO,
            MUSHROOM,
            BURGER,
        }


        protected Dictionary<Data, int> data = new Dictionary<Data, int>() {
            {Data.RICE, 0 },
            {Data.TOMATO, 0 },
            {Data.MUSHROOM, 0 },
            {Data.BURGER, 0 }
        };



        public override void Load( PersistentData persistentData ) {
            isLoaded = true;
        }


        public override void Save( PersistentData persistentData ) { }



        protected override void Upgrade() {
            isUpgraded = true;
        }
    }
}