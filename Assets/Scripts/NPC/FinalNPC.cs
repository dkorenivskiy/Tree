using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalNPC : MonoBehaviour
{
    [SerializeField] private PickableTypes _getType;
    [SerializeField] private float _neededNumber;
    
    private float _inv = 0f;

    private void GetMaterial(Player player)
    {
        bool itemRecieved = player.RemoveItemFromInventory(_getType);
        if (itemRecieved)
        {
            _inv++;
        }

        if (_inv == _neededNumber)
        {
            LvlEnd();
        }
    }

    private void LvlEnd()
    {
        SceneManager.LoadScene("Gameplay");
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            GetMaterial(player);
        }
    }
}
