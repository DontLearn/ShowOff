using UnityEngine;
using UnityEngine.UI;

public class PopupReadyRecipe : MonoBehaviour
{
    public int timeoutSeconds;
    public Text _recipeName;

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
    public void ActivatePopup(string pRecipeName)
    {
        Debug.Log($"Activating popup.");
        if (!_isActive)
        {
            for (int childIndex = 0; childIndex < this.transform.childCount; ++childIndex)
            {
                this.transform.GetChild(childIndex).gameObject.SetActive(true);
            }
            _recipeName.text = pRecipeName;
            _isActive = true;
        }        
    }

    private void hideChildren()
    {
        _recipeName.text = "x";
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
