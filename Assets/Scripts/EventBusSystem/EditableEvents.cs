using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public abstract class EditableEvents
{
    // public int x = 4;

    public EditableEvents()
    {

    }
}


[Serializable]
public class EEvent1 : EditableEvents
{
    public int x = 0;
}


[Serializable]
public class EEvent2 : EditableEvents
{
    public int y = 0;
}
