using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Vector3 playerInput;
    Rigidbody2D rb;
    [SerializeField] float speed;
    ChangeOutfit outfitScript;
    bool outfitChanging;

    [Header("Animators")]
    [SerializeField] Animator headAnim;
    [SerializeField] Animator torsoAnim;
    [SerializeField] Animator legAnim;
    [SerializeField] Animator shoeAnim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        outfitScript = GetComponent<ChangeOutfit>();
        Expressions.StartTalk += ChangingOutfitHandler;
        outfitScript.OnOutfitChanging += ChangingOutfitHandler;
        outfitScript.OnStore += ChangingOutfitHandler;
        outfitScript.OutOutfitChanging += OutChangingOutfitHandler;
    }

    private void FixedUpdate()
    {
        Move();
        WalkAnim(headAnim);
        WalkAnim(torsoAnim);
        WalkAnim(legAnim);
        WalkAnim(shoeAnim);
    }

    void Move()
    {
        if (outfitChanging) { return; }

        playerInput.x = Input.GetAxis("Horizontal");
        playerInput.y = Input.GetAxis("Vertical");
        playerInput.Normalize();

        rb.velocity = playerInput * speed;
    }
    void ChangingOutfitHandler()
    {
        rb.velocity = Vector3.zero;
        outfitChanging = true;
    }
    void OutChangingOutfitHandler()
    {
        outfitChanging = false;
    }

    void WalkAnim(Animator bodyPartAnimator)
    {
        if (outfitChanging) 
        {
            bodyPartAnimator.SetBool("Stop", true);
            bodyPartAnimator.SetFloat("WalkHorizontal", 0);
            bodyPartAnimator.SetFloat("WalkVertical", 0);
            return; 
        }

        bodyPartAnimator.SetBool("Stop", false);
        bodyPartAnimator.SetFloat("WalkHorizontal", Input.GetAxis("Horizontal"));
        bodyPartAnimator.SetFloat("WalkVertical", Input.GetAxis("Vertical"));


        if(Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0) 
        {
            bodyPartAnimator.SetBool("Stop", true);
        }
    }
}
