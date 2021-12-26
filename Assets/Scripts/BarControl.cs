using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BarControl : MonoBehaviour
{
    private Image barImage;
    private Mana mana;

    private Image inCD1Image, inCD2Image;
    private int cdTime = 5;
    public int manaRegen = 5;
    public bool inCD1; 
    public bool inCD2;
    public float takeCD1, takeCD2;
    public static BarControl Instance { get; private set; }
    private void Awake()
    {
        barImage = GameObject.Find("ManaBar").GetComponent<Image>();

        //CoolDown Skill
        inCD1Image = GameObject.Find("CoolDown1Bar/CoolDown").GetComponent<Image>();
        inCD2Image = GameObject.Find("CoolDown2Bar/InCD2").GetComponent<Image>();
        inCD1Image.fillAmount = 0f;
        inCD2Image.fillAmount = 0f;
        takeCD1 = inCD1Image.fillAmount;
        inCD1 = false;
        //Mana Bar
        barImage.fillAmount = 1f;
        mana = new Mana();

        Instance = this;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void RecoverMana()
    {
        mana.manaRecover(manaRegen);
        barImage.fillAmount = mana.getCurrentMana();
    }

    public void ConsumeMana(float amount)
    {
        mana.consumeMana(amount);
        barImage.fillAmount = mana.getCurrentMana();
    }

    private void Update()
    {
        if(inCD1)
        {
            inCD1Image.fillAmount = takeCD1;
            takeCD1 -= 1f / cdTime * Time.deltaTime;

            if (inCD1Image.fillAmount <= 0)
            {
                takeCD1 = 0f;
                inCD1Image.fillAmount = takeCD1;
                inCD1 = false;
            }
        }

        if (inCD2)
        {
            Physics.IgnoreLayerCollision(8, 6, inCD2);
            inCD2Image.fillAmount = takeCD2;
            takeCD2 -= 1f / (cdTime + 1) * Time.deltaTime;

            if (inCD2Image.fillAmount <= 0)
            {
                takeCD2 = 0f;
                inCD2Image.fillAmount = takeCD2;
                inCD2 = false;
                Physics.IgnoreLayerCollision(8, 6, inCD2);
            }
        }

        RecoverMana();
    }

    public float getCurrentMana() { return mana.getCurrentMana(); }
}

public class Mana
{
    private float MAX_MANA = 100;
    private float currMana;

    public Mana()
    {
        currMana = 100f;
    }

    public void manaRecover(int manaRegen)
    {
        currMana += manaRegen * Time.deltaTime;
        if (currMana > 100)
            currMana = 100;
    }

    public void consumeMana(float manaAmount)
    {
        currMana -= manaAmount * Time.deltaTime;
        if (currMana < 0)
            currMana = -2;
    }
    public float getCurrentMana()
    {
        return currMana / MAX_MANA;
    }

}