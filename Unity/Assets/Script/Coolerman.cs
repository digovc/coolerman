using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coolerman : MonoBehaviour
{
    public const string DIE_CLIP = "die";
    public const string WALK_CLIP = "walk";

    private Animator animator;
    private AppCoolerman app;
    private AudioSource audioSource;
    private bool dead;
    private List<Renderer> renderers;
    private float velocity;

    public void OnMouseDown()
    {
        Die();
    }

    public void OnTriggerEnter()
    {
        Debug.Log("Colidiu.");
        app.Kill(this);
    }

    public void Start()
    {
        app = FindObjectOfType<AppCoolerman>();
        renderers = new List<Renderer>(GetComponentsInChildren<Renderer>());
        animator = GetComponentInChildren<Animator>();
        audioSource = GetComponentInChildren<AudioSource>();

        app.enemies.Add(this);
        velocity = app.currentVelocity;

        StartAlpha();
        StartPosition();
        StartCoroutine(fadeIn());
    }

    public void Update()
    {
        Move();
    }

    internal void Destroy()
    {
        Destroy(gameObject);
    }

    private void Die()
    {
        if (!dead)
        {
            dead = true;
            enabled = false;
            app.Kill(this);
            StopAllCoroutines();
            animator.Play(Animator.StringToHash(DIE_CLIP));
            audioSource.Play();
            StartCoroutine(FadeOut());
        }
    }

    private IEnumerator fadeIn()
    {
        for (float t = 0f; t < 2.5f; t += Time.deltaTime)
        {
            renderers.ForEach(objRender => FadeIn(objRender, t));
            yield return null;
        }

        renderers.ForEach(x => FadeInFinish(x));
    }

    private void FadeIn(Renderer renderer, float t)
    {
        var color = renderer.material.color;
        color.a = t * 2.5f * 1;
        renderer.material.color = color;
    }

    private void FadeInFinish(Renderer renderer)
    {
        var color = renderer.material.color;
        color.a = 1;
        renderer.material.color = color;
    }

    private IEnumerator FadeOut()
    {
        for (float t = 0f; t < 1.5f; t += Time.deltaTime)
        {
            renderers.ForEach(x => FadeOut(x, t));
            yield return null;
        }

        Destroy();
    }

    private void FadeOut(Renderer renderer, float t)
    {
        var color = renderer.material.color;
        color.a = 1 - t / 1.5f;
        renderer.material.color = color;
    }

    private void Move()
    {
        if (dead)
        {
            return;
        }

        if (app.isPlayerDead)
        {
            return;
        }

        if (renderers[0].material.color.a < 1)
        {
            return;
        }

        transform.Translate(Vector3.forward * velocity * Time.deltaTime, Space.Self);
        animator.Play(Animator.StringToHash(WALK_CLIP));
    }

    private Vector3 RandomPosition()
    {
        var distance = 25f;
        var angle = Random.Range(0, Mathf.PI * 2);
        var pos2d = new Vector2(Mathf.Sin(angle) * distance, Mathf.Cos(angle) * distance);
        return new Vector3(pos2d.x, 0, pos2d.y);
    }

    private void StartAlpha()
    {
        renderers.ForEach(x => StartAlpha(x));
    }

    private void StartAlpha(Renderer renderer)
    {
        var color = renderer.material.color;
        color.a = 0;
        renderer.material.color = color;
    }

    private void StartPosition()
    {
        var center = Vector3.zero;
        var position = RandomPosition();
        var rotation = Quaternion.FromToRotation(Vector3.forward, center - position);
        transform.position = position;
        transform.rotation = rotation;
    }
}