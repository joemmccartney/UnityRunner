using UnityEngine;

public class ParticleSystemManager : MonoBehaviour {

    public ParticleSystem[] particleSystems;

	void Start () {
        GameEventsManager.GameStart += GameStart;
        GameEventsManager.GameOver += GameOver;
        GameOver();
	}

    private void GameStart ()
    {
        for(int i = 0; i < particleSystems.Length; i++)
        {
            particleSystems[i].Clear();
            ParticleSystem.EmissionModule em = particleSystems[i].emission;
            em.enabled = true;
        }
    }

    private void GameOver()
    {
        for (int i = 0; i < particleSystems.Length; i++)
        {

            ParticleSystem.EmissionModule em = particleSystems[i].emission;
            em.enabled = false;
        }
    }
}
