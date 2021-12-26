using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CoveryChar : MovingCharacter
{
    public float speed;
    CharacterController character;
    [SerializeField] Transform cam;
    [SerializeField] Transform checkGround;
    [SerializeField] LayerMask layerGround;
    [SerializeField] Collider charCollide;
    // Start is called before the first frame update
    void Start()
    {
        character = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gameManager.isPlaying)
        {
            SkillPlayer();
            Moving(character, speed, cam, checkGround, layerGround);
        }
    }

    public override void SkillPlayer()
    {
        if(Input.GetKeyDown(KeyCode.Space) && BarControl.Instance.getCurrentMana() >= 0.8f
            && !BarControl.Instance.inCD2)
        {
            BarControl.Instance.ConsumeMana(45f / Time.deltaTime);
            BarControl.Instance.inCD2 = true;
            BarControl.Instance.takeCD2 = 1.0f;
        }
        
    }


    
}
