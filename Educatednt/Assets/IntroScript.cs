using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class IntroScript : MonoBehaviour
{

    public GameObject nextImage;
    public float time;
    private float timeLeft;
    // Start is called before the first frame update
    void Start()
    {
        timeLeft = time;
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            gameObject.SetActive(false);
            nextImage.SetActive(true);
        }
    }
}
