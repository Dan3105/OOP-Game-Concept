using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(CharacterController))]
public class MovingCharacter : MonoBehaviour
{
    public float g = -9.8f;
    public float groundDistance = 0.4f;
    protected Vector3 velocity;
    protected Vector3 movingCharacter;

    public void Moving(CharacterController characterController, float speed, Transform cam,
        Transform groundCheck, LayerMask groundMask)
    {
        float xMove = Input.GetAxis("Horizontal");
        float zMove = Input.GetAxis("Vertical");

        movingCharacter = new Vector3(xMove, 0f, zMove).normalized;

        if (movingCharacter.magnitude > 0.1f)
        {
            float targetAngle = Mathf.Atan2(movingCharacter.x, movingCharacter.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            Vector3 dirMove = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;

            characterController.Move(dirMove.normalized * speed * Time.deltaTime);
        }
        velocity.y += g * Time.deltaTime * Time.deltaTime;
        characterController.Move(velocity);
        CheckGround(groundCheck, groundMask);
    }

    public virtual void SkillPlayer() { }

    public void CheckGround(Transform groundCheck, LayerMask groundMask)
    {
        bool isGround = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGround && velocity.y < 0)
        {
            velocity.y = -1f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Score")
        {
            GameManager.gameManager.UpdateScore(++GameManager.gameManager.score);
            GameManager.gameManager.SpawnGem();
            Destroy(other.gameObject);
        }

        if (other.tag == "Power")
        {
            BarControl.Instance.ConsumeMana(-80 / Time.deltaTime);
            Destroy(other.gameObject);
        }
    }

    public void CheckNearCover()
    {

        Debug.Log(Physics.CheckSphere(transform.position, 10f, 4));
        
    }
}
