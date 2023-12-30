internal interface IWantToOrder
{
    public bool CashRegisterBusy();
    public void CashRegisterSwitch(bool value);
}