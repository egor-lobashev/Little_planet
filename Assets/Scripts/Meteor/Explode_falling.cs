﻿using UnityEngine;

public class Explode_falling : MonoBehaviour
{
    private bool not_fallen = true;
    public float explode_force, explode_radius;
    public int count;
    public Vector2 cent;
    private float mass;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (not_fallen)
        {
            not_fallen = false;
            gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
            Vector2 explode_center = collision.GetContact(0).point;
            Explode(explode_center);
            cent = explode_center;
        }
    }

    void Start()
    {
        mass = GetComponent<Rigidbody2D>().mass;
    }

    void Explode(Vector2 explode_center)
    {
        foreach (Collider2D meteor in Physics2D.OverlapCircleAll(explode_center, explode_radius, 1, 0, 5))
        {
            count++;
            if (meteor.gameObject == gameObject)
                continue;
            Vector2 to_meteor = (Vector2)meteor.gameObject.transform.position - explode_center;
            float r = to_meteor.magnitude;
            float koef = 1f/(1f+0.2f*r*r);
            meteor.gameObject.GetComponent<Rigidbody2D>().AddForce(to_meteor.normalized * explode_force * mass * koef, ForceMode2D.Impulse);
        }
    }
}