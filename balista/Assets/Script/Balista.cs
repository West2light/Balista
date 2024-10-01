using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Balista : MonoBehaviour
{
    public Balista prefabBalista;
    private bool isAutoFire;
    public float speedRotation = 50f;
    public Arrow prefabArrow;
    public Arrow prefabArrowFire;

    public float attackPerSecond = 2;
    private float timerAttack;

    public float durationRage = 8f;
    private bool isRage;
    private float timerRage;

    public Arrow scriptArrow;
    public Transform arrowSpawn;
    public float fireRate = 0.5f;           // Tốc độ bắn tên lửa (thời gian giữa các phát bắn)
    private int fireCount = 0;              // Biến đếm số lần bắn thường
    private float nextFireTime = 0f;        // Thời gian tiếp theo có thể bắn
    private Coroutine autoRageRoutine;      //quản lý thời gian AutoRage();

    private int a;
    private void Awake()
    {
        isAutoFire = true;
    }

    private void Update()
    {
        Rotate();

        if (isAutoFire)
        {
            AutoFire();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isRage == false)
            {
                timerRage = 0f;
                isRage = true;
            }

            //// Nếu đang chạy AutoRage trước đó, dừng nó lại
            //if (autoRageRoutine != null)
            //{
            //    StopCoroutine(autoRageRoutine);
            //}

            //// Bắt đầu AutoRage mới
            //autoRageRoutine = StartCoroutine(AutoRage());
            //isAutoFire = false;
        }

        if (isRage)
        {
            timerRage += Time.deltaTime;
            if (timerRage >= durationRage)
            {
                isRage = false;
            }
        }
    }

    private void Rotate()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        Vector3 direction = mousePos - transform.position;

        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        Vector3 euler = transform.localEulerAngles;
        euler.z = targetAngle;
        transform.localEulerAngles = euler;

        //Quaternion targetQuaternion = Quaternion.Euler(0f, 0f, targetAngle);
        //transform.rotation = Quaternion.Lerp(transform.rotation, targetQuaternion, speedRotation * Time.deltaTime);
    }

    private void Fire()
    {
        if (isRage)
        {
            for (int i = -1; i < 2; i++)
            {
                Arrow arrow = Instantiate(prefabArrowFire, arrowSpawn.position, transform.rotation);

                Vector3 euler = arrow.transform.localEulerAngles;
                euler.z += (i * 30f);
                arrow.transform.localEulerAngles = euler;

                arrow.Active(true);
            }
        }
        else
        {
            if (fireCount < 3)
            {
                // Bắn tên lửa thường
                Arrow arrow = Instantiate(prefabArrow, arrowSpawn.position, transform.rotation);
                arrow.Active(false);

                // Tăng biến đếm fireCount
                fireCount++;
            }
            else
            {
                // Bắn tên lửa đặc biệt
                Arrow arrow = Instantiate(prefabArrowFire, arrowSpawn.position, transform.rotation);
                arrow.Active(false);

                // Reset lại biến đếm về 0 sau khi bắn tên lửa đặc biệt
                fireCount = 0;
            }
        }
    }

    private void AutoFire()
    {
        float intervalFire = 1f / attackPerSecond;

        if (isRage)
        {
            intervalFire = 1f / (attackPerSecond * 2f);
        }

        if (Time.time >= nextFireTime)
        {
            Fire();
            nextFireTime = Time.time + intervalFire;
        }
    }

    private void Rage()
    {
        //nextFireTime /= 2;
        //scriptArrow.speedMove *= 2;
        //GameObject arrowFire2 = Instantiate(prefabArrowFire);
        //// position
        //arrowFire2.transform.position = arrowSpawn.position;
        //// rotation
        //Vector3 euler2 = arrowFire2.transform.localEulerAngles;
        //euler2.z = transform.eulerAngles.z;
        //arrowFire2.transform.localEulerAngles = euler2;

        //GameObject arrowFire1 = Instantiate(prefabArrowFire);
        //// position
        //arrowFire1.transform.position = arrowSpawn.position;
        //// rotation
        //Vector3 euler1 = arrowFire1.transform.localEulerAngles;
        //euler1.z = transform.eulerAngles.z + 30f;
        //arrowFire1.transform.localEulerAngles = euler1;

        //GameObject arrowFire3 = Instantiate(prefabArrowFire);
        //// position
        //arrowFire3.transform.position = arrowSpawn.position;
        //// rotation
        //Vector3 euler3 = arrowFire3.transform.localEulerAngles;
        //euler3.z = transform.eulerAngles.z - 30f;
        //arrowFire3.transform.localEulerAngles = euler3;
    }

    IEnumerator AutoRage()
    {
        float rageDuration = 8f;  // Thời gian kéo dài chế độ Rage
        float rageEndTime = Time.time + rageDuration;

        // Trong chế độ AutoRage, bắn liên tục trong 8 giây
        while (Time.time < rageEndTime)
        {
            // Bắn mũi tên liên tục nếu đã đủ thời gian giữa các phát bắn
            if (Time.time >= nextFireTime)
            {
                Rage();
                nextFireTime = Time.time + fireRate;  // Đặt thời gian cho lần bắn tiếp theo
            }

            yield return null;  // Chờ một frame trước khi tiếp tục
        }

        //isAutoFire = true;
        // autoRageRoutine = null;
    }
}
