using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public class SpeedyChar : MovingCharacter
{
    public float speed;
    float pSpeed;
    CharacterController character;
    [SerializeField] Transform cam;
    [SerializeField] Transform checkGround;
    [SerializeField] LayerMask layerGround;
    // Start is called before the first frame update
    void Start()
    {
        character = gameObject.GetComponent<CharacterController>();
        pSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.gameManager.isPlaying)
        {
            SkillPlayer();
            Moving(character, speed, cam, checkGround, layerGround);
        }
        
    }
    public override void SkillPlayer()
    {
        if (Input.GetKey(KeyCode.Space) && BarControl.Instance.getCurrentMana() > 0f)
        {
            speed = pSpeed * 2f;
            if(movingCharacter.magnitude >= 0.1f)
                BarControl.Instance.ConsumeMana(20);
        }
        else speed = pSpeed;
    }
}
