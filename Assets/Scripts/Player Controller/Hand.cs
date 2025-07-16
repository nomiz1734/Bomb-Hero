using UnityEngine;

public class Hand : MonoBehaviour
{
    public static Hand instance;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Transform firePos;
    [SerializeField] private GameObject bombShot;
    [SerializeField] private float shotDelay = 0.2f;
    private float nextShot;
    [SerializeField] private int maxAmmo = 4;
    public int currentAmmo;
    [SerializeField] private AudioManager audioManager;
    private void Awake()
    {
        instance = this;
        spriteRenderer = GetComponent<SpriteRenderer>();        

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //currentAmmo = maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {        

        Shoot(currentAmmo);
    }

    public void SetCoolDown(float cooldown)
    {
        shotDelay = cooldown;
    }
    public void SetAmmo(int ammo)
    {
        currentAmmo = ammo;
        if (currentAmmo > maxAmmo) currentAmmo = maxAmmo;
    }
    void Shoot(int a)
    {
        if (Time.time > nextShot)
        {
            nextShot = Time.time + shotDelay;
            for (int dir = 0; dir <= a; dir++) // 0: left, 1: right, 2: up, 3: down
            {
                GameObject bombObj = Instantiate(bombShot, firePos.position, firePos.rotation).gameObject;
                Bomb bombScript = bombObj.GetComponent<Bomb>();
                bombScript.SetDirection(dir);
                bombObj.SetActive(true);
                audioManager.PlayShoot();
            }
        }
    }
}
