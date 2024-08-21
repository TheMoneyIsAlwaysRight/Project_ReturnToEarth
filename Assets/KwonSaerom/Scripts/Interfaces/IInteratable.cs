using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 상호작용하는 오브젝트들은 이 인터페이스를 상속받아 구현하면 된다.
public interface IInteractable
{
    public void Close();
    public void Interact();
}
