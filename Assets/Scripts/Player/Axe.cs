using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{
    [SerializeField] float _damage = 1f;

    public void IncreaseDamage(float damage)
    {
        _damage += damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Destroyable"))
        {
            if(other.TryGetComponent(out Destroyable destroyable))
            {
                destroyable.GetDamage(_damage);
            }
        }
    }
}
