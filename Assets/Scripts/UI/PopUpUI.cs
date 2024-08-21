using UnityEngine.UI;

public class PopUpUI : BaseUI
{
    protected override void Awake()
    {
        base.Awake();
        CanvasScaler scaler = GetComponent<CanvasScaler>();
        if(scaler != null)
            DisplayManager.Instance.SetCanvas(GetComponent<CanvasScaler>());
    }

    public override void LocalUpdate()
    {

    }

    public void Close()
    {
        Manager.UI.ClosePopUpUI();
    }
}
