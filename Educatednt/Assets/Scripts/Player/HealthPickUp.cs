using Combat;
using UnityEngine;


public class HealthPickUp : MonoBehaviour {
    [SerializeField, Range( 0, 100 )]
    private int _healAmount = 25;


    private Health _health = null;
    private HealthBar _healthBar = null;



    private void OnTriggerEnter( Collider other ) {
        if ( other.gameObject.CompareTag( "Player" ) ) {

            _health = other.gameObject.GetComponent<Health>();
            _health.AddHitPoints( _healAmount );
            _healthBar = other.gameObject.GetComponentInChildren<HealthBar>();
            _healthBar.SetHealth( _health.HitPoints );

            Debug.Log( $"{this}: Added {_healAmount} to the Player's hit points, which is now {_health.HitPoints}." );
            
            Destroy( gameObject );
        }
    }
}