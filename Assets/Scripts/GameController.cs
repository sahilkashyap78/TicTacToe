using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> m_Positions = new List<GameObject>();
    private List<GameObject> m_BotPositions = new List<GameObject>();
    [SerializeField]
    private Camera m_MainCamera;
    [SerializeField]
    private GameObject m_RayCaster;
    private const int POSITION_LAYER = 1 << 8;
    private const float MIN_RAYCASTDISTANCE = 1f;
    private const float MIN_POSITIONDISTANCE = 0.5f;
    private const float ZERO_POSITION = 0f;
    private Position m_CurrentPosition = null;
    [SerializeField]
    private Texture2D m_WhiteTexture;
    private int m_OZeroRowCounter;
    private int m_OZeroColumnCounter;
    private int m_OFirstRowCounter;
    private int m_OFirstColumnCounter;
    private int m_OSecondRowCounter;
    private int m_OSecondColumnCounter;
    private int m_XZeroRowCounter;
    private int m_XZeroColumnCounter;
    private int m_XFirstRowCounter;
    private int m_XFirstColumnCounter;
    private int m_XSecondRowCounter;
    private int m_XSecondColumnCounter;
    private int m_XLeftDiagonalCounter;
    private int m_XRightDiagonalCounter;
    private int m_OLeftDiagonalCounter;
    private int m_ORightDiagonalCounter;
    private bool m_PlayerTurn;
    public bool IsGameOver;
    [SerializeField]
    private Text m_BotTimer;
    [SerializeField]
    private Text m_PlayerTimer;
    private int m_Timer;


    int m_GameCurrentSignStatus;
    // Start is called before the first frame update

    void OnEnable()
    {
        InputManager.s_MouseUpPosition += MouseUp;
    }

    void OnDisable()
    {
        InputManager.s_MouseUpPosition -= MouseUp;
        SetWhiteTexture();
    }

    void Start()
    {
        m_Timer = 5;
        IsGameOver = false;
        m_PlayerTurn = true;
        m_GameCurrentSignStatus = 0;
        SetRowsAndColumns();
    }

    void MouseUp(Vector3 mousePosition)
    {
        if(m_PlayerTurn && !IsGameOver)
        {
            RaycastHit2D hit = GetCurrentPosition(mousePosition);
            if (hit.collider != null)
            {
                if (!hit.collider.gameObject.GetComponent<Position>().IsFilled)
                {
                    hit.collider.gameObject.GetComponent<Position>().PositionStatus = m_GameCurrentSignStatus;
                    hit.collider.gameObject.GetComponent<Position>().SetPositionTexture();
                    hit.collider.gameObject.GetComponent<Position>().IsFilled = true;
                    SetPositionsCounters(hit.collider.gameObject);
                    CheckWinCondition();
                    ToogleTurn();
                    StartCoroutine(BotTurn());
                }
            }
        }
    }

    void ToogleCurrentStatus()
    {
        if (m_GameCurrentSignStatus == 0)
        {
            m_GameCurrentSignStatus = 1;
        }
        else
        {
            m_GameCurrentSignStatus = 0;
        }

    }

    void SetWhiteTexture()
    {
        int positionIndex = 0;
        for (int row = 0; row < 3; row++)
        {
            for (int column = 0; column < 3; column++)
            {
                m_Positions[positionIndex].GetComponent<Position>().MeshRenderer.sharedMaterial.mainTexture = m_WhiteTexture;
                positionIndex++;
            }
        }
    }

    void SetPositionsCounters(GameObject gameObject)
    {
        if (gameObject.GetComponent<Position>().RowStatus == 0 && gameObject.GetComponent<Position>().ColumnStatus == 0)
        {
            if (gameObject.GetComponent<Position>().PositionStatus == 0)
            {
                m_OLeftDiagonalCounter++;
            }
            else if (gameObject.GetComponent<Position>().PositionStatus == 1)
            {
                m_XLeftDiagonalCounter++;
            }
        }

        if (gameObject.GetComponent<Position>().RowStatus == 1 && gameObject.GetComponent<Position>().ColumnStatus == 1)
        {
            if (gameObject.GetComponent<Position>().PositionStatus == 0)
            {
                m_OLeftDiagonalCounter++;
                m_ORightDiagonalCounter++;
            }
            else if (gameObject.GetComponent<Position>().PositionStatus == 1)
            {
                m_XLeftDiagonalCounter++;
                m_XRightDiagonalCounter++;
            }
        }

        if (gameObject.GetComponent<Position>().RowStatus == 2 && gameObject.GetComponent<Position>().ColumnStatus == 2)
        {
            if (gameObject.GetComponent<Position>().PositionStatus == 0)
            {
                m_OLeftDiagonalCounter++;
            }
            else if (gameObject.GetComponent<Position>().PositionStatus == 1)
            {
                m_XLeftDiagonalCounter++;
            }
        }

        if (gameObject.GetComponent<Position>().RowStatus == 0 && gameObject.GetComponent<Position>().ColumnStatus == 2)
        {
            if (gameObject.GetComponent<Position>().PositionStatus == 0)
            {
                m_ORightDiagonalCounter++;
            }
            else if (gameObject.GetComponent<Position>().PositionStatus == 1)
            {
                m_XRightDiagonalCounter++;
            }
        }

        if (gameObject.GetComponent<Position>().RowStatus == 2 && gameObject.GetComponent<Position>().ColumnStatus == 0)
        {
            if (gameObject.GetComponent<Position>().PositionStatus == 0)
            {
                m_ORightDiagonalCounter++;
            }
            else if (gameObject.GetComponent<Position>().PositionStatus == 1)
            {
                m_XRightDiagonalCounter++;
            }
        }

        if (gameObject.GetComponent<Position>().RowStatus == 0)
        {
            if (gameObject.GetComponent<Position>().PositionStatus == 0)
            {
                m_OZeroRowCounter++;
            }
            else if (gameObject.GetComponent<Position>().PositionStatus == 1)
            {
                m_XZeroRowCounter++;
            }
        }

        if (gameObject.GetComponent<Position>().ColumnStatus == 0)
        {
            if (gameObject.GetComponent<Position>().PositionStatus == 0)
            {
                m_OZeroColumnCounter++;
            }
            else if (gameObject.GetComponent<Position>().PositionStatus == 1)
            {
                m_XZeroColumnCounter++;
            }

        }

        if (gameObject.GetComponent<Position>().RowStatus == 1)
        {
            if (gameObject.GetComponent<Position>().PositionStatus == 0)
            {
                m_OFirstRowCounter++;
            }
            else if (gameObject.GetComponent<Position>().PositionStatus == 1)
            {
                m_XFirstRowCounter++;
            }

        }

        if (gameObject.GetComponent<Position>().ColumnStatus == 1)
        {
            if (gameObject.GetComponent<Position>().PositionStatus == 0)
            {
                m_OFirstColumnCounter++;
            }
            else if (gameObject.GetComponent<Position>().PositionStatus == 1)
            {
                m_XFirstColumnCounter++;
            }
        }

        if (gameObject.GetComponent<Position>().RowStatus == 2)
        {
            if (gameObject.GetComponent<Position>().PositionStatus == 0)
            {
                m_OSecondRowCounter++;
            }
            else if (gameObject.GetComponent<Position>().PositionStatus == 1)
            {
                m_XSecondRowCounter++;
            }
        }

        if (gameObject.GetComponent<Position>().ColumnStatus == 2)
        {
            if (gameObject.GetComponent<Position>().PositionStatus == 0)
            {
                m_OSecondColumnCounter++;
            }
            else if (gameObject.GetComponent<Position>().PositionStatus == 1)
            {
                m_XSecondColumnCounter++;
            }
        }
    }

    void CheckWinCondition()
    {
        if(m_OFirstColumnCounter >= 3 || m_OFirstRowCounter >= 3 || m_OZeroColumnCounter >= 3 || m_OZeroRowCounter >= 3|| m_OSecondColumnCounter >= 3 || m_OSecondRowCounter >= 3 || m_ORightDiagonalCounter >= 3 || m_OLeftDiagonalCounter >= 3)
        {
            Debug.Log("Sahil wins");
            IsGameOver = true;
        }
        else if(m_XFirstColumnCounter >= 3 || m_XFirstRowCounter >= 3 || m_XZeroColumnCounter >= 3 || m_XZeroRowCounter >= 3 || m_XSecondColumnCounter >= 3 || m_XSecondRowCounter >= 3 || m_XRightDiagonalCounter >= 3 || m_XLeftDiagonalCounter >= 3)
        {
            Debug.Log("Bot wins");
            IsGameOver = true;
        }
    }
    
    void SetRowsAndColumns()
    {
        int positionIndex = 0;
        for(int row = 0; row < 3; row++)
        {
            for(int column = 0; column < 3; column++)
            {
                m_Positions[positionIndex].GetComponent<Position>().RowStatus = row;
                m_Positions[positionIndex].GetComponent<Position>().ColumnStatus = column;
                positionIndex++;
            }
        }
    }

    private RaycastHit2D GetCurrentPosition(Vector3 mousePosition)
    {
        mousePosition = m_MainCamera.ScreenToWorldPoint(mousePosition);
        m_RayCaster.transform.position = new Vector3(mousePosition.x, mousePosition.y, ZERO_POSITION);
        RaycastHit2D hit = Physics2D.Raycast(m_RayCaster.transform.position, m_RayCaster.transform.forward, MIN_RAYCASTDISTANCE, POSITION_LAYER);
        return hit;
    }

    void CheckBotsRemainingPosition()
    {
        for(int index = 0; index < m_Positions.Count; index++)
        {
            if(!m_Positions[index].GetComponent<Position>().IsFilled)
            {
                m_BotPositions.Add(m_Positions[index]);
            }
        }
    }

    IEnumerator BotTurn()
    {
        if(!m_PlayerTurn && !IsGameOver)
        {
            CheckBotsRemainingPosition();
            if(m_BotPositions.Count != 0)
            {
                int randomNumber = (int)Random.Range(0, m_BotPositions.Count);
                yield return new WaitForSeconds(Random.Range(0f, 4f));
                Debug.Log(" Count" + m_BotPositions.Count);
                m_BotPositions[randomNumber].GetComponent<Position>().PositionStatus = m_GameCurrentSignStatus;
                m_BotPositions[randomNumber].GetComponent<Position>().SetPositionTexture();
                m_BotPositions[randomNumber].GetComponent<Position>().IsFilled = true;
                SetPositionsCounters(m_BotPositions[randomNumber]);
                m_BotPositions.Clear();
                CheckWinCondition();
                ToogleTurn();
            }
        }
    }

    void ToogleTurn()
    {
        m_PlayerTurn = !m_PlayerTurn;
        ToogleCurrentStatus();
    }

 

   


    // Update is called once per frame
    void Update()
    {
        
    }
}
