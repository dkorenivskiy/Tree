using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ui : MonoBehaviour
{
    [SerializeField] private List<Sprite> _images;
    [SerializeField] private Image _image;
    [SerializeField] private PickableTypes _pickableType;

    private void Start()
    {
        if(_pickableType == PickableTypes.tree)
        {
            SetImage(0);
        }

        if (_pickableType == PickableTypes.woodenPlank)
        {
            SetImage(1);
        }

        if (_pickableType == PickableTypes.diamond)
        {
            SetImage(2);
        }
    }

    private void SetImage(int index)
    {
        _image.sprite = _images[index];
    }
}
