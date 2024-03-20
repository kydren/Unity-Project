using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class code : MonoBehaviour
{
    public float toc_to;
    public int di_chuyen;
    public int do_cao;
    public bool duocPhepNhay;

    private Rigidbody2D cd;
    // Start is called before the first frame update
    void Start()
    {
        cd = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //move
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            di_chuyen = -1;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            di_chuyen = 1;
        }
        else 
        {
            di_chuyen = 0;
        }
        transform.Translate(Vector2.right * di_chuyen * toc_to * Time.deltaTime);
        //jump
        
        if(Input.GetKeyDown(KeyCode.Space) && duocPhepNhay)
        {
            cd.AddForce(Vector2.up * do_cao, ForceMode2D.Impulse);
        }
    }
    private void OnTriggerEnter2D(Collider2D hitboxkhac)
    {
        if(hitboxkhac.gameObject.tag == "san")
        {
            duocPhepNhay = true;
        }
    }
    private void OnTriggerExit2D(Collider2D hitboxkhac)
    {
        if (hitboxkhac.gameObject.tag == "san")
        {
            duocPhepNhay = false;
        }
    }
}


