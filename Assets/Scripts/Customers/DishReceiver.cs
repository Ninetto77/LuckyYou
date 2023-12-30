using UnityEngine;

[RequireComponent(typeof(EmojiController))]
public class DishReceiver : Interactable
{
    private GameObject _scriptsHere;
    private Dish _choosedDish = null;
    private Customer _customer;
    private GameObject _player;
    private Fraction _fraction;
    private EmojiController _emoji;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        _scriptsHere = GameObject.FindGameObjectWithTag("ScriptsHere");
        _player = GameObject.FindGameObjectWithTag("Player");
        _customer = GetComponent<Customer>();
        _emoji = GetComponent<EmojiController>();
    }

    public void IWillWaitOrder(Fraction fraction)
    {
        _fraction = fraction;
        if (_scriptsHere.TryGetComponent(out IChoose choose))
            _choosedDish = choose.SelectDish(_fraction.IsFraction);
        //_emoji.WaitOrder(true, _choosedDish.DishName);
        _emoji.WaitOrder(true, _choosedDish);
    }

    public override void Interact()
    {
        base.Interact();
        if (_player.TryGetComponent(out IGiveOrder giveOrder))
        {            
            if (_customer.CheckDesire() == 3 && giveOrder.CheckDishInHands() == _choosedDish.DishName)
            {
                CheckPls();
                _customer.SwitchDesire(false);
                //_emoji.WaitOrder(false, _choosedDish.DishName);
                _emoji.WaitOrder(false, _choosedDish);
                giveOrder.GiveDish();
            }
        }
    }

    private void CheckPls()
    {
        if (_scriptsHere.TryGetComponent(out ICheck check)) check.PayForOrder(_choosedDish);        
        if (_scriptsHere.TryGetComponent(out IReputation reputation)) reputation.AddReputation(_choosedDish, _fraction);        
    }
}
