# Csharp-TSP-Client-Server-app
#
This test project contains 2 console applications: client and server. Client starts listening, creating and serializing new instance 
of the class "Point", and sending bytes to the server. Server starts listening, receiving and deserializing bytes, printing instance of the class "Point"
on the screen, and sending (string) answer back to client.
#
This project shows how to work with TCP/IP and serializing/deserializing.    
#
### How it works on the server side:
1) server starts to listen. Program pauses waiting for entry connection.
2) server accepting connection.
3) server receiving data.
4) Deserializing received data.
5) Sending answer "Thanks!" back to server.
6) Showing deserialized data on the terminal.
7) back to the the 2 step.
#
### How it works on the client side:
1)
