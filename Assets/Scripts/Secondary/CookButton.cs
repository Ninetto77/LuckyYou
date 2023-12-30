using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CookButton : MonoBehaviour
{
    private string dishName;
    private Transform _parent;
    private ProgressImagesUI _progressImages;
    [SerializeField] private CookingPlaceEnum _place;

    // Start is called before the first frame update
    private void Awake()
    {
        SelectPlaceCooking(_place);
    }

    public void ForStart()
    {
        //_scriptsHere = GameObject.FindGameObjectWithTag("ScriptsHere");
        dishName = GetComponentInChildren<Text>().text;
        _progressImages = GetComponentInParent<ProgressImagesUI>(true);
    }

    private void SelectPlaceCooking(CookingPlaceEnum cookingPlace)
    {
        switch (cookingPlace)
        {
            case CookingPlaceEnum.fryer:
                _parent = GameObject.FindGameObjectWithTag("Fryer").transform;
                break;
            case CookingPlaceEnum.grill:
                _parent = GameObject.FindGameObjectWithTag("Grill").transform;
                break;
            case CookingPlaceEnum.oven:
                _parent = GameObject.FindGameObjectWithTag("Oven").transform;
                break;
            case CookingPlaceEnum.stove:
                _parent = GameObject.FindGameObjectWithTag("Stove").transform;
                break;
            default:
                break;
        }
    }

    public void PressButton()
    {
        if (_parent.TryGetComponent(out ICooking cooking))
        {
            cooking.ICookingExecute(dishName, _progressImages);
        }
    }

}
