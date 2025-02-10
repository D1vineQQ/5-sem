#include <iostream>
#include "Winsock2.h"
#include <ws2tcpip.h>
#include <string>
#pragma comment(lib, "WS2_32.lib")
#include <set>
#include <chrono>

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

bool GetRequestFromClient(const char* name, short port, struct sockaddr_in* from, int* flen) {
    char buffer[256];
    SOCKET sS;
    SOCKADDR_IN serv;
    int bytesRecv;

    sS = socket(AF_INET, SOCK_DGRAM, 0);
    if (sS == INVALID_SOCKET) {
        throw SetErrorMsgText("Socket creation failed:", WSAGetLastError());
    }
    serv.sin_family = AF_INET;
    serv.sin_port = htons(port);
    serv.sin_addr.s_addr = INADDR_ANY;

    if (bind(sS, (LPSOCKADDR)&serv, sizeof(serv)) == SOCKET_ERROR) {
        throw SetErrorMsgText("Bind failed:", WSAGetLastError());
    }

    *flen = sizeof(*from);
    bytesRecv = recvfrom(sS, buffer, sizeof(buffer), 0, (sockaddr*)from, flen);
    if (bytesRecv == SOCKET_ERROR) {
        throw SetErrorMsgText("recvfrom failed:", WSAGetLastError());
    }
    buffer[bytesRecv] = '\0';
    closesocket(sS);
    return strcmp(buffer, name) == 0;
}

bool PutAnswerToClient(const char* name, short port, const struct sockaddr_in* to, int tlen) {
    SOCKET sS = socket(AF_INET, SOCK_DGRAM, 0);
    if (sS == INVALID_SOCKET) {
        throw SetErrorMsgText("Socket creation failed:", WSAGetLastError());
    }

    int bytesSent = sendto(sS, name, strlen(name), 0, (sockaddr*)to, tlen);
    if (bytesSent == SOCKET_ERROR) {
        throw SetErrorMsgText("Send to client failed:", WSAGetLastError());
    }

    closesocket(sS);
    return true;
}

set<string> SearchForOtherServers(const char* name, short port, int timeoutSec) {
    SOCKET sS;
    SOCKADDR_IN serv;
    char buffer[256];
    strcpy_s(buffer, sizeof(buffer), name);
    sockaddr_in broadcastAddr;
    set<string> knownServers;

    sS = socket(AF_INET, SOCK_DGRAM, 0);
    if (sS == INVALID_SOCKET) {
        throw SetErrorMsgText("Socket creation failed:", WSAGetLastError());
    }

    int broadcastPermission = 1;
    setsockopt(sS, SOL_SOCKET, SO_BROADCAST, (char*)&broadcastPermission, sizeof(broadcastPermission));

    broadcastAddr.sin_family = AF_INET;
    broadcastAddr.sin_port = htons(port);
    broadcastAddr.sin_addr.s_addr = INADDR_BROADCAST;

    if (sendto(sS, buffer, strlen(buffer), 0, (sockaddr*)&broadcastAddr, sizeof(broadcastAddr)) == SOCKET_ERROR) {
        closesocket(sS);
        throw SetErrorMsgText("Broadcast failed:", WSAGetLastError());
    }
    u_long mode = 1;
    ioctlsocket(sS, FIONBIO, &mode);
    SOCKADDR_IN from;
    int fromLen = sizeof(from);
    auto start = chrono::steady_clock::now();

    while (true) {
        int bytesRecv = recvfrom(sS, buffer, sizeof(buffer), 0, (sockaddr*)&from, &fromLen);
        if (bytesRecv == SOCKET_ERROR) {
            if (WSAGetLastError() == WSAEWOULDBLOCK) {
                auto now = chrono::steady_clock::now();
                if (chrono::duration_cast<chrono::seconds>(now - start).count() > timeoutSec) {
                    break;  
                }
                continue;
            }
            else {
                throw SetErrorMsgText("recvfrom failed:", WSAGetLastError());
            }
        }
        buffer[bytesRecv] = '\0';
        if (strcmp(buffer, name) == 0) {
            char clientIp[INET_ADDRSTRLEN];
            inet_ntop(AF_INET, &(from.sin_addr), clientIp, INET_ADDRSTRLEN);
            knownServers.insert(clientIp);
        }
    }
    closesocket(sS);
    return knownServers;
}

int main() {
    WSADATA wsaData;
    SOCKADDR_IN from;
    int fromLen;
    char clientIp[INET_ADDRSTRLEN];
    set<string> knownServers;

    try {
        if (WSAStartup(MAKEWORD(2, 2), &wsaData) != 0) {
            throw SetErrorMsgText("WSAStartup failed:", WSAGetLastError());
        }
        knownServers = SearchForOtherServers("Hello", 2000, 5);

        cout << "Known servers count: " << knownServers.size() << endl;
        cout << "Known server IPs: ";
        for (const auto& ip : knownServers) {
            cout << ip << " ";
        }
        cout << endl;

        cout << "Server is waiting for client requests..." << endl;

        while (true) {
            if (GetRequestFromClient("Hello", 2000, &from, &fromLen)) {
                cout << "Correct server call sign received!" << endl;
                inet_ntop(AF_INET, &(from.sin_addr), clientIp, INET_ADDRSTRLEN);
                cout << "Client IP: " << clientIp << endl;
                cout << "Client port: " << ntohs(from.sin_port) << endl;
                if (PutAnswerToClient("Hello", 2000, &from, fromLen)) {
                    cout << "Response sent to the client." << endl;
                }
            }
            else {
                cout << "Request timed out or failed." << endl;
            }
        }

        if (WSACleanup() == SOCKET_ERROR) {
            throw SetErrorMsgText("WSACleanup failed:", WSAGetLastError());
        }
    }
    catch (const string& errorMsg) {
        cout << "Error: " << errorMsg << endl;
    }

    return 0;
}
