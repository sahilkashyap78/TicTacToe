using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    [SerializeField]
    private GameObject m_InputField;
    [SerializeField]
    private GameObject m_TextDisplay;
    public static string s_Name;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckName()
    {
        string name = m_InputField.GetComponentInChildren<Text>().text;
        Debug.Log(name);
        if(name == "" || name.Length > 10)
        {
            m_TextDisplay.GetComponent<Text>().text = "Please Enter the Valid Name";
        }
        else
        {
            s_Name = m_InputField.GetComponentInChildren<Text>().text;
            SceneManager.LoadScene(1);
        }

    }
}
