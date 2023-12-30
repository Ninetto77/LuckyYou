using UnityEngine;

public class BossCreator : MonoBehaviour, IBossTaxi
{
    private GameObject _scriptsHere;
    [SerializeField] private Transform _createPosition;
    void Start()
    {
        _scriptsHere = GameObject.FindGameObjectWithTag("ScriptsHere");
    }

    public void CreateBoss(FromFraction fromFraction)
    {
        Fraction[] fractions = null;
        if (_scriptsHere.TryGetComponent(out IChangeInFraction changeInFraction))
        {
            fractions = changeInFraction.AllFractions();
        }
        if (fractions != null)
        {
            for (int i = 0; i < fractions.Length; i++)
            {
                if (fractions[i].IsFraction == fromFraction)
                {
                    Instantiate(fractions[i].Boss, _createPosition.position, Quaternion.identity);
                }
            }
        }
    }
}
