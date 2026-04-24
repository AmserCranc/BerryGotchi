// using System;
// using System.Net.WebSockets;
// using System.Text;
// using System.Threading;
// using System.Threading.Tasks;
// using UnityEngine;
// using System.Collections.Concurrent;
// using Newtonsoft.Json;


// public class StreamerBotListen : MonoBehaviour
// {
//     private ClientWebSocket socket;
//     private CancellationTokenSource cancelSource;

//     private ConcurrentQueue<string> messageQueue = new();

//     async void Start()
//     {
//         socket = new ClientWebSocket();
//         cancelSource = new CancellationTokenSource();

//         try
//         {
//             await socket.ConnectAsync(new Uri("ws://127.0.0.1:8080"), cancelSource.Token);
//             Debug.Log("Connected to Streamer.bot");

//             await socket.SendAsync(
//                 Encoding.UTF8.GetBytes("{\n\"request\":\"Subscribe\",\n\"id\":\"my-subscribe-id\",\n\"events\":{\n\"command\":[\n\"!testAction0\"\n]\n},\n}"),
//                 WebSocketMessageType.Text,
//                 true,
//                 cancelSource.Token
//             );            

//             _ = ReceiveLoop();
//         }
//         catch (Exception e)
//         {
//             Debug.LogError("Connection failed: " + e.Message);
//         }
//     }

//     void Update()
//     {
//         Debug.Log("Socket state: " + socket.State);
//         while (messageQueue.TryDequeue(out var message))
//         {
//             Debug.Log("Received: " + message);

//             HandleMessage(message);
//         }
//     }

    
//     async Task ReceiveLoop()
//     {
//         byte[] buffer = new byte[4096];

//         while (socket.State == WebSocketState.Open)
//         {
//             byte[] messageBuffer = new byte[8192];
//             int offset = 0;

//             WebSocketReceiveResult result;

//             do
//             {
//                 result = await socket.ReceiveAsync(
//                     new ArraySegment<byte>(buffer),
//                     cancelSource.Token
//                 );

//                 Array.Copy(buffer, 0, messageBuffer, offset, result.Count);
//                 offset += result.Count;

//             } while (!result.EndOfMessage);

//             string message = Encoding.UTF8.GetString(messageBuffer, 0, offset);

//             messageQueue.Enqueue(message);
//         }
//     }

//     void HandleMessage(string JASON)
//     {
//         try
//         {
//             SBEvent evt = JsonConvert.DeserializeObject<SBEvent>(JASON);

//             if (evt == null) 
//                 return;

//             Debug.Log($"Event: {evt.@event}");

//             switch (evt.@event)
//             {
//                 case "Sub":
//                     Debug.Log($"User subscribed: {evt.data.user}");
//                     break;

//                 case "Follow":
//                     Debug.Log($"New follower: {evt.data.user}");
//                     break;
//             }
//         }
//         catch (Exception e)
//         {
//             Debug.LogWarning("Failed to parse message: " + e.Message);
//         }
//     }
// }