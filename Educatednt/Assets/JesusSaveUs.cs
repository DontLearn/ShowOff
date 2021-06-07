using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JesusSaveUs : MonoBehaviour
{
    // Start is called before the first frame update

    private void Awake()
    {
        try
        {
            File.ReadAllText("non-existing.file");
        }

        catch (DirectoryNotFoundException e)
        {
            File.Create(e.Source);
        }
    }


    void Start()
    {
        try
        {
            File.ReadAllText("non-existing.file");
        }

        catch (DirectoryNotFoundException e)
        {
            File.Create(e.Source);
        }
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            File.ReadAllText("non-existing.file");
        }
        
        catch (DirectoryNotFoundException e)
        {
            File.Create(e.Source);
        }  
    }
}
