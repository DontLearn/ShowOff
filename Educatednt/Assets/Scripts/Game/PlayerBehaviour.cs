using System.Collections.Generic;


namespace Data {
    public class PlayerBehaviour : PersistentDataBehaviour {
        public enum Data {
            UPGRADE,
            HEALTH,
            DAMAGE,
            KNOCKBACK,
            ATTACK_LEVEL,
            JUMPFORCE
        }

        protected Dictionary<Data, int> data = new Dictionary<Data, int>() {
            { Data.UPGRADE, 0 },
            { Data.HEALTH, 0 },
            { Data.DAMAGE, 0 },
            { Data.KNOCKBACK, 0 },
            { Data.ATTACK_LEVEL, 0 },
            { Data.JUMPFORCE, 0 }
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