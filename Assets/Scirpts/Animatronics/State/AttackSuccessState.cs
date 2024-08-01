﻿using UnityEngine;
public class AttackSuccessState : IState
{
    private AnimatronicsController controller;
    private Animatronics animatronics;


    public AttackSuccessState(AnimatronicsController controller)
    {
        this.controller = controller;
        this.animatronics = controller.animatronics;
    }
    public void Enter()
    {
        Debug.Log("AttackSuccessState");
        animatronics.jumpscareObject.SetActive(true);

        animatronics.flashButton.interactable = false;
        animatronics.shockButton.interactable = false;
        animatronics.GameOverOverlay();
        animatronics.transform.GetChild(1).gameObject.SetActive(false);
    }

    public void Update()
    {
    }

    public void Exit()
    {
    }
}