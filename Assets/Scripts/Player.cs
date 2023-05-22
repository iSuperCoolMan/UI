using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private UnityEvent _healthChanged;

    private float _health;

    public int MaxHealth
    {
        get
        {
            return _maxHealth;
        }
    }

    public float Health
    {
        get
        {
            return _health;
        }

        private set
        {
            if (value <= _maxHealth)
            {
                if (value >= 0)
                    _health = value;
                else
                    _health = 0;
            }
            else
            {
                _health = _maxHealth;
            }
        }
    }

    private void Awake()
    {
        Health = _maxHealth;
    }

    public void GetDamage(float damage)
    {
        Health -= damage;
        _healthChanged.Invoke();
    }

    public void GetHeal(float heal)
    {
        Health += heal;
        _healthChanged.Invoke();
    }
}
