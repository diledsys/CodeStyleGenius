using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoPlaces : MonoBehaviour  //непонятно, что делает класс, название неестественное
{
    //поля объявлены хаотично, лучше сгруппировать по смыслу
    public float _float; // плохое имя _float выглядит как приватное поле, но оно public, нужно переименовать в более осмысленное имя

    //Нарушение инкапсуляции, поля должны быть private, а доступ к ним должен быть через методы или свойства
    public Transform AllPlacespoint; //странное имя, неясно, что это за точка У каждого GameObject уже есть Transform
    private int NumberOfPlaceInArrayPlaces; //странное имя
    public Transform[] arrayPlaces; //префикс array в имени не нужен

    void Start()
    {
        arrayPlaces = new Transform[AllPlacespoint.childCount]; // инициализация массива, размер которого равен количеству дочерних объектов у AllPlacespoint, лучше использовать List<Transform> для динамического размера массива

        for (int abcd = 0; abcd < AllPlacespoint.childCount; abcd++)  //  abcd мусор
            arrayPlaces[abcd] = AllPlacespoint.GetChild(abcd).GetComponent<Transform>(); // получение дочерних объектов и сохранение их Transform в массив, лучше использовать более осмысленные имена для переменных и методов, например GetChildTransforms или что-то подобное
    }

    public void Update() // он должен быть private, так как вызывается Unity автоматически и не должен быть доступен извне класса
    {
        var _pointByNumberInArray = arrayPlaces[NumberOfPlaceInArrayPlaces]; // странное имя, неясно, что это за точка
        transform.position = Vector3.MoveTowards(transform.position, _pointByNumberInArray.position, _float * Time.deltaTime); // _float должно быть переименовано в более осмысленное имя, например speed или moveSpeed

        if (transform.position == _pointByNumberInArray.position) // сравнение векторов на равенство может привести к проблемам из-за погрешности вычислений, лучше использовать Vector3.Distance или другой способ сравнения
            NextPlaceTakerLogic(); 
    }
    public Vector3 NextPlaceTakerLogic() //название неестественное
    {
        NumberOfPlaceInArrayPlaces++; // странное имя, лучше переименовать в currentPlaceIndex или что-то подобное

        if (NumberOfPlaceInArrayPlaces == arrayPlaces.Length) // сравнение индекса с длиной массива, лучше использовать >= для предотвращения выхода за границы массива
            NumberOfPlaceInArrayPlaces = 0; // сброс индекса на 0, если он достиг конца массива, это может привести к бесконечному циклу, если массив пустой, нужно добавить проверку на пустой массив

        var thisPointVector = arrayPlaces[NumberOfPlaceInArrayPlaces].transform.position; // странное имя, лучше переименовать в nextPointPosition или что-то подобное
        transform.forward = thisPointVector - transform.position; // установка направления движения к следующей точке, лучше использовать Vector3.Normalize для получения единичного вектора направления

        return thisPointVector; // возвращение позиции следующей точки, но этот метод не используется нигде, возможно, его стоит удалить или использовать для других целей


    }


}