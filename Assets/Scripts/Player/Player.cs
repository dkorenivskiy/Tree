using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public UnityEvent<float> OnDamageIncrease;

    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Animator _animator;

    [SerializeField] private float Speed = 10f;

    [SerializeField] private LayerMask _destroyable;
    private bool _destroying = false;

    [SerializeField] private LayerMask _pickable;

    //Inventory
    [SerializeField] private Transform _target;
    [SerializeField] private int _maxSizeOfInv = 10;
    private int _inv = 0;
    private int _trees = 0, _diamonds = 0, _planks = 0;
    [SerializeField] private Text _treesUi, _diamondsUi, _planksUi;

    private void Update()
    {
        _treesUi.text = _trees.ToString();
        _diamondsUi.text = _diamonds.ToString();
        _planksUi.text = _planks.ToString();

        float x = SimpleInput.GetAxis("Horizontal");
        float z = SimpleInput.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        Vector3 movementDirection = new Vector3(x, 0, z);

        if (move != Vector3.zero)
        {
            transform.localRotation = Quaternion.LookRotation(movementDirection);
            _rb.MovePosition(transform.position + movementDirection * Speed * Time.deltaTime);
            _animator.SetBool("IsRunning", true);
        }
        else
        {
            _animator.SetBool("IsRunning", false);
        }

        if(_diamonds == _maxSizeOfInv)
        {
            IncreaseDamage();
        }

        Collider[] hitDestroyable = Physics.OverlapSphere(_target.position, 2f, _destroyable);
        if (hitDestroyable.Length > 0)
        {
            Destroy();
        }

        Collider[] hitPickable = Physics.OverlapSphere(_target.position, 2f, _pickable);
        if (hitPickable.Length > 0 && _inv < _maxSizeOfInv)
        {
            foreach (var pickable in hitPickable)
            {
                if (pickable.TryGetComponent(out Pickable pick))
                {
                    pick.SwitchPick(_target);
                }
            }
        }
    }

    //Destroy an object method
    private void Destroy()
    {
        if (_destroying)
        {
            return;
        }

        _destroying = true;
        _animator.SetTrigger("Destroy");
    }

    //For animator. Invoking in animation Destroy
    public void SetDestroyingFalse()
    {
        _destroying = false;
    }

    //Inventory method
    private void AddItemToInventory(Pickable pick)
    {
        _inv++;
        if (pick._pickableType == PickableTypes.tree)
        {
            _trees++;
        }

        if (pick._pickableType == PickableTypes.diamond)
        {
            _diamonds++;
        }

        if (pick._pickableType == PickableTypes.woodenPlank)
        {
            _planks++;
        }
    }

    public bool RemoveItemFromInventory(PickableTypes pickableType)
    {
        if(pickableType == PickableTypes.tree && _trees > 0)
        {
            _trees--;
            _inv--;
            return true;
        }

        if (pickableType == PickableTypes.diamond && _diamonds > 0)
        {
            _diamonds--;
            _inv--;
            return true;
        }

        if (pickableType == PickableTypes.woodenPlank && _planks > 0)
        {
            _planks--;
            _inv--;
            return true;
        }

        return false;
    }

    private void ClearInventory()
    {
        _inv = 0;
        _trees = 0;
        _diamonds = 0;
        _planks = 0;
    }

    //Increase damage
    private void IncreaseDamage()
    {
        OnDamageIncrease?.Invoke(1f);
        _inv -= _diamonds;
        _diamonds = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Pickable pick))
        {
            AddItemToInventory(pick);
        }

        if (other.CompareTag("Resetter"))
        {
            ClearInventory();
        }
    }
}
