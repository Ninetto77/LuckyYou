using UnityEngine;
using Random = System.Random;

public class PlaceSelector : MonoBehaviour, ISelectPlace
{    
    //private string _WCName = "WC";
    //private string _TableAnchorName = "Table";
    [SerializeField] private int _numberOfTableAnchors;

    //public string BarQueue()
    //{
    //    string selectedAnchor = null;
    //    int result;
    //    if (gameObject.TryGetComponent(out IAnchorManager anchorManager))
    //    {
    //        for (int i = 0; i < _numberOfBarAnchors; i++)
    //        {
    //            result = anchorManager.CheckAnchorBusy($"{_barAnchorName}{i + 1}");
    //            if (result == 0)
    //            {
    //                selectedAnchor = $"{_barAnchorName}{i + 1}";
    //                anchorManager.AnchorBusySwitcher($"{_barAnchorName}{i + 1}", true);
    //                break;
    //            }
    //        }
    //    }
    //    return selectedAnchor;
    //}

    public string Select(string place)
    {
        return PlaceQueue(place);
        //if (place == _WCName)        
        //    return _WCName;
        //if (place == _TableAnchorName)
        //    return PlaceQueue();
        //else return null;
    }

    public string PlaceQueue(string placeName) 
    {
        string[] freePlaces;
        string selectedAnchor = null;        
        if (gameObject.TryGetComponent(out IAnchorManager anchorManager))
        {
            freePlaces = anchorManager.FreePlaces(placeName);
            if(freePlaces.Length > 0) selectedAnchor = freePlaces[Randomizer(0, freePlaces.Length)];
            else selectedAnchor = null;
        }
        return selectedAnchor;

    }

    private int Randomizer(int min, int max)
    {
        Random rnd = new Random();
        return rnd.Next(min, max);
    }
}
