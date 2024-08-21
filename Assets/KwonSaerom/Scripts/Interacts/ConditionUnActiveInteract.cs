using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class ConditionUnActiveInteract : InteractObject
{
    public int Count
    {
        get { return count; }
        set 
        { 
            count = value;
            if(count == completeCount)
                Complete();
        } 
    }
    [SerializeField] int powerActiveKey;

    private int count = 0;
    private int completeCount = 4;
    private bool isComplete = false;

    public override void Interact()
    {
        if (isComplete == true)
            return;

        if (IsPassLastObject() == false)
            return;
    }

    public override void Load()
    {
        Complete();
    }

    private void Complete()
    {
        isComplete = true;
        gameScene.FindInteractObject(powerActiveKey).gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
