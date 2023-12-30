using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestRewardUI : MonoBehaviour
{
    [SerializeField] private Text _nameText;
    [SerializeField] private TMP_Text _questText;
    [SerializeField] private TMP_Text _questRewardText;
    [SerializeField] private Image _bossSprite;
    private Dish[] _allMenu;

    void Start()
    {
        if (GameObject.FindGameObjectWithTag("ScriptsHere").TryGetComponent(out ICookBook cookBook))
        {
            _allMenu = cookBook.AllMenu();
        }
        QuestTasks.OnQuestDone += Appear;
    }

    public void Appear(Quest quest)
    {
        GameManager.Instance.ShowQuestRewardCanvas();
        UpdateUI(quest);
    }

    public void UpdateUI(Quest quest)
    {
        _nameText.text = quest.QuestName;
        _questText.text = quest.QuestDone;
        _questRewardText.text = quest.QuestRewardText;
        _bossSprite.sprite = quest.BossSprite;
    }
}
