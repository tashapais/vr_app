using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Http;
using System.Threading.Tasks;

public class NewBehaviourScript : MonoBehaviour
{
    private readonly string apiKey = "YOUR_API_KEY_HERE";
    private readonly string apiURL = "https://api.openai.com/v1/chat/completions";

    // Start is called before the first frame update
    async void Start()
    {
        string prompt = "Hello, world!";
        string response = await GetChatGPTResponse(prompt);
        Debug.Log(response);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public async Task<string> GetChatGPTResponse(string prompt)
    {
        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

            var requestData = new
            {
                model = "text-davinci-003",
                prompt = prompt,
                temperature = 0.5,
                max_tokens = 100
            };
            string json = JsonUtility.ToJson(requestData);

            HttpResponseMessage response = await client.PostAsync(apiURL,
                new StringContent(json, System.Text.Encoding.UTF8, "application/json"));

            string result = await response.Content.ReadAsStringAsync();
            return result;
        }
    }
}
