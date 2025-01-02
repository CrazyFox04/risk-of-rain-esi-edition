using UnityEngine;
using System;

public class Chest : MonoBehaviour
{
    private GameController gameController;
    private Transform player;
    private int row;
    private int column;
    private int id = -1;
    public bool isOpen = false;
    public Animator animator;
    
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        // player = GameObject.FindGameObjectWithTag("Player").transform;
        animator.Play("ChestReady");
    }
    
    public void set(int row, int column, int id)
    {
        this.row = row;
        this.column = column;
        this.id = id;
    }
    
    void Update()
    {
        if (id == -1) return;
        if (player == null)
        {
            try
            { 
                player = GameObject.FindGameObjectWithTag("Player").transform;
            }
            catch (Exception e)
            {
                return;
            }
        }
        if (!isOpen)
        {
            openChest();
        }
    }
    
    private async void openChest()
    {
        await SemaphoreManager.Semaphore.WaitAsync();
        try
        {
            if (Input.GetButtonDown("Use") && true && Vector2.Distance(player.position, transform.position) < 1 && !gameController.isChestEmpty(row, column, id))
            {
                gameController.openChest(row, column, id);
                isOpen = true;
                animator.Play("ChestOpen");
            }
        }
        finally
        {
            SemaphoreManager.Semaphore.Release();
        }
    }
}