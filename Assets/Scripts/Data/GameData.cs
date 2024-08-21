using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 제작 : 찬규 
/// 각종 테이블과 데이터 모음
/// </summary>

[Serializable]
public class GameData
{

}

#region IDTable
[Serializable]
public class IDTable : Table
{
    public int TABLE_NUMBER;
    public string TABLE_NAME;
    public string FileName;
}

[Serializable]
public class IDData
{
    public IDTable[] tables;

    public Dictionary<int, IDTable> MakeDic()
    {
        Dictionary<int, IDTable> dic = new Dictionary<int, IDTable>();

        foreach (IDTable i in tables)
        {
            dic.Add(i.TABLE_NUMBER, i);
        }
        return dic;
    }
}
#endregion
#region ResourceTable
[Serializable]
public class ResourceTable : Table
{
    public int ID;
    public string NAME;
    public int Type;
}

[Serializable]
public class ResourceData
{
    public ResourceTable[] tables;

    public Dictionary<int, ResourceTable> MakeDic()
    {
        Dictionary<int, ResourceTable> dic = new Dictionary<int, ResourceTable>();

        foreach (ResourceTable i in tables)
        {
            dic.Add(i.ID, i);
        }
        return dic;
    }
}
#endregion
#region TextTable
[Serializable]
public class TextTable : Table
{
    public int ID;
    public string FontRGB;
    public int FontSize;
    public int TextFont;
    public string Text_KR;
    public string Text_EN;
    public string Text_CH;
    public string Text_JP;
    public string Text_SP;
    public string Text_FR;
}

[Serializable]
public class TextData
{
    public TextTable[] tables;
    public Dictionary<int, TextTable> MakeDic()
    {
        Dictionary<int, TextTable> dic = new Dictionary<int, TextTable>();

        foreach (TextTable i in tables)
        {
            dic.Add(i.ID, i);
        }
        return dic;
    }
}
#endregion
#region TextBundleTable
[Serializable]
public class TextBundleTable : Table
{
    public int ID;
    public string DESCRIPTION;
    public int[] TEXT;   // 텍스트테이블의 배열
}

[Serializable]
public class TextBundleData
{
    public TextBundleTable[] tables;

    public Dictionary<int, TextBundleTable> MakeDic()
    {
        Dictionary<int, TextBundleTable> dic = new Dictionary<int, TextBundleTable>();

        foreach (TextBundleTable i in tables)
        {
            dic.Add(i.ID, i);
        }
        return dic;
    }
}
#endregion
#region UITable
[Serializable]
public class UITable : Table
{
    public int ID;
    public string Name;
    public int Type;
    public int Text;
    public int[] Img;
    public int[] Sound;
    public int[] UI;
    public int Video;
}

[Serializable]
public class UIData
{
    public UITable[] tables;

    public Dictionary<int, UITable> MakeDic()
    {
        Dictionary<int, UITable> dic = new Dictionary<int, UITable>();

        foreach (UITable i in tables)
        {
            dic.Add(i.ID, i);
        }
        return dic;
    }
}
#endregion
#region PuzzleTable
[Serializable]
public class PuzzleTable : Table
{
    public int ID;
    public string Name;
    public int Type;
    public int IntAnswer;
    public string StrAnswer;
    public int[] Resource;
    public int UI;
}

[Serializable]
public class PuzzleData
{
    public PuzzleTable[] tables;

    public Dictionary<int, PuzzleTable> MakeDic()
    {
        Dictionary<int, PuzzleTable> dic = new Dictionary<int, PuzzleTable>();

        foreach (PuzzleTable i in tables)
        {
            dic.Add(i.ID, i);
        }
        return dic;
    }
}
#endregion
#region ItemTable
[Serializable]
public class ItemTable : Table
{
    public int ID;
    public string Name;    
    public int Value;
    public int[] RelationItem;
    public int Resource;
    public int[] RequiredItem;
    public int[] Result;
    public bool Permanent;
    public int UI;
}

[Serializable]
public class ItemData
{
    public ItemTable[] tables;

    public Dictionary<int, ItemTable> MakeDic()
    {
        Dictionary<int, ItemTable> dic = new Dictionary<int, ItemTable>();

        foreach (ItemTable i in tables)
        {
            dic.Add(i.ID, i);
        }
        return dic;
    }
}
#endregion
#region InteractionObject
[Serializable]
public class InteractionObject : Table
{
    public int ID;
    public string Name;
    public bool CanInteract;
    public bool IsTransparency;
    public int Priority;    // 우선도?
    public float PosX;
    public float PosY;
    public int Type;
    public int[] RequiredID;
    public int[] RelationID;
    public int ResourceID;
    public int[] UI;
    public int StageX;
    public int StageY;
    public float imgSizeX;
    public float imgSizeY;
    public int interactText;
}

[Serializable]
public class InteractionObjectData
{
    public InteractionObject[] tables;

