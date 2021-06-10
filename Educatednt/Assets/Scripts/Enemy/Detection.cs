using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Detection : MonoBehaviour {
    public Transform Target => _target;
    
    
    public enum DetectionType {
        VISION,
        DISTANCE
    }



    [Tooltip( "Detecting through a field of view or simply distance to the target." )]
    [SerializeField]
    private DetectionType _detectionType = DetectionType.DISTANCE;


    /// TODO: Remake these to show only those given for the current ChangeToChase case
    /// {
    [SerializeField, Range( 2f, 22f )]
    private float _detectionDistance = 11f;

    [SerializeField, Range( 30f, 180f )]
    private float _viewAngle = 45f;
    /// }
    /// 

    [SerializeField]
    private string _targetTag = "Player";


    private Animator _animator = null;
    private Transform _target = null;



    private void Start() {
        LoadComponents();
    }


    private void LoadComponents() {
        _animator = GetComponent<Animator>();
        if ( !_animator ) {
            _animator = GetComponentInChildren<Animator>();
        }
        Debug.Assert( _animator, $"{this}: Animator component missing on {name}." );

        _target = GameObject.FindGameObjectWithTag( _targetTag ).transform;
        Debug.Assert( _target, $"{this}: Object with tag {_targetTag} could not be found in scene." );
    }


    private void OnDrawGizmosSelected() {
        if ( _detectionType == DetectionType.VISION && _viewAngle > Mathf.Epsilon ) {
            Vector3 direction1 = Quaternion.Euler( 0, _viewAngle, 0 ) * transform.forward;
            Vector3 direction2 = Quaternion.Euler( 0, -_viewAngle, 0 ) * transform.forward;
            Gizmos.color = Color.red;
            Gizmos.DrawRay( transform.position + Vector3.up * 1.6f, direction1 * 5f );
            Gizmos.DrawRay( transform.position + Vector3.up * 1.6f, direction2 * 5f );
        }
        else {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere( transform.position + Vector3.up, _detectionDistance );
        }
    }


    public bool DetectTarget() {
        if ( _target ) {
            switch ( _detectionType ) {
                default:
                    Debug.LogWarning( $"{this}: Detection type not defined." );
                    break;
                case DetectionType.VISION:
                    Vector3 difference = _target.position - transform.position;

                    float angle = Vector3.SignedAngle( difference, transform.forward, Vector3.up );

                    if ( difference.magnitude <= _detectionDistance && angle <= _viewAngle ) {
                        difference.y = 0;
                        _animator.SetBool( "PlayerDetected", true );
                        return true;
                    }
                    break;
                case DetectionType.DISTANCE:
                    difference = _target.position - transform.position;

                    if ( difference.magnitude <= _detectionDistance ) {
                        _animator.SetBool( "PlayerDetected", true );
                        return true;
                    }
                    break;
            }
        }
        _animator.SetBool( "PlayerDetected", false );
        return false;
    }


    /*public bool DetectTarget( out float distance ) {
        if ( _target ) {
            switch ( _detectionType ) {
                default:
                    Debug.LogWarning( $"{this}: Detection type not defined." );
                    break;
                case DetectionType.VISION:
                    Vector3 difference = _target.position - transform.position;
                    distance = difference.magnitude;

                    float angle = Vector3.SignedAngle( difference, transform.forward, Vector3.up );

                    if ( distance <= _detectionDistance && angle <= _viewAngle ) {
                        difference.y = 0;
                        _animator.SetBool( "PlayerDetected", true );
                        return true;
                    }
                    break;
                case DetectionType.DISTANCE:
                    difference = _target.position - transform.position;
                    distance = difference.magnitude;

                    if ( distance <= _detectionDistance ) {
                        _animator.SetBool( "PlayerDetected", true );
                        return true;
                    }
                    break;
            }
        }
        _animator.SetBool( "PlayerDetected", false );
        distance = 0f;
        return false;
    }*/
}