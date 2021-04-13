using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    //config para
    [SerializeField] AudioClip breakSound;
    [SerializeField] GameObject blockSparklesVFX;
   // [SerializeField] int maxHits;
    [SerializeField] Sprite[] hitSprites;

    //cached reference
    Level level;
    GameStatus gamestatus;

    //state variables
    [SerializeField] int timesHit; 

    private void Start()
    {
        gamestatus = FindObjectOfType<GameStatus>();
        CountBreakableBlocks();

    }

    private void CountBreakableBlocks()
    {
        level = FindObjectOfType<Level>();


        if (tag == "Breakable")
        {
            
            level.CountBlocks();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(tag == "Breakable")
        {
            HandleHit();
        }

    }

    private void HandleHit()
    {
        timesHit++;
        int maxHits = hitSprites.Length + 1;
        if (timesHit >= maxHits)
        {
            DestroyBlock();
        }
        else
        {
            ShowNextHitSprite();
        }
    }

    private void ShowNextHitSprite()
    {
        int spriteIndex = timesHit - 1;
        if (hitSprites[spriteIndex] != null)
        {
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        }
        else
        {
            Debug.LogError("Block Sprite is missing from Array" + gameObject.name);
        }
    }

    private void DestroyBlock()
    {
        PlayBlockDestroyedSFX();
        Destroy(gameObject);
        level.BlockDestroyed();
        TriggerSparklesVFX();

    }

    private void PlayBlockDestroyedSFX()
    {
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
        gamestatus.AddToScore();
    }

    private void TriggerSparklesVFX()
    {
        GameObject sparkles = Instantiate(blockSparklesVFX, transform.position, transform.rotation);
        Destroy(sparkles, 2f);
    }
}
