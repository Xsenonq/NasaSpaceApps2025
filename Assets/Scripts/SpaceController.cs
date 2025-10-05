using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpaceController : MonoBehaviour
{
    [SerializeField] private List<SpaceUnit> units;
    [SerializeField] private Transform followPoint;
    [SerializeField] private TMP_Text label;
    [SerializeField] private TMP_Text description;
    [SerializeField] private AudioSource audioSource;

    private int index;

    void Start()
    {
        followPoint.position = units[0].Pos;
        label.text = units[0].Name;
        description.text = units[0].Description;
        audioSource.clip = units[0].AudioClip;
        audioSource.Play();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            MovePrevious();
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            MoveNext();
        }
    }


    public void MoveNext()
    {
        index++;
        if (index >= units.Count)
            index = 0;
        MoveFollow();
    }

    public void MovePrevious()
    {
        index--;
        if (index < 0)
            index = units.Count - 1;
        MoveFollow();
    }

    public void MoveFollow()
    {
        label.text = "";
        description.text = "";
        audioSource.Stop();

        IEnumerator moveFollowAsync()
        {
            var moveTo = units[index].Pos;
            var startPos = followPoint.position;

            var time = 2f;
            for (var timer = 0f; timer < time; timer += Time.deltaTime)
            {
                followPoint.position = Vector3.Lerp(startPos, moveTo, timer / time);
                yield return null;
            }
            followPoint.position = moveTo;

            label.text = units[index].Name;
            description.text = units[index].Description;
            audioSource.clip = units[index].AudioClip;
            audioSource.Play();
        }

        StartCoroutine(moveFollowAsync());
    }
}
