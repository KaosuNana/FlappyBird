/*
 Create By Ray : ray@raymix.net @ 极视教育
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipelineManager : MonoBehaviour {

    public GameObject pipelineTemplate;

    List<Pipeline> pipelines = new List<Pipeline>();

    public float speed;

	// Use this for initialization
	void Start () {
        
	}

    Coroutine runner = null;

    public void Init()
    {
        for (int i = 0; i < pipelines.Count; i++)
        {
            Destroy(pipelines[i].gameObject);
        }
        pipelines.Clear();
    }


    public void StartRun()
    {

        runner = StartCoroutine(GenetatePipelines());
    }


    public void Stop()
    {
        StopCoroutine(runner);
        for (int i = 0; i < pipelines.Count; i++)
            pipelines[i].enabled = false;
    }

    IEnumerator GenetatePipelines()
    {
        for(int i=0;i<3;i++)
        {
            if (pipelines.Count < 3)
                CreatePipeline();
            else
            {
                pipelines[i].enabled = true;
                pipelines[i].Init();
            }

            yield return new WaitForSeconds(speed);
        }
    }

    void CreatePipeline()
    {
        if (pipelines.Count < 3)
        {
            GameObject obj = Instantiate(pipelineTemplate, this.transform);
            Pipeline p = obj.GetComponent<Pipeline>();
            pipelines.Add(p);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
