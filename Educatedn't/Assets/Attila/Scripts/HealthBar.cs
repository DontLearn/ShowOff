using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider _healthBar;


    private Transform _camera;



    private void Start() {
        _camera = Camera.main.transform;
    }


    public void LateUpdate()
    {
        transform.LookAt(transform.position + _camera.forward);
    }


    public void SetHealth (int _health)
    {
        _healthBar.value = _health;
    }


    public void SetMaxHealth (int _maxHealth)
    {
        _healthBar.maxValue = _maxHealth;
    }
}
