using System.Collections.Generic;
using UnityEngine;

public class Destroyable : MonoBehaviour
{
    [SerializeField] private float _secondsRespawn = 15f;
    private float _timeToRespawn = 0f;

    [SerializeField] private float _maxHealthPoints = 3f;
    private float _healthPoints;

    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private Collider _collider;

    [SerializeField] private Pickable _pickableItem;
    [SerializeField] private List<Transform> _blockSpawnPoints;

    private bool _needRespawn = false;

    private void Start()
    {
        _healthPoints = _maxHealthPoints;
    }

    private void Update()
    {
        if (_needRespawn && Time.time > _timeToRespawn)
        {
            _healthPoints = _maxHealthPoints;
            _meshRenderer.enabled = true;
            _collider.enabled = true;
            _needRespawn = false;
        }
    }

    public void GetDamage(float Damage)
    {
        _healthPoints -= Damage;
        for (int i = 0; i < Damage; i++)
        {
            Instantiate(_pickableItem.gameObject, GetRandomPosition().position, Quaternion.identity);
        }

        if (_healthPoints <= 0)
        {
            _meshRenderer.enabled = false;
            _collider.enabled = false;
            _needRespawn = true;
            _timeToRespawn = Time.time + _secondsRespawn;
        }
    }

    private Transform GetRandomPosition()
    {
        int index = Random.Range(0, _blockSpawnPoints.Count);
        return _blockSpawnPoints[index];
    }
}
