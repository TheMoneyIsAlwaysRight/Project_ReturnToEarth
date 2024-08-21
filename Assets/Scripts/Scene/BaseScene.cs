using System.Collections;
using UnityEngine;

public abstract class BaseScene : MonoBehaviour
{
    protected int iD;     // 각 스테이지 씬의 데이터 테이블의 ID값 : 데이터테이블의 스테이지테이블 참고

    public int ID {get{ return iD; } set{ iD = value; } }

    public abstract IEnumerator LoadingRoutine();
}
