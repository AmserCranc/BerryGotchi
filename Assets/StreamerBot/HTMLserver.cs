using UnityEngine;
using System.Net;
using System.Text;
using System.Threading;
using System.IO;
using UnityEditor.PackageManager;
using Newtonsoft.Json.Utilities;


public class SB_HTMLserver : MonoBehaviour
{
    private HttpListener listener;
    private Thread serverThread;
    private bool running;

    public int port = 7474;

    void Start()
    {
        StartServer();
    }

    void OnDestroy()
    {
        StopServer();
    }

    void StartServer()
    {
        listener = new HttpListener();
        listener.Prefixes.Add($"http://localhost:{port}/");
        listener.Start();

        running = true;

        serverThread = new Thread(HandleRequests);
        serverThread.Start();

        Debug.Log($"HTTP Server started on port {port}");
    }

    void HandleRequests()
    {
        while (running)
        {
            try
            {
                var context = listener.GetContext(); 
                var request = context.Request;

                string body = "";

                using (var reader = new StreamReader(request.InputStream, request.ContentEncoding))
                {
                    body = reader.ReadToEnd();
                }

                Debug.Log("Received from Streamer.bot: " + body);

                UnityMainThreadEnqueue(() =>
                {
                    ProcessCommand(body);
                });

                byte[] responseBytes = Encoding.UTF8.GetBytes("OK");
                context.Response.OutputStream.Write(responseBytes, 0, responseBytes.Length);
                context.Response.Close();
            }
            catch (System.Exception e)
            {
                if (running)
                    Debug.LogError(e.Message);
            }
        }
    }

    void StopServer()
    {
        running = false;

        listener?.Stop();
        listener?.Close();

        if (serverThread != null && serverThread.IsAlive)
            serverThread.Abort();
    }

    private static readonly System.Collections.Generic.Queue<System.Action> mainThreadActions = new System.Collections.Generic.Queue<System.Action>();

    void Update()
    {
        lock (mainThreadActions)
        {
            while (mainThreadActions.Count > 0)
            {
                mainThreadActions.Dequeue()?.Invoke();
            }
        }
    }

    public static void UnityMainThreadEnqueue(System.Action action)
    {
        lock (mainThreadActions)
        {
            mainThreadActions.Enqueue(action);
        }
    }

    void ProcessCommand(string json)
    {
        Debug.Log("Processing command " + json);
        Command c = JsonUtility.FromJson<Command>(json);

        switch(c.commandID)
        {
            case "action0":
                Actions.Add(
                    Actions.PetBerries,
                    0,
                    0.2f,
                    2,
                    1,
                    Actions.queue.Count);
                break;

            case "action1":
                Actions.Add(
                    Actions.ThrowBerry,
                    0,
                    0.2f,
                    0,
                    0,
                    Actions.queue.Count);
                break;

            default:
                Debug.Log("action " + c.commandID + " not found");
                break;
        }
    }
}