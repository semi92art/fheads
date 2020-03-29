using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ConsoleView : MonoBehaviour 
{
    #region public fields
    
    public GameObject viewContainer; //Container for console view, should be a child of this GameObject
    public Text logTextArea;
    public InputField inputField;
    public ScrollRect consoleScrollbar;
    
    #endregion

    #region private fields
    
    private readonly ConsoleController m_Console = new ConsoleController();
    private Vector3 m_SwipeFirstPosition;
    private Vector3 m_SwipeLastPosition;
    private float m_SwipeDragDistance;
    private readonly List<Vector3> m_TouchPositions = new List<Vector3>();
    private int m_CurrentCommand;
    private int m_Index;
    
    #endregion

    #region engine methods
    
    private void Start()
    {
        m_Console.VisibilityChanged += OnVisibilityChanged;
        m_Console.LogChanged += OnLogChanged;
        UpdateLogStr(m_Console.Log);
        m_SwipeDragDistance = Screen.width * 30 * 0.01f;
    }
    
    private void OnDestroy()
    {
        m_Console.VisibilityChanged -= OnVisibilityChanged;
        m_Console.LogChanged -= OnLogChanged;
    }
    
    private void Update()
    {
        #if UNITY_EDITOR
        
        if (Input.GetKeyUp(KeyCode.Return))
            RunCommand();
        
        //Toggle visibility when tilde key pressed
        if (Input.GetKeyUp("`"))
            ToggleVisibility();
        
        //Arrow up in history
        if (Input.GetKeyUp(KeyCode.UpArrow))
            UpCommand();
        if (Input.GetKeyUp(KeyCode.DownArrow))
            DownCommand();
        
        //Visibility on mouse swipe
        if(Input.GetMouseButtonDown(0))
            m_SwipeFirstPosition = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
        else if(Input.GetMouseButtonUp(0))
        {
            m_SwipeLastPosition = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
            if (Mathf.Abs(m_SwipeLastPosition.x - m_SwipeFirstPosition.x) > m_SwipeDragDistance)
                ToggleVisibility();
        }
        
        #endif
        
        //Toggle visibility when right swipe
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Moved)
                m_TouchPositions.Add(touch.position);
            
            if (touch.phase == TouchPhase.Ended)
            {
                m_SwipeFirstPosition = m_TouchPositions[0];
                m_SwipeLastPosition = m_TouchPositions[m_TouchPositions.Count - 1];

                //if swipeDragDistance > 30% from screen edge
                if (Mathf.Abs(m_SwipeLastPosition.x - m_SwipeFirstPosition.x) > m_SwipeDragDistance)
                    ToggleVisibility();
            }
        }
    }
    
    #endregion

    #region public methods
    
    public void UpCommand()
    {
        m_CurrentCommand++;
        m_Index = m_Console.CommandHistory.Count - m_CurrentCommand;
        if (m_Index >= 0 && m_Console.CommandHistory.Count!=0)
            inputField.text = m_Console.CommandHistory[m_Index].ToString();
        else
            m_CurrentCommand = m_Console.CommandHistory.Count;
        
        inputField.ActivateInputField();
        inputField.Select();
    }

    public void DownCommand()
    {
        m_CurrentCommand--;
        m_Index = m_Console.CommandHistory.Count - m_CurrentCommand ;
        if (m_Index < m_Console.CommandHistory.Count)
            inputField.text = m_Console.CommandHistory[m_Index].ToString();
        else
        {
            inputField.text = "";
            m_CurrentCommand = 0;
        }
        inputField.ActivateInputField();
        inputField.Select();
    }
    
    public void RunCommand()
    {
        m_Console.RunCommandString(inputField.text);
        inputField.text = "";
        inputField.ActivateInputField();
        inputField.Select();
        m_CurrentCommand = 0;
    }
    
    #endregion

    #region private methods

    private void ToggleVisibility()
    {
        SetVisibility(!viewContainer.activeSelf);
        consoleScrollbar.verticalNormalizedPosition = 0f;
        inputField.ActivateInputField();
        inputField.Select();
    }

    private void SetVisibility(bool _Visible)
    {
        viewContainer.SetActive(_Visible);
        if (inputField.text == "`")
            inputField.text = "";
    }

    private void OnVisibilityChanged(bool _Visible)
    {
        SetVisibility(_Visible);
    }

    private void OnLogChanged(string[] _NewLog)
    {
        UpdateLogStr(_NewLog);
    }

    private void UpdateLogStr(string[] _NewLog)
    {
        logTextArea.text = _NewLog == null ? "" : string.Join("\n", _NewLog);
    }

    #endregion
}