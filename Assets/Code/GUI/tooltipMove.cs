using UnityEngine;
using System.Collections;

public class tooltipMove : MonoBehaviour {
    public GuiFunction gui;
    RectTransform m_transform = null;
    Vector3 move;
    Vector3 offset = new Vector3(Screen.width / 20, -(Screen.height / 9), 0);

    void Start()
    {
        m_transform = GetComponent<RectTransform>();
    }
    void Update()
    {
        if (gui.isToolTShowing())
        {
            move = Input.mousePosition;
            move += offset;
            m_transform.position = move;
        }
    }
}
