#pragma once
#define _WINSOCK_DEPRECATED_NO_WARNINGS
#include "WinSock2.h"
#pragma comment(lib, "ws2_32.lib")


#include <iostream>
#include <string>
#include <conio.h>

using namespace std;

string GetErrorMsgText(int code) {
    string msgText;
    switch (code) {
    case WSAEINTR:              msgText = "WSAEINTR: Interrupted function call"; break;
    case WSAEACCES:             msgText = "WSAEACCES: Permission denied"; break;
    case WSAEFAULT:             msgText = "WSAEFAULT: Bad address"; break;
    case WSAEINVAL:             msgText = "WSAEINVAL: Invalid argument"; break;
    case WSAEMFILE:             msgText = "WSAEMFILE: Too many open files"; break;
    case WSAEWOULDBLOCK:        msgText = "WSAEWOULDBLOCK: Resource temporarily unavailable"; break;
    case WSAEINPROGRESS:        msgText = "WSAEINPROGRESS: Operation now in progress"; break;
    case WSAEALREADY:           msgText = "WSAEALREADY: Operation already in progress"; break;
    case WSAENOTSOCK:           msgText = "WSAENOTSOCK: Socket operation on non-socket"; break;
    case WSAEDESTADDRREQ:       msgText = "WSAEDESTADDRREQ: Destination address required"; break;
    case WSAEMSGSIZE:           msgText = "WSAEMSGSIZE: Message too long"; break;
    case WSAEPROTOTYPE:         msgText = "WSAEPROTOTYPE: Protocol wrong type for socket"; break;
    case WSAENOPROTOOPT:        msgText = "WSAENOPROTOOPT: Bad protocol option"; break;
    case WSAEPROTONOSUPPORT:    msgText = "WSAEPROTONOSUPPORT: Protocol not supported"; break;
    case WSAESOCKTNOSUPPORT:    msgText = "WSAESOCKTNOSUPPORT: Socket type not supported"; break;
    case WSAEOPNOTSUPP:         msgText = "WSAEOPNOTSUPP: Operation not supported"; break;
    case WSAEPFNOSUPPORT:       msgText = "WSAEPFNOSUPPORT: Protocol family not supported"; break;
    case WSAEAFNOSUPPORT:       msgText = "WSAEAFNOSUPPORT: Address family not supported by protocol family"; break;
    case WSAEADDRINUSE:         msgText = "WSAEADDRINUSE: Address already in use"; break;
    case WSAEADDRNOTAVAIL:      msgText = "WSAEADDRNOTAVAIL: Cannot assign requested address"; break;
    case WSAENETDOWN:           msgText = "WSAENETDOWN: Network is down"; break;
    case WSAENETUNREACH:        msgText = "WSAENETUNREACH: Network is unreachable"; break;
    case WSAENETRESET:          msgText = "WSAENETRESET: Network dropped connection on reset"; break;
    case WSAECONNABORTED:       msgText = "WSAECONNABORTED: Software caused connection abort"; break;
    case WSAECONNRESET:         msgText = "WSAECONNRESET: Connection reset by peer"; break;
    case WSAENOBUFS:            msgText = "WSAENOBUFS: No buffer space available"; break;
    case WSAEISCONN:            msgText = "WSAEISCONN: Socket is already connected"; break;
    case WSAENOTCONN:           msgText = "WSAENOTCONN: Socket is not connected"; break;
    case WSAESHUTDOWN:          msgText = "WSAESHUTDOWN: Cannot send after socket shutdown"; break;
    case WSAETIMEDOUT:          msgText = "WSAETIMEDOUT: Connection timed out"; break;
    case WSAECONNREFUSED:       msgText = "WSAECONNREFUSED: Connection refused"; break;
    case WSAEHOSTDOWN:          msgText = "WSAEHOSTDOWN: Host is down"; break;
    case WSAEHOSTUNREACH:       msgText = "WSAEHOSTUNREACH: No route to host"; break;
    case WSASYSCALLFAILURE:     msgText = "WSASYSCALLFAILURE: System call failure"; break;
    default:                    msgText = "***ERROR***: Unknown error"; break;
    }
    return msgText;
}

string SetErrorMsgText(string msgText, int code) {
    return msgText + " " + GetErrorMsgText(code);
}

int main() {
    setlocale(LC_ALL, "ru");
    WSADATA wsaData;
    try
    {
        if (WSAStartup(MAKEWORD(2, 0), &wsaData) != 0)
            throw  SetErrorMsgText("Startup:", WSAGetLastError());

        //cout << "WSAStartup succeeded. Winsock initialized." << endl;
        cout << "Server Console" << endl;





        SOCKET sS;
        if ((sS = socket(AF_INET, SOCK_DGRAM, NULL)) == INVALID_SOCKET)
            throw SetErrorMsgText("Socket:", WSAGetLastError());
        //cout << "Socket open succeeded." << endl;


        SOCKADDR_IN serv;
        serv.sin_family = AF_INET;           // используется IP-адресация  
        serv.sin_port = htons(2000);          // порт 2000
        serv.sin_addr.s_addr = INADDR_ANY;   // любой собственный IP-адрес 


        if (bind(sS, (LPSOCKADDR)&serv, sizeof(serv)) == SOCKET_ERROR)
            throw  SetErrorMsgText("Bind:", WSAGetLastError());
        //cout << "Socket bind succeeded." << endl;


        int k = 0;
        char buf[1024];   //буфер
        int  lb = 0;
        int lc;
        //string msg;
        while (true)
        {
            SOCKADDR_IN clnt;               // параметры  сокета клиента
            memset(&clnt, 0, sizeof(clnt));   // обнулить память
            lc = sizeof(clnt);
            lb = 0;                    //количество принятых байт
            if ((lb = recvfrom(sS, buf, sizeof(buf), NULL, (sockaddr*)&clnt, &lc)) == SOCKET_ERROR)
                throw  SetErrorMsgText("Recieve:", WSAGetLastError());
            if (lb == 1) {
                break;
            }
            //buf[lb] = '\0';
            //cout << lb << endl;
            if (lb <= 50) {
                cout << inet_ntoa(clnt.sin_addr) << ": " << buf << endl;

                k++;
                int  lobuf = 0;  //количество отправленных байт 
                if ((lobuf = sendto(sS, buf, strlen(buf) + 1, NULL, (sockaddr*)&clnt, sizeof(clnt))) == SOCKET_ERROR)
                    throw  SetErrorMsgText("recv:", WSAGetLastError());
            }


            //cout << lobuf;
        }
        cout << k << endl;


        if (closesocket(sS) == SOCKET_ERROR)
            throw  SetErrorMsgText("Closesocket:", WSAGetLastError());
        //cout << "Socket closed succeeded." << endl;
        if (WSACleanup() == SOCKET_ERROR)
            throw  SetErrorMsgText("Cleanup:", WSAGetLastError());
 

    }
    catch (string errorMsgText)
    {
        cout << endl << errorMsgText;
    }
    system("pause");
    return 0;
}