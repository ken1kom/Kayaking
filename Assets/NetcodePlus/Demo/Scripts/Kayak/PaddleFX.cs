using UnityEngine;

public class PaddleFX : MonoBehaviour
{
    [SerializeField] private Transform PaddleRightPos;
    [SerializeField] private Transform PaddleLeftPos;

    [SerializeField] private ParticleSystem paddleSplashParticles;

    [SerializeField] private AudioClip paddleSFX;

    public void PlayPaddleParticles(int paddleSide)
    {
        if (paddleSide == 1)
        {
            Instantiate(paddleSplashParticles, PaddleRightPos.position, Quaternion.identity);

        }
        else if(paddleSide == 2)
        {
            Instantiate(paddleSplashParticles, PaddleLeftPos.position, Quaternion.identity);

        }
    }
}
