using UnityEngine;

public enum PickableTypes
{
    tree,
    diamond,
    woodenPlank
}

public class Pickable : MonoBehaviour
{
    [SerializeField] public PickableTypes _pickableType;

    [SerializeField] private Collider _collider;

    private bool _needToPick = false;
    private Transform _target;
    private float _speed = 10f;

    private void FixedUpdate()
    {
        if( _needToPick)
        {
            GoTo(_target);
        }
    }
    public void SwitchPick(Transform target)
    {
        _needToPick = true;
        _target = target;
    }

    private void GoTo(Transform target)
    {
        _collider.isTrigger = true;
        transform.position = Vector3.MoveTowards(transform.position, target.position, _speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
