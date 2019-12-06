using UnityEngine;
using UnityEngine.UI;

public class player : MonoBehaviour
{
    #region 欄位區域
    [Header("速度"), Range(0f,100f)]
    public float speed = 10f;
    [Header("跳躍"), Range(100, 2000)]
    public int jump = 300;
    [Header("是否在地上"), Tooltip("用來判定角色是否在地板上。")]
    public bool isGround = false;
    [Header("名字")]
    public string _name = "Dragon King";
    [Header("元件")]
    public Rigidbody2D r2d;
    public Animator ani;
    [Header("音效區域")]
    public AudioSource aud;
    public AudioClip soundDiamond;
    [Header("鑽石區域")]
    public int diamondCurrent;
    public int diamondTotal;
    public Text textDiamond;

    #endregion

    private void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        r2d.AddForce(new Vector2(speed * h, 0));
        ani.SetBool("跑開關", h != 0);

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) transform.eulerAngles = new Vector3(0, 180, 0);
        else if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) transform.eulerAngles = new Vector3(0, 0, 0);
    }
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround == true)
        {
            isGround = false;
            r2d.AddForce(new Vector2(0, jump));
            ani.SetTrigger("跳躍觸發");
        }
    }
    private void Dead()
    {

    }

    private void Start()
    {
        diamondTotal = GameObject.FindGameObjectsWithTag("鑽石").Length;
        textDiamond.text = "鑽石: 0 / " + diamondTotal;
    }
    private void Update()
    {
        Move();
        Jump();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "地板")
        {
            isGround = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "鑽石")
        {
            aud.PlayOneShot(soundDiamond, 1.5f);
            Destroy(collision.gameObject);
            diamondCurrent++;
            textDiamond.text = "鑽石: " + diamondCurrent + " / " + diamondTotal;
        }
    }
}

    