public interface IQuestBroker
{
    public Quest[] AllQuests();
    public void UpdateUI();
    public Quest CurrentQuest();
    public void CheckQuestsInQueue();
}
