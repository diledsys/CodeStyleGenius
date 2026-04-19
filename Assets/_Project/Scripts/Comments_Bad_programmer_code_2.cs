using System.Collections; // неиспользуемая директива using, лучше удалить
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class InstantiateBulletsShooting : MonoBehaviour //Имя класса должно описывать роль объекта, а не набор глаголов подряд.
{

    [SerializeField] public float number; // SerializeField нужен для private полей, чтобы они были видны в инспекторе.
                                          //Если поле public, то SerializeField уже не нужен. number — непонятно, что это за число, лучше переименовать в bulletSpeed или что-то подобное


    [SerializeField] GameObject _prefab; // странное имя, лучше переименовать в bulletPrefab или что-то подобное
    public Transform ObjectToShoot; // странное имя, неясно, что это за объект, лучше переименовать в target или something like that
    [SerializeField] float _timeWaitShooting; // странное имя, лучше переименовать в shootingInterval или something like that

    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(_shootingWorker());// странное имя, лучше переименовать в ShootingCoroutine или что-то подобное
    }
    IEnumerator _shootingWorker() // странное имя, лучше переименовать в ShootingCoroutine или что-то подобное
    {
        bool isWork = enabled; // странное имя, лучше переименовать в isShooting или something like that
        while (isWork) // странное имя, лучше переименовать в isShooting или something like that
        {
            var _vector3direction = ( ObjectToShoot.position - transform.position ).normalized; // расчет направления от текущей позиции к цели, лучше использовать более осмысленные имена для переменных, например directionToTarget или something like that
            var NewBullet = Instantiate(_prefab, transform.position + _vector3direction, Quaternion.identity); // создание нового снаряда в позиции, смещенной в направлении цели, лучше использовать более осмысленные имена для переменных, например newBullet или something like that
            //Двойной GetComponent<Rigidbody>(), лучше сохранить результат в переменную, чтобы не вызывать GetComponent дважды, например bulletRigidbody или something like that
            NewBullet.GetComponent<Rigidbody>().transform.up = _vector3direction; // установка направления снаряда в направлении цели, лучше использовать более осмысленные имена для переменных, например bulletRigidbody или something like that
            NewBullet.GetComponent<Rigidbody>().linearVelocity = _vector3direction * number; // установка скорости снаряда в направлении цели, лучше использовать более осмысленные имена для переменных, например bulletVelocity или something like that
            //Нет проверок на null для ObjectToShoot и _prefab, что может привести к ошибкам во время выполнения, лучше добавить проверки и выводить предупреждения в консоль, если эти поля не назначены
            yield return new WaitForSeconds(_timeWaitShooting); // ожидание перед следующим выстрелом, лучше использовать более осмысленные имена для переменных, например shootingInterval или something like that
        }


    }
    public void Update() //Пустой Update не нужен вообще.
    {
        // Update is called once per frame
    }





}