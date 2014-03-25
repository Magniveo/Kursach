using UnityEngine;
using System.Collections;

public class PinkyAI : MonoBehaviour
{
	// скорость ходьбы и скорость поворота в секунду
	public float moveSpeed = 2;
	public float turnSpeed = 90;
	
	private CharacterController _controller;
	private Transform _thisTransform;
	private Transform _playerTransform;
	
	public void Start()
	{
		// Получаем контроллер
		_controller = GetComponent<CharacterController>();
		
		// Получаем компонент трансформации объекта, к которому привязан данный компонент
		_thisTransform = transform;
		
		// Получаем компонент трансформации игрока
		PlayerPrefs player = (PlayerPrefs)FindObjectOfType(typeof(PlayerPrefs));
		_playerTransform = player.transform;
	}
	
	// Все что связано с физикой выполняем в FixedUpdate
	public void FixedUpdate()
	{
		// направление на игрока
		Vector3 playerDirection = (_playerTransform.position - _thisTransform.position).normalized;
		
		// угол поворота на игрока
		float angle = Vector3.Angle(_thisTransform.forward, playerDirection);
		
		// максимальный угол поворота на текущем кадре
		float maxAngle = turnSpeed * Time.deltaTime;
		
		// Вычисляем прямой поворот на игрока
		Quaternion rot = Quaternion.LookRotation(_playerTransform.position - _thisTransform.position);
		
		// поворачиваем врага на игрока с учетом скорости поворота
		if (maxAngle < angle)
		{
			_thisTransform.rotation = Quaternion.Slerp(_thisTransform.rotation, rot, maxAngle / angle);
		}
		else
		{
			_thisTransform.rotation = rot;
		}
		
		// если дистанция до игрока больше трех метров
		if (Vector3.Distance(_playerTransform.position, _thisTransform.position) > 3.0f)
		{
			// двигаемся к игроку
			_controller.Move(_thisTransform.forward * moveSpeed * Time.deltaTime);
		}
		else // если меньше или равна трем метрам
		{
			// здесь например стреляем в игрока
		}
		
		// гравитация
		_controller.Move(Vector3.down * 10.0f * Time.deltaTime);
	}
}
