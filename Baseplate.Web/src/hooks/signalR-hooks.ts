import { useState, useEffect} from 'react';
import * as signalR from '@microsoft/signalr';
import type { Message } from '@/types/message.t.ts';
import {HubConnection} from "@microsoft/signalr";

export const useSignalR = (url:string, messageHistory: Message[]) => {
    const [connection, setConnection] = useState<signalR.HubConnection | null>(null);
    const [messages, setMessages] = useState<Message[]>(messageHistory);
    const [connectionStatus, setConnectionStatus] = useState('Disconnected');

    useEffect(() => {
        const newConnection : HubConnection = new signalR.HubConnectionBuilder()
            .withUrl(url)
            .withAutomaticReconnect()
            .configureLogging(signalR.LogLevel.Information)
            .build();

        setConnection(newConnection);

        return () => {
            if (newConnection) {
                newConnection.stop();
            }
        };
    }, [url]);

    useEffect(() => {
        if (connection) {
            connection.start()
                .then(() => {
                    setConnectionStatus('Connected');
                    console.log('SignalR Connected');
                })
                .catch(error => {
                    setConnectionStatus('Failed');
                    console.error('Connection failed: ', error);
                });

            connection.onclose(() => {
                setConnectionStatus('Disconnected');
            });

            connection.onreconnecting(() => {
                setConnectionStatus('Reconnecting');
            });

            connection.onreconnected(() => {
                setConnectionStatus('Connected');
            });
        }
    }, [connection]);

    return { connection, messages, connectionStatus, setMessages };
};