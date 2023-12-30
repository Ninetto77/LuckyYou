public interface IAnchorManager
{
    /// <summary>
    /// ����� ��� �������� ��������� �����. 0 - ����� ��������, 1 - ����� �����, 2 - ����� �� ������
    /// </summary>
    /// <param name="anchorName"></param>
    /// <returns></returns>
    public int CheckAnchorBusy(string anchorName);
    public void AnchorBusySwitcher(string anchorName, bool value);
    public string[] FreePlaces(string anchorNamePart);
}
