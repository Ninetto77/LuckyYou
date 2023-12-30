using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _questText;
    [SerializeField] private Text _questName;
    [SerializeField] private Image _bossSprite;
    [SerializeField] private TMP_Text[] targetTexts;
    [SerializeField] private Image[] targetSprites;
    private Dish[] _allMenu;
    
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("ScriptsHere").TryGetComponent(out ICookBook cookBook))
        {
            _allMenu = cookBook.AllMenu();
        }
        HideImages();
    }

    public void HideImages()
    {
        for (int i = 0; i < targetSprites.Length; i++)
        {
            targetSprites[i].enabled = false;
        }
    }

    public void UpdateUI(Quest quest)
    {
        _questName.text = quest.QuestName;
        _questText.text = quest.QuestBody;
        _bossSprite.sprite = quest.BossSprite;
        for (int i = 0; i < quest.QuestDish.Length; i++) 
        {
            targetTexts[i].text = quest.QuestDishesNeed[i].ToString();            
        }
        for (int j = 0; j < quest.QuestDish.Length; j++)
        {
            for (int k = 0; k < _allMenu.Length; k++)
            {
                if (quest.QuestDish[j] == _allMenu[k].DishName)
                {
                    targetSprites[j].enabled = true;
                    targetSprites[j].sprite = _allMenu[k].Icon;
                }
            }
        }
    }
}
