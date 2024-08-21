using BackEnd;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace changyu
{
    public class Test : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI text;

        [ContextMenu("불리언값 바꾸는 테스트")]
        public void TestMethod()
        {
            Debug.Log(Manager.Data.TestCaseLoad(1).boolean[0]);
            Debug.Log(Manager.Data.TestCaseLoad(1).boolean[1]);
            Manager.Data.TestCaseLoad(1).boolean[0] = true;
            Manager.Data.TestCaseLoad(1).boolean[1] = true;
            Debug.Log(Manager.Data.TestCaseLoad(1).boolean[0]);
            Debug.Log(Manager.Data.TestCaseLoad(1).boolean[1]);
        }

        [ContextMenu("데이터 변환 테스트")]
        public void TextTest()
        {
            Debug.Log(Manager.Data.TestCaseLoad(1).a);
            Manager.Data.TestCaseLoad(1).a = "11";
            Debug.Log(Manager.Data.TestCaseLoad(1).a);
            Debug.Log(Manager.Data.TestCaseLoad(2).a);
            Manager.Data.TestCaseLoad(2).a = "22";
            Debug.Log(Manager.Data.TestCaseLoad(2).a);
            Debug.Log(Manager.Data.TestCaseLoad(3).a);
            Manager.Data.TestCaseLoad(3).a = "33";
            Debug.Log(Manager.Data.TestCaseLoad(3).a);
        }

        [ContextMenu("제이슨으로 변환 테스트")]
        public void TestSave()
        {
            Manager.Data.SaveTable("TestCaseTable");
        }


        [ContextMenu("googleHashKey")]
        public void GetGoogleHashKey()
        {
            string googleHashKey = Backend.Utils.GetGoogleHash();
            Debug.Log($"googleHashKey : {googleHashKey}");
            text.text = googleHashKey;
        }

    }

}


