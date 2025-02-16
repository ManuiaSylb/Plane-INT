using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using TMPro;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;

public class PlaneManager : MonoBehaviour
{
    [SerializeField] public GameObject plane1;
    [SerializeField] public GameObject plane2;
    [SerializeField] private GameObject terrain;
    [SerializeField] public float movingSpeed = 2f;
    [SerializeField] JoystickController joystickController;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI FinalscoreText;
    [SerializeField] private TextMeshProUGUI PausescoreText;
    [SerializeField] private TextMeshProUGUI LoserscoreText;

    [SerializeField] private TextMeshProUGUI lifeText;
    [SerializeField] GameObject InGameCanva;
    [SerializeField] GameObject FinishCanva;
    [SerializeField] GameObject PauseCanva;
    [SerializeField] GameObject LooserCanva;
    [SerializeField] GameObject Joystick;

    private float SpeedMultiplier;

    private float prevSpeedMultiplier;
    private Transform frontPropeller;
    private Vector3 minLimit = new Vector3(-2, 3, 0);
    private Vector3 maxLimit = new Vector3(2, 5, 0);
    private GameObject plane; // Déclaration globale de plane

    public int score;
    public int life;

    void Start()
    {
        // Déterminer quel avion utiliser en fonction des PlayerPrefs
        if (PlayerPrefs.HasKey("Plane") && PlayerPrefs.GetInt("Plane") == 2)
        {
            plane = plane2;
            plane1.SetActive(false);
        }
        else
        {
            plane = plane1;
            plane2.SetActive(false);
        }

        InGameCanva.SetActive(true);
        Joystick.SetActive(true);
        FinishCanva.SetActive(false);
        LooserCanva.SetActive(false);
        PauseCanva.SetActive(false);
        plane.SetActive(true);
        SpeedMultiplier = 1f;
        score = 0;
        life = 5;

        if (plane != null)
        {
            frontPropeller = plane.transform.Find("FrontPropeller");
            if (frontPropeller == null)
            {
                Debug.LogError("FrontPropeller not found!");
            }
        }
        else
        {
            Debug.LogError("Plane not assigned!");
        }

        UpdateScoreText();
    }

    void Update()
    {
        if (frontPropeller != null)
        {
            float rotationSpeed = 25 * 360;
            frontPropeller.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime, Space.Self);
        }

        MoveTerrain();
        Vector3 planeDirection = joystickController.PlaneDirection;

        plane.transform.position += Time.deltaTime * planeDirection.x * plane.transform.forward.normalized * SpeedMultiplier;
        plane.transform.position += Time.deltaTime * planeDirection.y * plane.transform.up.normalized * SpeedMultiplier;

        plane.transform.position = new Vector3(
            Mathf.Clamp(plane.transform.position.x, minLimit.x, maxLimit.x),
            Mathf.Clamp(plane.transform.position.y, minLimit.y, maxLimit.y),
            plane.transform.position.z
        );

        if (life <= 0)
        {
            Looser();
        }
    }

    public void NormalSpeed() => SpeedMultiplier = 1f;
    public void FastSpeed() => SpeedMultiplier = 2f;
    public void SuperFastSpeed() => SpeedMultiplier = 3f;

    public void CoinCollect()
    {
        score += ((int)SpeedMultiplier);
        UpdateScoreText();
    }

    public void HitBlocks()
    {
        life -= 1;
        UpdateLifeText();
    }

    public void Finish()
    {
        SpeedMultiplier = 0f;
        plane.SetActive(false);
        InGameCanva.SetActive(false);
        Joystick.SetActive(false);
        FinishCanva.SetActive(true);
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            PlayerPrefs.SetInt("Level_1", 1);
        }
    }

    public void Looser()
    {
        SpeedMultiplier = 0f;
        plane.SetActive(false);
        InGameCanva.SetActive(false);
        Joystick.SetActive(false);
        LooserCanva.SetActive(true);
    }

    public void Pause()
    {
        prevSpeedMultiplier = SpeedMultiplier;
        SpeedMultiplier = 0f;
        plane.SetActive(false);
        InGameCanva.SetActive(false);
        Joystick.SetActive(false);
        PauseCanva.SetActive(true);
    }

    public void Resume()
    {
        SpeedMultiplier = prevSpeedMultiplier;
        plane.SetActive(true);
        InGameCanva.SetActive(true);
        Joystick.SetActive(true);
        PauseCanva.SetActive(false);
    }

    void MoveTerrain()
    {
        if (terrain != null)
        {
            terrain.transform.position -= Vector3.forward * SpeedMultiplier * movingSpeed * Time.deltaTime;
        }
        else
        {
            Debug.LogError("Terrain not assigned!");
        }
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score\n" + score.ToString();
        }
        if (FinalscoreText != null)
        {
            FinalscoreText.text = "Score\n" + score.ToString();
        }
        if (PausescoreText != null)
        {
            PausescoreText.text = "Score\n" + score.ToString();
        }
        if (LoserscoreText != null)
        {
            LoserscoreText.text = "Score\n" + score.ToString();
        }
    }

    private void UpdateLifeText()
    {
        if (lifeText != null)
        {
            lifeText.text = "Life\n" + life.ToString();
        }
    }

    public void restart() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    public void next() => SceneManager.LoadScene(2);
    public void home() => SceneManager.LoadScene(0);
}
