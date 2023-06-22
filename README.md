# Chat Application Readme

This is a readme file for the Chat Application project.
The Chat Application allows users to communicate with each other via a TCP/IP connection and also provides a file transfer feature using FTP.

## Prerequisites

To run the chat application, you need the following:

- Microsoft Visual Studio or any C# compatible IDE.
- .NET Framework installed on your machine.
- Basic understanding of C# programming.

## Getting Started

Follow the steps below to get started with the chat application:

1. Open the Visual Studio or your preferred C# IDE.
2. Create a new C# Windows Forms Application project.
3. In the project, add a new form and name it "ChatForm.cs".
4. Replace the content of the "ChatForm.cs" file with the provided code snippets.
5. Save the file and build the project to resolve any dependencies.

## Functionality

The chat application provides the following functionality:

1. Sign In/Sign Out: Users can sign in to join the chat room and sign out to leave the room.
2. Chat Messaging: Users can send and receive text messages within the chat room.
3. User Listing: The application displays a list of currently active users in the chat room.
4. File Transfer: Users can send and receive files with other participants in the chat room.

## Usage

1. Sign In/Sign Out:
   - Launch the chat application.
   - Enter your desired nickname in the provided text box.
   - Click on the "Sign In" button to join the chat room.
   - To sign out, click on the "Sign Out" button.

2. Chat Messaging:
   - After signing in, select the users you want to chat with from the user list.
   - Enter your message in the text box at the bottom.
   - Click on the "Send" button to send the message.
   - The received messages will appear in the message history box.

3. File Transfer:
   - Select the users you want to send a file to from the user list.
   - Click on the "FTP" button to select a file from your local system.
   - Choose the file to send from the file dialog that appears.
   - A confirmation dialog will appear, asking if you want to download the file on the recipient's side.
   - If accepted, the file transfer will begin.

## Additional Notes

- The chat application uses TCP/IP sockets for communication between clients and the server.
- The code includes FTP functionality for file transfer. However, make sure you have the necessary FTP server configurations in place for the file transfer to work correctly.
- The code snippets provided in the previous messages assume that you have an understanding of C# programming. Feel free to modify and enhance the code as needed.

## Conclusion

The chat application provides a basic foundation for creating a real-time chat system with text messaging and file transfer capabilities. By building upon this code, you can further customize and expand the functionality to meet your specific requirements.
