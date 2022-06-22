using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PowerupIndex
{
    ORANGEMUSHROOM = 0,
    REDMUSHROOM = 1
}
public class PowerupManagerEV : MonoBehaviour
{
    // reference of all player stats affected
    public IntVariable marioJumpSpeed;
    public IntVariable marioMaxSpeed;
    public PowerupInventory powerupInventory;
    public List<GameObject> powerupIcons;

    void Start()
    {
        if (!powerupInventory.gameStarted)
        {
            powerupInventory.gameStarted = true;
            powerupInventory.Setup(powerupIcons.Count);
            resetPowerup();
        }
        else
        {
            // re-render the contents of the powerup from the previous time
            for (int i = 0; i < powerupInventory.Items.Count; i++)
            {
                Powerup p = powerupInventory.Get(i);
                if (p != null)
                {
                    AddPowerupUI(i, p.powerupTexture);
                }
            }
        }
    }

    public void resetPowerup()
    {
        for (int i = 0; i < powerupIcons.Count; i++)
        {
            powerupIcons[i].SetActive(false);
        }
    }

    void AddPowerupUI(int index, Texture t)
    {
        powerupIcons[index].GetComponent<RawImage>().texture = t;
        powerupIcons[index].SetActive(true);
    }

    public void AddPowerup(Powerup p)
    {
        powerupInventory.Add(p, (int)p.index);
        AddPowerupUI((int)p.index, p.powerupTexture);
    }

    public void UsePowerup(KeyCode k)
    {
        Powerup p;
        if (k == KeyCode.Z)
        {
            // Set something
            p = powerupInventory.Get(0);
            if (p != null)
            {
                marioJumpSpeed.ApplyChange(p.absoluteJumpBooster);
                marioMaxSpeed.ApplyChange(p.aboluteSpeedBooster);
                powerupIcons[0].SetActive(false);
                powerupInventory.Remove(0);
                StartCoroutine(removeEffect(p));
            }
        }
        else if (k == KeyCode.X)
        {
            // Do other thing
            p = powerupInventory.Get(1);
            if (p != null)
            {
                marioJumpSpeed.ApplyChange(p.absoluteJumpBooster);
                marioMaxSpeed.ApplyChange(p.aboluteSpeedBooster);
                powerupIcons[1].SetActive(false);
                powerupInventory.Remove(1);
                StartCoroutine(removeEffect(p));
            }
        }

    }

    IEnumerator removeEffect(Powerup p)
    {
        yield return new WaitForSeconds(p.duration);
        marioJumpSpeed.ApplyChange(-p.absoluteJumpBooster);
        marioMaxSpeed.ApplyChange(-p.aboluteSpeedBooster);
    }

    void OnApplicationQuit()
    {
        resetPowerup();
        powerupInventory.Clear();
        // resetValues?
    }
}