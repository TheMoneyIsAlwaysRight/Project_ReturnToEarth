using UnityEngine;

public class GroundBlock : MonoBehaviour
{

    [SerializeField] JumpPlayerController playerController;

    public bool noRemoved = false;


    private void Start()
    {
        if (noRemoved == false) //플랫폼일때,
        {
            int random = Random.Range(0, 2);
            if (random == 0)
                noRemoved = false;
            else
                noRemoved = true;
        }
    }


    public void PlayDisappear()
    {
        if (noRemoved == false)
            Invoke("Disappear", 1.5f); // 1.5초 후에 Disappear 함수 호출
    }

    private void Disappear()
    {
        gameObject.SetActive(false); // GameObject를 비활성화하여 사라지게 함
        Invoke("Reappear", 5f); // 5초 후에 Reappear 함수 호출

    }

    private void Reappear()
    {
        gameObject.SetActive(true); // GameObject를 다시 활성화하여 나타나게 함
    }
}
