using UnityEngine;
using UnityEngine.UI;

public class AbilityPopup : MonoBehaviour
{
    public int timeoutSeconds;
    [SerializeField]
    private Text _abilityName;
    [Space(10)]
    [Header("First element = negative outcome. Ithers unlocking abilities based on happiness level.")]
    public string[] abilityTexts = new string[4];
    [Space(10)]
    public GameObject background;
    [Header("0 - bad outcome img, 1 - good outcome img")]
    public Sprite[] outcomeSprites = new Sprite[2];

    private float _timer = 0.0f;
    private bool _isActive = false;

    private void Update()
    {
        if (_isActive)
        {
            if (timerDone(timeoutSeconds))
            {
                hideChildren();
            }
        }
    }
    public void ActivatePopup(int pReachedLevel)
    {
        Debug.Log($"Activating popup.");
        if (!_isActive)
        {
            for (int childIndex = 0; childIndex < this.transform.childCount; ++childIndex)
            {
                this.transform.GetChild(childIndex).gameObject.SetActive(true);
            }

            switch (pReachedLevel)
            {
                case 0:
                    _abilityName.text = abilityTexts[2];
                    background.GetComponent<Image>().sprite = outcomeSprites[0];
                    break;
                case 1:
                    _abilityName.text = abilityTexts[1];
                    background.GetComponent<Image>().sprite = outcomeSprites[1];
                    break;
                case 2:
                    _abilityName.text = abilityTexts[2];
                    background.GetComponent<Image>().sprite = outcomeSprites[2];
                    break;
                case 3:
                    _abilityName.text = abilityTexts[3];
                    background.GetComponent<Image>().sprite = outcomeSprites[3];
                    break;
            }
            
            _isActive = true;
        }
    }

    private void hideChildren()
    {
        _abilityName.text = "x";
        for (int childIndex = 0; childIndex < this.transform.childCount; ++childIndex)
        {
            this.transform.GetChild(childIndex).gameObject.SetActive(false);
        }
        _isActive = false;

        _timer = 0;
    }
    private bool timerDone(int pTimeout)
    {
        _timer += Time.deltaTime;
        if (((int)_timer % 60) >= pTimeout)
        {
            Debug.Log($"Timer done!");
            return true;
        }
        else return false;
    }
}