    public Dictionary<int, InteractionObject> MakeDic()
    {
        Dictionary<int, InteractionObject> dic = new Dictionary<int, InteractionObject>();

        foreach (InteractionObject i in tables)
        {
            dic.Add(i.ID, i);
        }
        return dic;
    }
}
#endregion
#region StageTable
[Serializable]
public class StageTable : Table
{
    public int ID;
    public string Name;
    public int Timer;
    public int FrontStory;
    public int BackStory;
    public int[] UI;
    public int[] stageX;
    public int[] stageY;
    public int[] Object;
    public int[] Priority;
    public int Exit;
    public int[] Item;
    public int[] HintUI;
    public int StageProcessID;
    public int InventoryID;
}

[Serializable]
public class StageData
{
    public StageTable[] tables;

    public Dictionary<int, StageTable> MakeDic()
    {
        Dictionary<int, StageTable> dic = new Dictionary<int, StageTable>();

        foreach (StageTable i in tables)
        {
            dic.Add(i.ID, i);
        }
        return dic;
    }
}
#endregion
#region ChapterTable
[Serializable]
public class ChapterTable : Table
{
    public int ID;
    public string Name;
    public float PosX;
    public float PosY;
    public int Type;
    public bool FirstOpen;
    public int[] Stage;
    public int[] StageImg;
}

[Serializable]
public class ChapterData
{
    public ChapterTable[] tables;

    public Dictionary<int, ChapterTable> MakeDic()
    {
        Dictionary<int, ChapterTable> dic = new Dictionary<int, ChapterTable>();

        foreach (ChapterTable i in tables)
        {
            dic.Add(i.ID, i);
        }
        return dic;
    }
}
#endregion
#region UserInfoTable
[Serializable]
public class UserInfoTable : Table
{
    public int ID;
    public string UID;
    public string PhoneModel;
    public int[] StageClear;
    public int AccessDate;
    public int AccessTime;
}

[Serializable]
public class UserInfoData
{
    public UserInfoTable[] tables;

    public Dictionary<int, UserInfoTable> MakeDic()
    {
        Dictionary<int, UserInfoTable> dic = new Dictionary<int, UserInfoTable>();

        foreach (UserInfoTable i in tables)
        {
            dic.Add(i.ID, i);
        }
        return dic;
    }
}
#endregion
#region UserItemTable
[Serializable]
public class UserItemTable : Table
{
    public int ID;
    public int Oxygen;
    public int Hint;
}

[Serializable]
public class UserItemData
{
    public UserItemTable[] tables;

    public Dictionary<int, UserItemTable> MakeDic()
    {
        Dictionary<int, UserItemTable> dic = new Dictionary<int, UserItemTable>();

        foreach (UserItemTable i in tables)
        {
            dic.Add(i.ID, i);
        }
        return dic;
    }
}
#endregion
#region InventoryTable
[Serializable]
public class InventoryTable : Table
{
    public int ID;
    public int[] slot;
    public int Item;
    public int stageID;
}

[Serializable]
public class InventoryData
{
    public InventoryTable[] tables;

    public Dictionary<int, InventoryTable> MakeDic()
    {
        Dictionary<int, InventoryTable> dic = new Dictionary<int, InventoryTable>();

        foreach (InventoryTable i in tables)
        {
            dic.Add(i.ID, i);
        }
        return dic;
    }
}
#endregion
#region StageProcessTable
[Serializable]
public class StageProcessTable : Table
{
    public int ID;
    public int NumStage;
    public int[] ObjectID;
    public bool[] IsClear;
    public float Timer;
    public bool[] ItemCondition;
}

[Serializable]
public class StageProcessData
{
    public StageProcessTable[] tables;

    public Dictionary<int, StageProcessTable> MakeDic()
    {
        Dictionary<int, StageProcessTable> dic = new Dictionary<int, StageProcessTable>();

        foreach (StageProcessTable i in tables)
        {
            dic.Add(i.ID, i);
        }
        return dic;
    }
}
#endregion
#region UserLogTable
[Serializable]
public class UserLogTable : Table
{
    public int ID;
    public string UserID;
    public int Date;
    public int Time;
    public int Sec;
    public int PlayType;
    public float posX;
    public float posY;
    public int InteractType;
    public int numUI;
    public int numStage;
}

[Serializable]
public class UserLogData
{
    public UserLogTable[] tables;

    public Dictionary<int, UserLogTable> MakeDic()
    {
        Dictionary<int, UserLogTable> dic = new Dictionary<int, UserLogTable>();

        foreach (UserLogTable i in tables)
        {
            dic.Add(i.ID, i);
        }
        return dic;
    }
}
#endregion

[Serializable]
public class Table
{

}

[Serializable]
public class TestCase
{
    public int ID;
    public string a;
    public int[] b;
    public bool[] boolean;
}

[Serializable]
public class TestCaseData
{
    public TestCase[] tables;

    public Dictionary<int, TestCase> MakeDic()
    {
        Dictionary<int, TestCase> dic = new Dictionary<int, TestCase>();

        foreach (TestCase i in tables)
        {
            dic.Add(i.ID, i);
        }
        return dic;
    }
}