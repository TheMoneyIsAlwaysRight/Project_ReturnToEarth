/*
 

-File-

          WriteAllBytes(string path,byte[] bytes) : 경로에 이진 파일 형태로  쓰기.
          -> File.WriteAllBytes("Assets/PakJungMin/test/sss.png", captureTexture.EncodeToPNG());

          Exists(stirng path) : 경로에 파일이 존재하는 지 여부를 bool 반환.
 

-Path-

(static)  Combine(string path,string path2) : 매개변수의 문자열을 한 경로로 합친 문자열 반환.

          
-Texture2D-

          생성자

-Rect- 

          생성자


-AssetDatabase-
            
(static)   Refresh() : 게임 중, 유니티 에디터의 Project 창을 업데이트한다. 빌드 시엔 무시된다.

-Object-
        
            Destroy();




드래그 앤 드롭 혹은 pointer 핸들러 사용시 클릭 오브젝트에 RayCast Target 프로퍼티를 가진 UI이든 뭐든 컴포넌트가 무언가가 있어야한다.
그렇지 않을 경우 작동하지 않는다.
ChatGPT
Unity에서 UI 요소들은 2D 또는 3D 오브젝트와는 다른 방식으로 처리됩니다. 여기에는 몇 가지 주요 이유가 있습니다:

렌더링 방식의 차이: UI 요소들은 Canvas 내에서 텍스처로 렌더링됩니다. 이는 Unity 엔진 내부에서 UI 요소들이 화면에 표시되는 방식을 의미합니다. 반면에 3D 또는 2D 오브젝트들은 Mesh Collider 등의 물리적인 충돌체를 통해 충돌을 감지할 수 있습니다.

Collider와의 호환성: UI 요소들은 일반적으로 Collider를 가지고 있지 않습니다. Collider는 물리적 충돌을 감지하기 위해 사용되며, 물리 엔진과 함께 작동하여 Rigidbody와 결합될 수 있습니다. UI 요소들은 사용자 인터페이스를 구성하는 데 특화되어 있어, 물리적 충돌보다는 사용자 입력 및 상호작용에 중점을 둡니다.

이벤트 시스템 사용: Unity의 UI 요소들은 EventSystem과 함께 동작하여 마우스 클릭, 터치 등의 사용자 입력을 감지하고 처리합니다. 이를 통해 UI 요소들 간의 상호작용을 구현할 수 있습니다. 따라서 UI 요소들 간의 충돌을 감지하려면 EventSystem을 사용하는 것이 일반적입니다.

Optimization: UI 요소들은 매우 많은 수가 존재할 수 있고, 이들 간의 Collider 연산은 성능에 부담을 줄 수 있습니다. 따라서 Unity는 UI 요소들의 상호작용을 효율적으로 처리하기 위해 다른 메커니즘을 사용합니다.

결론적으로, Unity의 UI 요소들은 사용자 인터페이스를 위해 최적화되어 있으며, 이러한 목적에 충실하기 위해 Collider와 같은 물리적 충돌 감지 기능을 제공하지 않습니다. 대신에 EventSystem과 Raycasting을 통해 UI 요소들 간의 상호작용을 구현하고 처리할 수 있습니다.

Awake() 함수 : Start함수 이전에 각 오브젝트가 인스턴스화 된 직후 발동된다.

 */