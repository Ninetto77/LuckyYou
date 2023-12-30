internal interface IGiveOrder
{
    public bool TakeDish(string dishName);
    public string CheckDishInHands();
    public void GiveDish();
}