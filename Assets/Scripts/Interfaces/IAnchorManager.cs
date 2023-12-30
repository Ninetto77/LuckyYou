public interface IAnchorManager
{
    /// <summary>
    /// Метод для проверки занятости якоря. 0 - якорь свободен, 1 - якорь занят, 2 - якорь не найден
    /// </summary>
    /// <param name="anchorName"></param>
    /// <returns></returns>
    public int CheckAnchorBusy(string anchorName);
    public void AnchorBusySwitcher(string anchorName, bool value);
    public string[] FreePlaces(string anchorNamePart);
}
