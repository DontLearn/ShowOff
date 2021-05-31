using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider _healthBar;
    [SerializeField] private Transform _camera;

    public void LateUpdate()
    {
        transform.LookAt(transform.position + _camera.forward);
    }
    public void SetHealth (int _health)
    {
        _healthBar.value = _health;
    }
}
