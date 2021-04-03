using UnityEngine;

public class TargetBox : MonoBehaviour
{
    public float hp = 50f;
    public GameObject destroyObj;




    public void TakeDamage(float amount)
    {
        // 데미지 받고 체력이 0이하가 되면 없애는 처리를 해준다
        hp -= amount;
        if(hp <= 0f)
        {
            Die();
        }
    }

    void Die() {
        //나를 없애주고 부서지는 게임오브젝트로 생성해준다.
      Instantiate(destroyObj, this.transform.position, this.transform.rotation);
        Destroy(this.gameObject);
    }
}
