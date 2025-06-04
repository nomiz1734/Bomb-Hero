using UnityEngine;

public class Hand : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Transform firePos;
    [SerializeField] private GameObject bulletPrefabs;
    [SerializeField] private float shotDelay = 0.15f;
    private float nextShot;
    [SerializeField] private int maxAmmo = 5;
    public int currentAmmo;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();        

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentAmmo = maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 moveInput = new Vector3(0f, 0f, 0f);
        //moveInput.y = Input.GetAxisRaw("Vertical");
        //moveInput.x = Input.GetAxisRaw("Horizontal");

        //moveInput.Normalize();

        //if (moveInput.x < 0)
        //{
        //    spriteRenderer.flipX = true;
        //}
        //else if (moveInput.x > 0)
        //{
        //    spriteRenderer.flipX = false;
        //}
        Shoot();
    }
    void Shoot()
    {
        if (currentAmmo > 0 && Time.time > nextShot)
        {
            nextShot = Time.time+ shotDelay;
            Instantiate(bulletPrefabs, firePos.position, firePos.rotation);
            //currentAmmo--;
        }
    }
}
