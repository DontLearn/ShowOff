using System;
using System.Collections.Generic;


namespace Data {
    public class PersistentData {
        public static PersistentData instance {
            get {
                if ( _instance == null ) {
                    _instance = new PersistentData();
                }
                return _instance;
            }
        }


        private static PersistentData _instance = null;


        private List<PersistentDataBehaviour> _maintainedBehaviours;



        public void LoadAllPersistentItems() {
            foreach ( PersistentDataBehaviour behaviour in _maintainedBehaviours ) {
                behaviour.Load( this );
            }
        }


        public void SaveAllPersistentItems() {
            foreach ( PersistentDataBehaviour behaviour in _maintainedBehaviours ) {
                behaviour.Save( this );
            }
        }


        public void AddToPersistencyManager( PersistentDataBehaviour behaviour ) {
            _maintainedBehaviours.Add( behaviour );
        }


        public void RemoveFromPersistencyManager( PersistentDataBehaviour behaviour ) {
            _maintainedBehaviours.Remove( behaviour );
        }


        private Dictionary<String, String> _data = new Dictionary<String, String>();


        public void Reset() {
            _data.Clear();
        }


        public void SetIntData( string id, int value ) {
            _data[ id ] = value.ToString();
        }


        public void SetStringData( string id, string value ) {
            _data[ id ] = value;
        }


        public string GetStringData( string id ) {
            if ( _data.ContainsKey( id ) ) {
                return _data[ id ];
            }
            else {
                Console.WriteLine( "ERROR : DATA " + id + " CANNOT BE FOUND" );
                return "";
            }
        }


        public bool TryGetStringData( string id, out string result ) {
            if ( _data.ContainsKey( id ) ) {
                result = _data[ id ];
                return true;
            }
            else {
                result = null;//undefined
                return false;
            }
        }
    }
}