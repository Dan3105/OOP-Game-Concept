using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class HidingChar : MovingCharacter
{
    public float speed;
    CharacterController character;
    [SerializeField] Transform cam;
    [SerializeField] Transform checkGround;
    [SerializeField] LayerMask layerGround;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] ParticleSystem effectChar;

    private Coroutine duration;
    // Start is called before the first frame update
    void Start()
    {
        character = gameObject.GetComponent<CharacterController>();
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
        if(Input.GetKeyDown(KeyCode.Space) && !BarControl.Instance.inCD1
            && BarControl.Instance.getCurrentMana() >= 0.5f)
        {

            //skill active
            Collider[] listEnemy = Physics.OverlapSphere(transform.position, 20f, enemyLayer);
            foreach(var enemy in listEnemy)
            {
                enemy.GetComponent<EnemyAI>().beAttacked = true;
            }
            
            //set CD
            BarControl.Instance.inCD1 = true;
            BarControl.Instance.takeCD1 = 1f;
            BarControl.Instance.ConsumeMana(50f / Time.deltaTime);

            //animation
            effectChar.Play();
        }
           
    }

    
}
