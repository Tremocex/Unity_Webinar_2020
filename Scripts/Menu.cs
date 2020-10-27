using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SceneLoader(int indexscene)
    {

        SceneManager.LoadScene(indexscene);


    }

    public void OptionsonClick(int index)
    {

        SceneManager.LoadScene(index);
    }
    public void ExitonClick()
    {
        Debug.Log("FechouJogo!");
        PlayerPrefs.DeleteAll();
        Application.Quit();
    }

}
