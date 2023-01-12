using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Character_Movement : MonoBehaviour
{
    [Header ("General")]
    public Rigidbody character;
    public GameObject curiosity;
    public int score = 0;
    bool in_wind = false;

    public float motor_force;
    public float break_force;
    float current_break_force;
    bool breaking;

    float horizontal;
    float vertical;

    [Header ("Beacons")]
    public GameObject beacon_1_empty;
    public GameObject beacon_2_empty;
    public GameObject beacon_3_empty;
    public GameObject beacon_1;
    public GameObject beacon_2;
    public GameObject beacon_3;
    public GameObject icon;

    [Header ("Wind")]
    GameObject wind_zone;
    public Text wind_speed;
    public Text wind_direction;
    bool is_playing = false;
    bool is_moving = true;
    bool slowed_down = false;
    bool started = false;
    public AudioSource wind_quiet;
    public AudioSource wind_loud;
    public AudioSource moving;
    public AudioSource claim;
    bool claim_1 = false;
    bool claim_2 = false;
    bool claim_3 = false;
    public GameObject timer_alert;
    public GameObject alert;
    public GameObject reason_1;
    public GameObject reason_2;
    public GameObject found_alert;
    public GameObject timer;

    [Header("Wheels")]
    public WheelCollider wheel_left_1;
    public WheelCollider wheel_left_2;
    public WheelCollider wheel_left_3;
    public WheelCollider wheel_right_1;
    public WheelCollider wheel_right_2;
    public WheelCollider wheel_right_3;

    void Get_Input()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        breaking = Input.GetKey(KeyCode.Space);
    }

    void Handle_Motor()
    {
        wheel_left_1.motorTorque = vertical * motor_force;
        wheel_left_2.motorTorque = vertical * motor_force;
        wheel_left_3.motorTorque = vertical * motor_force;

        wheel_right_1.motorTorque = vertical * motor_force;
        wheel_right_2.motorTorque = vertical * motor_force;
        wheel_right_3.motorTorque = vertical * motor_force;

        current_break_force = breaking ? break_force : 0f;

        if (breaking)
        {
            Apply_Breaking();
        }

        else
        {
            Stop_Breaking();
        }
    }

    void Apply_Breaking()
    {
        wheel_left_1.brakeTorque = current_break_force;
        wheel_left_2.brakeTorque = current_break_force;
        wheel_left_3.brakeTorque = current_break_force;

        wheel_right_1.brakeTorque = current_break_force;
        wheel_right_2.brakeTorque = current_break_force;
        wheel_right_3.brakeTorque = current_break_force;
    }

    void Stop_Breaking()
    {
        wheel_left_1.brakeTorque = 0f;
        wheel_left_2.brakeTorque = 0f;
        wheel_left_3.brakeTorque = 0f;

        wheel_right_1.brakeTorque = 0f;
        wheel_right_2.brakeTorque = 0f;
        wheel_right_3.brakeTorque = 0f;
    }

    void Handle_Steering()
    {
        wheel_left_1.steerAngle = horizontal;
        wheel_right_1.steerAngle = horizontal;
    }

    void OnTriggerEnter(Collider coll)
    {
        if(coll.gameObject.tag == "Wind")
        {
            wind_zone = coll.gameObject;
            in_wind = true;
        }

        else if(coll.gameObject.tag == "Alert")
        {
            if (!timer.activeSelf)
            {
                alert.SetActive(true);
                reason_1.SetActive(true);
            }

            else
            {
                alert.SetActive(true);
                reason_2.SetActive(true);
            }
        }
    }

    void OnTriggerExit(Collider coll)
    {
        if(coll.gameObject.tag == "Wind")
        {
            in_wind = false;

            StartCoroutine(FadeOut(wind_loud, 6f));
            StartCoroutine(FadeOut(wind_quiet, 6f));
        }

        else if (coll.gameObject.tag == "Alert")
        {
            alert.SetActive(false);
            reason_1.SetActive(false);
            reason_2.SetActive(false);

            alert.GetComponent<Alert>().toggle = true;
        }
    }

    void FixedUpdate()
    {
        Get_Input();

        Handle_Motor();

        Handle_Steering();

        if (Input.GetKey(KeyCode.A))
        {
            Quaternion rotation = Quaternion.Euler(new Vector3(0, -20, 0) * Time.fixedDeltaTime);

            character.MoveRotation(character.rotation * rotation);
        }

        if (Input.GetKey(KeyCode.D))
        {
            Quaternion rotation = Quaternion.Euler(new Vector3(0, 20, 0) * Time.fixedDeltaTime);

            character.MoveRotation(character.rotation * rotation);
        }

        if (in_wind)
        {
            float speed = wind_zone.GetComponent<Wind>().strength;
            Vector3 direction = wind_zone.GetComponent<Wind>().direction;
            string angle = wind_zone.GetComponent<Wind>().angle;

            character.AddForce(direction * speed);

            if (speed < 2 && !is_playing)
            {
                StartCoroutine(Quiet());
            }

            else if (speed >= 2 && !is_playing)
            {
                StartCoroutine(Loud());
            }

            if (direction == Vector3.zero)
            {
                wind_speed.text = "0 kn";
                wind_direction.text = "No Wind";
            }

            else
            {
                wind_speed.text = speed.ToString() + " kn";
                wind_direction.text = angle;
            }
        }

        else
        {
            wind_speed.text = "0 kn";
            wind_direction.text = "No Wind";
        }

        if (score == 1 && !claim_1)
        {
            claim.Play();
            claim_1 = true;

            beacon_1_empty.SetActive(false);
            beacon_1.SetActive(true);
        }

        else if (score == 2 && !claim_2)
        {
            claim.Play();
            claim_2 = true;

            beacon_2_empty.SetActive(false);
            beacon_2.SetActive(true);
        }

        else if (score == 3 && !claim_3)
        {
            claim.Play();
            claim_3 = true;

            beacon_3_empty.SetActive(false);
            beacon_3.SetActive(true);

            curiosity.SetActive(true);
            icon.SetActive(true);

            if (timer_alert.activeSelf)
            {
                timer_alert.SetActive(false);

                StartCoroutine(Show_Plus(10));
            }

            else
            {
                StartCoroutine(Show(10));
            }

            
        }

        if (character.velocity.magnitude >= 1f && is_moving)
        {
            started = true;

            StartCoroutine(Move());

            slowed_down = false;
        }

        else if (character.velocity.magnitude < 1f && started && !slowed_down)
        {
            StartCoroutine(FadeOut(moving, 1f));

            slowed_down = true;
        }
    }

    IEnumerator Move()
    {
        is_moving = false;

        moving.Play();

        yield return new WaitForSeconds(14.3f);

        is_moving = true;
    }

    IEnumerator Show(float time)
    {
        found_alert.SetActive(true);

        yield return new WaitForSeconds(time);

        found_alert.SetActive(false);
    }

    IEnumerator Show_Plus(float time)
    {
        found_alert.SetActive(true);

        yield return new WaitForSeconds(time);

        found_alert.SetActive(false);

        timer_alert.SetActive(true);

        timer_alert.GetComponent<Alert>().toggle = true;
    }

    IEnumerator Quiet()
    {
        is_playing = true;

        wind_quiet.Play();

        yield return new WaitForSeconds(108f);

        is_playing = false;
    }

    IEnumerator Loud()
    {
        is_playing = true;

        wind_loud.Play();

        yield return new WaitForSeconds(33f);

        is_playing = false;
    }

    IEnumerator FadeOut(AudioSource audio_source, float time)
    {
        float startVolume = audio_source.volume;

        while (audio_source.volume > 0)
        {
            audio_source.volume -= startVolume * Time.deltaTime / time;

            yield return null;
        }

        audio_source.Stop();
        audio_source.volume = startVolume;
        is_playing = false;
    }
}
