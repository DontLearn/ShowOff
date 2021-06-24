using UnityEngine;
using Combat;
using Data;


namespace Player {
    [RequireComponent( typeof( PlayerAnimator ), typeof( Rigidbody ) )]
    public class PlayerAttack : PlayerBehaviour {
        [HideInInspector]
        public bool struck = false;

        
        public bool pressed = false;



        [SerializeField, Range( 0f, 10f )]
        private float _downForce = 5f;

        [SerializeField, Range( 5f, 25f )]
        private float _bounceForce = 17f;

        [SerializeField, Range( 0f, 18f )]
        private float _bounceDash = 11.5f;

        [SerializeField, Range( 0.01f, 0.99f )]
        private float _slow = 0.7f;


        private Combat.Attack _attack = null;
        private MultiAttack.Hitbox _activeHitbox = MultiAttack.Hitbox.FRONT;
        private Rigidbody _rb = null;
        private bool _diveAvailable = true;
        private bool _isGrounded = true;
        private bool _attackPressed = false;



        private void Start() {
            LoadComponents();
        }


        private void Update() {
            if ( !isUpgraded && isLoaded ) {
                // UPGRADE
                Upgrade();
            }
        }


        protected override void Upgrade() {
            base.Upgrade();

            if ( null != _attack ) {
                int dmg = data[ Data.DAMAGE ];
                _attack.SetDamage( dmg );
                int knk = data[ Data.KNOCKBACK ];
                _attack.SetKnockback( knk );

                Debug.Log( $"{this}: Upgraded attack damage. Is now {dmg}." );
                Debug.Log( $"{this}: Upgraded attack knockback. Is now {knk}." );
            }
            else {
                Debug.LogWarning( $"{this} is trying to load data into an attack but not Attack could be found." );
            }
        }


        private void LoadComponents() {
            if ( null == _attack ) {
                _attack = GetComponent<Combat.Attack>();
                Debug.Assert( null != _attack, $"Attack component missing on {name}." );
            }

            if ( null == _rb ) {
                _rb = GetComponent<Rigidbody>();
                Debug.Assert( null != _rb, $"RigidBody component missing on {name}." );
            }
        }


        public void SetLevel( int level ) {
            // if the player is on level 0 or 1, they can't dive attack.
            _diveAvailable = level > 1;
            

            // from level 3 on, the player deals increased damage.
            if ( level > 2 && null != _attack ) {
                _attack.LevelUp( level );

                // Let's also save those new values
                data[ Data.DAMAGE ] = _attack.Damage;
                data[ Data.KNOCKBACK ] = _attack.Knockback;
            }
        }


        public void SetGrounded( bool pIsGrounded ) {
            _isGrounded = pIsGrounded;

            if ( _isGrounded && _activeHitbox == MultiAttack.Hitbox.BELOW ) {
                _attackPressed = false;
            }
        }


        public void AttackPressed() {
            _attackPressed = true;

            if ( !_isGrounded ) {
                float velY = _rb.velocity.y;
                if ( velY <= 0.4f ) {
                    _rb.velocity = new Vector3(
                        _rb.velocity.x * ( 1f - Mathf.Pow( _slow, 2f ) ),
                        _rb.velocity.y - _downForce,
                        _rb.velocity.z * ( 1f - Mathf.Pow( _slow, 2f ) )
                    );
                }
            }
        }


        public void SetActiveHitbox( MultiAttack.Hitbox hitbox ) {
            if ( !_diveAvailable ) {
                _activeHitbox = MultiAttack.Hitbox.FRONT;
                return;
            }
            else {
                _activeHitbox = hitbox;
            }

            if ( _attack && _attack is MultiAttack )
                ( ( MultiAttack )_attack ).SetActiveHitbox( hitbox );
        }


        private void FixedUpdate() {
            if ( null != _attack ) {
                float velY = _rb.velocity.y;
                if ( _diveAvailable && _attack is MultiAttack && !_isGrounded && velY <= 0.4f ) {
                    DiveAttack();
                }
                else {
                    FrontalAttack();
                }
            }
        }


        private void FrontalAttack() {
            if ( _attackPressed ) {
                pressed = true;
                _attackPressed = false;
                if ( _attack is MultiAttack ) {
                    MultiAttack multi = _attack as MultiAttack;
                }
                if ( _attack.Strike() ) { struck = true; }
            }
        }


        private void DiveAttack() {
            if ( _attackPressed ) {
                _rb.velocity = new Vector3(
                    _rb.velocity.x * ( 1f - Mathf.Pow( _slow, 2f ) ),
                    _rb.velocity.y,
                    _rb.velocity.z * ( 1f - Mathf.Pow( _slow, 2f ) )
                );

                _rb.AddForce( Vector3.down * _downForce * 10f );

                // Always attacking, checking for hits
                if ( _attack.Strike() ) {
                    _attackPressed = false;
                    Bounce();
                }
            }
        }


        private void Bounce() {
            Vector3 velocityXZ = new Vector3( _rb.velocity.x, 0, _rb.velocity.z ).normalized;
            _rb.velocity = velocityXZ;

            _rb.AddForce( 100f * new Vector3(
                velocityXZ.x * _bounceDash,
                _bounceForce,
                velocityXZ.z * _bounceDash )
            );
        }


        public override void Load( PersistentData persistentData ) {
            base.Load( persistentData );
            int level, damage, knockback;

            if ( !persistentData.TryGetIntData( Data.ATTACK_LEVEL.ToString(), out level ) ) {
                Debug.LogError( $"{this} Can't parse {Data.ATTACK_LEVEL}, not an int." );
            }
            if ( !persistentData.TryGetIntData( Data.DAMAGE.ToString(), out damage ) ) {
                Debug.LogError( $"{this} Can't parse {Data.DAMAGE}, not an int." );
            }
            if ( !persistentData.TryGetIntData( Data.KNOCKBACK.ToString(), out knockback ) ) {
                Debug.LogError( $"{this} Can't parse {Data.KNOCKBACK}, not an int." );
            }

            if ( null != _attack ) {
                data[ Data.ATTACK_LEVEL ] = _attack.Level;
                _attack.SetAttackLevel( level );
                Debug.Log( $"{this}: Loaded attack's level to {level}." );

                data[ Data.DAMAGE ] = damage;
                _attack.SetDamage( damage );
                Debug.Log( $"{this}: Loaded damage to {damage}." );

                data[ Data.KNOCKBACK ] = _attack.Knockback;
                _attack.SetKnockback( knockback );
                Debug.Log( $"{this}: Loaded knockback to {knockback}." );
            }
        }


        public override void Save( PersistentData persistentData ) {
            if ( null == _attack ) {
                Debug.Log( $"{this}: Attack isn't initialized yet, loading component.." );
                LoadComponents();

                if ( null == _attack ) {
                    Debug.LogError( $"{this}: Attack could not be initialized. No data was saved." );
                    return;
                }
            }

            persistentData.SetIntData( Data.ATTACK_LEVEL.ToString(), _attack.Level );
            Debug.Log( $"{this}: Saved attack's level to {_attack.Level}." );

            persistentData.SetIntData( Data.DAMAGE.ToString(), _attack.Damage );
            Debug.Log( $"{this}: Saved damage to {_attack.Damage}." );

            persistentData.SetIntData( Data.KNOCKBACK.ToString(), _attack.Knockback );
            Debug.Log( $"{this}: Saved knockback to {_attack.Knockback}." );
        }
    }
}
