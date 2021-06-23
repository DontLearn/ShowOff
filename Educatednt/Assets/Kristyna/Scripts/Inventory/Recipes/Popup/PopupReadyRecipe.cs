using UnityEngine;
using UnityEngine.UI;

public class PopupReadyRecipe : MonoBehaviour
{
    public int timeoutSeconds;
    public Text _recipeName;

    float timer = 0.0f;
    private bool _isActive = false;

    private void Update()
    {
        if (_isActive)
        {
            if (timerDone(timeoutSeconds))
            {
                hideItself();
            }
        }
    }
    public void ActivatePopup(string pRecipeName)
    {
        Debug.Log($"Activating popup.");
        if (!_isActive)
        {
            this.gameObject.SetActive(true);
            _recipeName.text = pRecipeName;
            _isActive = true;
        }        
    }

    private void hideItself()
    {
        Debug.Log($"Hide popup.");

        _recipeName.text = "x";
        this.gameObject.SetActive(false);
        _isActive = false;

        timer = 0;
    }
    private bool timerDone(int pTimeout)
    {
        timer += Time.deltaTime;
        if (((int)timer % 60) >= pTimeout)
        {
            Debug.Log($"Timer done!");
            return true;
        }
        else return false;
    }
}
