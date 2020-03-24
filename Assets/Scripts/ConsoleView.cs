using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public class ConsoleView : MonoBehaviour {
    ConsoleController console = new ConsoleController();
	
    public GameObject viewContainer; //Container for console view, should be a child of this GameObject
    public Text logTextArea;
    public InputField inputField;

    private Vector3 m_SwipeFirstPosition;
    private Vector3 m_SwipeLastPosition;
    private float m_SwipeDragDistance;
    private List<Vector3> touchPositions = new List<Vector3>();

    private int m_CurrentCommand;
    private int m_Index;

    void Start() {
        if (console != null) {
            console.VisibilityChanged += OnVisibilityChanged;
            console.LogChanged += OnLogChanged;
        }
        UpdateLogStr(console.Log);
        m_SwipeDragDistance = Screen.width * 30 / 100;
    }
	
    ~ConsoleView() {
        console.VisibilityChanged -= OnVisibilityChanged;
        console.LogChanged -= OnLogChanged;
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Return))
        {
            RunCommand();
        }
        
        //Toggle visibility when tilde key pressed
        if (Input.GetKeyUp("`"))
        {
            ToggleVisibility();
        }
        
        //Arrow up in history
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            UpCommand();
        }
        
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            DownCommand();
        }

        //Toggle visibility when right swipe
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Moved)
            {
                touchPositions.Add(touch.position);
            }

            if (touch.phase == TouchPhase.Ended)
            {
                m_SwipeFirstPosition = touchPositions[0];
                m_SwipeLastPosition = touchPositions[touchPositions.Count - 1];

                //if swipeDragDistance > 30% from screen edge
                if (Mathf.Abs(m_SwipeLastPosition.x - m_SwipeFirstPosition.x) > m_SwipeDragDistance)
                {
                    if ((m_SwipeLastPosition.x < m_SwipeFirstPosition.x)) 
                    {
                        ToggleVisibility();
                    }
                    else
                    {
                        ToggleVisibility();
                    }
                }
            }
        }
        
        //Visibility on mouse swipe
        if(Input.GetMouseButtonDown(0))
        {
            m_SwipeFirstPosition = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
        }
        if(Input.GetMouseButtonUp(0))
        {
            m_SwipeLastPosition = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
       
            if (Mathf.Abs(m_SwipeLastPosition.x - m_SwipeFirstPosition.x) > m_SwipeDragDistance)
            {
                if ((m_SwipeLastPosition.x < m_SwipeFirstPosition.x)) 
                {
                    ToggleVisibility();
                }
                else
                {
                    ToggleVisibility();
                }
            }
        }
    }

    public void UpCommand()
    {
        m_CurrentCommand++;
        m_Index = console.commandHistory.Count - m_CurrentCommand;
        if ((m_Index >= 0) && (console.commandHistory.Count!=0))
        {
            inputField.text = console.commandHistory[m_Index].ToString();
        }
        else
        {
            m_CurrentCommand = console.commandHistory.Count;
        }
        inputField.ActivateInputField();
        inputField.Select();
    }

    public void DownCommand()
    {
        m_CurrentCommand--;
        m_Index = console.commandHistory.Count - m_CurrentCommand ;
        if (m_Index < console.commandHistory.Count)
        {
            inputField.text = console.commandHistory[m_Index].ToString();
        }
        else
        {
            inputField.text = "";
            m_CurrentCommand = 0;
        }
        inputField.ActivateInputField();
        inputField.Select();
    }

    void ToggleVisibility()
    {
        SetVisibility(!viewContainer.activeSelf);
        inputField.ActivateInputField();
        inputField.Select();
    }

    void SetVisibility(bool _Visible)
    {
        viewContainer.SetActive(_Visible);
        if (inputField.text == "`")
        {
            inputField.text = "";
        }
    }

    void OnVisibilityChanged(bool _Visible)
    {
        SetVisibility(_Visible);
    }

    void OnLogChanged(string[] _NewLog)
    {
        UpdateLogStr(_NewLog);
    }

    void UpdateLogStr(string[] _NewLog)
    {
        if (_NewLog == null)
        {
            logTextArea.text = "";
        }
        else
        {
            logTextArea.text = string.Join("\n", _NewLog);
        }
    }

    public void RunCommand()
    {

        console.RunCommandString(inputField.text);
        inputField.text = "";
        inputField.ActivateInputField();
        inputField.Select();
        m_CurrentCommand = 0;
    }
}