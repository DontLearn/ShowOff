using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class JumpTowards : JumpAbs {
    private Animator _animator = null;



    private void Start() {
        LoadComponents();
    }


    private void LoadComponents() {
        _animator = GetComponent<Animator>();
        Debug.Assert( _animator, $"{this}: Animator component missing on {name}." );
    }


    public override void Jump() {
        /// TODO: jump from pos a to pos b in arc
        /// for now, just skips the jump
        _animator.SetTrigger( "Attack" );
    }
}