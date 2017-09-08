using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OperationUI : MonoBehaviour {
    [SerializeField]
    Button _right;
    [SerializeField]
    Button _left;
    [SerializeField]
    Button _up;
    [SerializeField]
    Button _down;
    
    public Button Right()
    {
        return _right;
    }

    public Button Left()
    {
        return _left;
    }

    public Button Up()
    {
        return _up;
    }

    public Button Down()
    {
        return _down;
    }
}
