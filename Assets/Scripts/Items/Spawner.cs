using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private PickableTypes _getType;
    [SerializeField] private Pickable _pickableItem;

    [SerializeField] private Transform _spawnPoint;

    private void GetMaterial(Player player)
    {
        bool itemRecieved = player.RemoveItemFromInventory(_getType);
        if(itemRecieved)
        {
            Instantiate(_pickableItem.gameObject, _spawnPoint.position, Quaternion.identity);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.TryGetComponent(out Player player))
        {
            GetMaterial(player);
        }
    }
}
