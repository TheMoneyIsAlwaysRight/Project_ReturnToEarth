using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;
using Object = UnityEngine.Object;

public class ResourceManager : Singleton<ResourceManager>
{
    private Dictionary<string, Object> resources = new Dictionary<string, Object>();

    #region addressable

    //public List<AsyncOperationHandle> Handles = new List<AsyncOperationHandle>();
    //// -----> 권새롬 추가 : 어드레서블
    //public T Load<T>(string key) where T : Object
    //{
    //    if (resources.TryGetValue(key, out Object resource))
    //    {
    //        return resource as T;
    //    }

    //    //스프라이트 로드할때 항상 .sprite가 붙어 있어야하는데 데이터시트에 .sprite가 붙어있지 않은 데이터가 많음
    //    if (typeof(T) == typeof(Sprite))
    //    {
    //        key = key + ".sprite";
    //        if (resources.TryGetValue(key, out Object temp))
    //        {
    //            return temp as T;
    //        }
    //    }
    //    return null;
    //}

    //public void LoadAsync<T>(string key, Action<T> callback = null) where T : UnityEngine.Object
    //{
    //    //스프라이트인 경우 하위객체의 찐이름으로 로드하면 스프라이트로 로딩이 됌
    //    string loadKey = key;
    //    if (key.Contains(".sprite"))
    //        loadKey = $"{key}[{key.Replace(".sprite", "")}]";

    //    var asyncOperation = Addressables.LoadAssetAsync<T>(loadKey);
    //    asyncOperation.Completed += (op) =>
    //    {
    //        // 캐시 확인.
    //        if (resources.TryGetValue(key, out Object resource))
    //        {
    //            callback?.Invoke(op.Result);
    //            return;
    //        }

    //        resources.Add(key, op.Result);
    //        callback?.Invoke(op.Result);
    //    };
    //    Handles.Add(asyncOperation);
    //}

    //// 챕터 정보를 label 로 넘겨줘서 필요한 프리팹들, 리소스들을 로드한다.
    //public void LoadAllAsync<T>(string label, Action<string, int, int> callback) where T : UnityEngine.Object
    //{
    //    var opHandle = Addressables.LoadResourceLocationsAsync(label, typeof(T));
    //    opHandle.Completed += (op) =>
    //    {
    //        int loadCount = 0;

    //        int totalCount = op.Result.Count;

    //        foreach (var result in op.Result)
    //        {
    //            if (result.PrimaryKey.Contains(".sprite"))
    //            {
    //                LoadAsync<Sprite>(result.PrimaryKey, (obj) =>
    //                {
    //                    loadCount++;
    //                    callback?.Invoke(result.PrimaryKey, loadCount, totalCount);
    //                });
    //            }
    //            else
    //            {
    //                LoadAsync<T>(result.PrimaryKey, (obj) =>
    //                {
    //                    loadCount++;
    //                    callback?.Invoke(result.PrimaryKey, loadCount, totalCount);
    //                });
    //            }
    //        }
    //    };
    //}
    #endregion

    #region assetbundle

    // -----> 권새롬 추가 : 에셋번들
    public T Load<T>(string key) where T : Object
    {
        if (typeof(T) == typeof(Sprite))
        {
            key = key + ".sprite";
            if (resources.TryGetValue(key, out Object temp))
                return temp as T;
        }

        if (resources.TryGetValue(key, out Object resource))
        {
            if (resource as T == null)
                return resource.GetComponent<T>();
            return resource as T;
        }

        return null;
    }
    

    public void Init(List<AssetBundle> bundles)
    {
        foreach(AssetBundle bundle in bundles)
        {
            Init(bundle);
        }
    }

    public void Init(AssetBundle bundle)
    {
        if (bundle == null)
        {
            return;
        }

        Object[] objects = bundle.LoadAllAssets(); //번들을 에셋으로 로드하는 함수 호출.
        foreach (Object obj in objects)
        {
            if (obj.name.Contains(".sprite")) //.sprite 이름이 적혀있으면 sprite하나만 넣기(Texture2D는 넣지않음. (키값이 겹친다))
            {
                if (obj as Sprite && resources.ContainsKey(obj.name) == false)
                    resources.Add(obj.name, obj);
                continue;
            }
            if (resources.ContainsKey(obj.name) == false)
                resources.Add(obj.name, obj);
        }
        if(bundle.name != "uis")
            bundle.Unload(false);
    }

    public void InitVideo()
    {
        VideoClip[] clips = Resources.LoadAll<VideoClip>("Video");
        for(int i=0;i<clips.Length;i++)
            resources.Add(clips[i].name, clips[i]);
    }

    public void Clear()
    {
        resources.Clear();
        Init(AssetBundleManager.Instance.UIsBundle); //공통으로 쓰는 데이터묶음
    }
    #endregion

    /// -------------------------
}
