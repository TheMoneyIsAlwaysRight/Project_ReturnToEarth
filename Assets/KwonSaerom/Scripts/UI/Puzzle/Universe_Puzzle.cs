using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class Universe_Puzzle : BasePuzzle
{
    public enum PlanetState
    {
        Mercury = 0,
        Venus = 1,
        Earth = 2,
        Mars = 3,
        Jupiter = 4,
        Saturn = 5,
        Uranus = 6,
        Neptune = 7,
    }

    [Serializable]
    public class Planet
    {
        public PlanetState state;
        public Sprite sprite;
    }

    [SerializeField] List<Planet> planets;
    [SerializeField] List<PlanetToken> tokens;

    private List<bool> isCheck = new List<bool>();
    private List<int> answer = new List<int>();
    private bool isClear = false;

    private void Awake()
    {
        //isCheck 초기화
        for (int i = 0; i < planets.Count; i++)
            isCheck.Add(false);

        for (int i = 0; i < tokens.Count; i++)
        {
            int random = UnityEngine.Random.Range(0, planets.Count);
            while (true)
            {
                if (isCheck[random] == false)
                {
                    tokens[i].SetHandleInfo(planets[random]);
                    answer.Add((int)planets[random].state);
                    isCheck[random] = true;
                    break;
                }
                random = UnityEngine.Random.Range(0, planets.Count);
            }
        }
    }

    private void Update()
    {
        if (!isClear && Input.GetMouseButtonUp(0))
        {
            CheckLeverState();
        }
    }

    public void CheckLeverState()
    {
        if (IsPuzzleClear)
            return;

        bool isComplete = true;

        for (int i=0;i<tokens.Count;i++)
        {
            //Debug.Log($"{answer[i]} : {tokens[i].CurPos}");
            if (answer[i] != tokens[i].CurPos)
            {
                isComplete = false;
                break;
            }
        }

        if (isComplete)
        {
            //DevModeManager.Instance.Debug("Universe_Puzzle Complete");
            IsPuzzleClear = true;
        }
    }
}
