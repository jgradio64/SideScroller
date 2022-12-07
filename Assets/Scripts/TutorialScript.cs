using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    [SerializeField] GameObject[] introSlides;
    // Start is called before the first frame update
    void Start()
    {
        foreach (var slide in introSlides)
        {
            slide.SetActive(false);
        }
        introSlides[0].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        // For the first slide
        if (Input.GetKeyDown("d") && introSlides[0].activeSelf)
        {
            introSlides[0].SetActive(false);
            introSlides[1].SetActive(true);
        }
        if (Input.GetKeyDown("a") && introSlides[1].activeSelf)
        {
            introSlides[1].SetActive(false);
            introSlides[2].SetActive(true);
        }
        if (Input.GetButtonDown("Jump") && introSlides[2].activeSelf)
        {
            introSlides[2].SetActive(false);
            introSlides[3].SetActive(true);
        }
        if (Input.GetButtonDown("Fire1") && introSlides[3].activeSelf)
        {
            introSlides[3].SetActive(false);
            introSlides[4].SetActive(true);
        }
        if (Input.GetButtonDown("Fire2") && introSlides[4].activeSelf)
        {
            introSlides[4].SetActive(false);
            introSlides[5].SetActive(true);
        }

    }

    IEnumerator NextSlide(int index)
    {
        introSlides[index].SetActive(false);
        yield return new WaitForSeconds(.1f);
        introSlides[index+1].SetActive(true);
    }
}
