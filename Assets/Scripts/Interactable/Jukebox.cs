using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jukebox : Interactable
{
    private AudioManager audioManager;
    private bool CanPlay = true;
    private GameObject _scriptsHere;
    private string soundName = "Jukebox";
    private float lengthSound;

    public override void Start()
    {
        base.Start();
        _scriptsHere = GameObject.FindGameObjectWithTag("ScriptsHere");
        audioManager = FindObjectOfType<AudioManager>();

        CanPlay = true;
        lengthSound = audioManager.LengthSound(soundName);

    }


public override void Interact()
    {
        base.Interact();

        if (CanPlay)
        {
            if (!audioManager.IsPlayingSound(soundName))
            {
                audioManager.PlaySound(soundName);
                TakeMoney(3);
                audioManager.StopSound("MainTheme");
                Invoke("AudioFinish", lengthSound);
            }
            else
            {
                audioManager.StopSound(soundName);
                StartCoroutine(WaiteToTurnOnMusic());

                audioManager.PlaySound("MainTheme");
            }
        }
        
    }

    private void AudioFinish()
    {
        TakeMoney(2);
        StartCoroutine(WaiteToTurnOnMusic());

    }

    public IEnumerator WaiteToTurnOnMusic()
    {
        CanPlay = false;

        yield return new WaitForSeconds(2f);

        CanPlay = true;

    }

    public void TakeMoney(int pay)
    {
        if (_scriptsHere.TryGetComponent(out Ipay ipay))
        {
            if (ipay.IsBalanceValid(pay))
            {
                ipay.ChangeBalance(pay);
            }
        }
    }
}
