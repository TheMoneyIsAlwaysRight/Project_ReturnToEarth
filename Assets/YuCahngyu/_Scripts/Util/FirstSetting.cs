using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
/// <summary>
/// 제작 : 찬규 
/// 빌드 시 어플이 가지고 있는 파일 초기생성을 위한 컴포넌트
/// 추후에 버전체크로 패치기능이 추가되면 같이 변경해야 함
/// </summary>
public class FirstSetting : MonoBehaviour
{
    [SerializeField] JsonTable[] jsons;     // 테이블들을 가지고 있을 배열

    bool isSet;

#if UNITY_EDITOR
    private string path => Path.Combine(Application.dataPath, $"Resources/Data");
#else
    private string path => Path.Combine(Application.persistentDataPath, $"Resources/Data");
#endif

    private void Awake()
    {
        foreach (JsonTable i in jsons)
        {
            // 버전체크해서 만드는 것으로 바꿔야 함
            //if (Manager.Data.ExistTable($"{i.fileName}"))
            //    continue;

            string json = i.json.text;

            if (Directory.Exists($"{path}/Json") == false)        // 폴더가 있는지 확인한다
            {
                Directory.CreateDirectory($"{path}/Json");
            }

            File.WriteAllText($"{path}/Json/{i.fileName}.json", json);
        }
    }
}

[Serializable]
public struct JsonTable
{
    [SerializeField] public string fileName;
    [SerializeField] public TextAsset json;
}
