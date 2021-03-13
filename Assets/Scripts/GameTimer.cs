using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameTimer : MonoBehaviour
{
    [SerializeField]
    Text m_TimerText;
    private int m_Timer = 6;
    private const int MIN_TIME = 0;
    private const float WAITING_TIME = 1f;
    private const int RESET_TIMER = 6;
    private const string TIMER_STRING = "6";
    [SerializeField]
    GameController m_GameController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public IEnumerator Timer()
    {
        while (m_Timer > MIN_TIME)
        {
            yield return new WaitForSeconds(WAITING_TIME);
            m_Timer--;
            m_TimerText.text = m_Timer.ToString();
        }
        m_GameController.ToogleTurn();
    }

    public void ResetTimer()
    {
        m_Timer = RESET_TIMER;
        m_TimerText.text = TIMER_STRING;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
