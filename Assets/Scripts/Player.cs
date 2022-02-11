using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public static Action Winned;
    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioClip _stepSound;
    [SerializeField] private AudioClip _winSound;

    private float _step;
    private bool _inited = false;
    private bool _pressed = false;
    private Rigidbody2D _rigidbody;
    private bool _sound;
    private bool _canMove = true;

    private void Awake()
    {
        TryGetComponent(out _rigidbody);
        if(PlayerPrefs.HasKey("Sound"))
            _sound = (SoundSetting.SoundState)Enum.Parse(typeof(SoundSetting.SoundState), PlayerPrefs.GetString("Sound")) == SoundSetting.SoundState.ON;
        else
            _sound = true;
    }

    private void OnEnable()
    {
        MazeSpawner._generated += Init;
        PauseMenu.OnPause += ChangePauseState;
    }

    private void OnDisable()
    {
        MazeSpawner._generated -= Init;
        PauseMenu.OnPause -= ChangePauseState;
    }

    private void Init(Cell startCell, float step)
    {
        transform.localPosition = startCell.transform.localPosition + new Vector3(5,5);
        _step = step;
        _inited = true;
    }

    private void ChangePauseState(bool state)
    {
        _canMove = !state;
    }

    private void Update()
    {
        if(_inited && _canMove)
        {
            float ver = Input.GetAxisRaw("Vertical");
            float hor = Input.GetAxisRaw("Horizontal");

            if(!_pressed && (ver != 0 || hor != 0))
            {
                float dist = transform.TransformVector(new Vector3(_step,0)).x;
                RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector3(hor, ver), dist, LayerMask.GetMask("Wall"));
                if(!hit)
                {
                    _rigidbody.MovePosition(transform.position + transform.TransformVector(new Vector3(hor * _step, ver * _step)));
                    
                    if(_sound)
                        _source.PlayOneShot(_stepSound);
                }

                _pressed = true;
            }

            if(!Input.anyKey)
            {
                _pressed = false;
            }
        }
    }

    private void Win()
    {
        if(_sound)
            _source.PlayOneShot(_winSound);
        _canMove = false;
        Winned?.Invoke();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(!other.transform.TryGetComponent<Wall>(out _))
            Win();
    }
}
